using AutoMapper;
using Library.API.Entities;
using Library.API.Helpers;
using Library.API.Models;
using Library.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.API.Controllers
{
    [Route("api/authors/{authorId}/books")]
    public class BooksController: Controller
    {
        private ILogger<BooksController> _logger;
        private ILibraryRepository _libraryRepository;
        private IUrlHelper _urlHelper;

        public BooksController(ILibraryRepository libraryRepository, ILogger<BooksController> logger, IUrlHelper urlHelper)
        {
            _logger = logger;
            _libraryRepository = libraryRepository;
            _urlHelper = urlHelper;
        }

        [HttpGet(Name = "GetBooksForAuthor")]
        public IActionResult GetBooksForAuthor(Guid authorId)
        {
            if (!_libraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            var booksForAuthorFromRepo = _libraryRepository.GetBooksForAuthor(authorId);
            var booksForAuthor = Mapper.Map<IEnumerable<BookDto>>(booksForAuthorFromRepo);

            #region CreateLinksForBooks => Supporting HATEOAS (Base and Wrapper Class Approach)
            booksForAuthor = booksForAuthor.Select(book =>
            {
                book = CreateLinksForBook(book);
                return book;
            });

            var wrapper = new LinkedCollectionResourceWrapperDto<BookDto>(booksForAuthor);
            #endregion

            //  CreateLinksForBooks => Supporting HATEOAS (Base and Wrapper Class Approach)
            return Ok(CreateLinksForBooks(wrapper));

        }

        [HttpGet("{id}", Name = "GetBookForAuthor")]
        public IActionResult GetBookForAuthor(Guid authorId, Guid id)
        {
            if (!_libraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var bookForAuthorFromRepo = _libraryRepository.GetBookForAuthor(authorId, id);
            if (bookForAuthorFromRepo == null)
            {
                return NotFound();
            }

            var bookForAuthor = Mapper.Map<BookDto>(bookForAuthorFromRepo);

            //return Ok(bookForAuthor);

                    //  CreateLinksForBooks => Supporting HATEOAS (Base and Wrapper Class Approach)
            return Ok(CreateLinksForBook(bookForAuthor));
        }

        [HttpPost(Name = "CreateBookForAuthor")]
        public IActionResult CreateBookForAuthor(Guid authorId, 
            [FromBody] BookForCreationDto book)
        {
            if (book == null)
            {
                return BadRequest();
            }

            if (book.Description == book.Title)
            {
                                        //  NAME of the property to be sent in headers as Key value pair
                ModelState.AddModelError(nameof(BookForCreationDto),
                    "The provided description should be different from the title.");
                    //  VALUE of the property to be sent in headers as Key value pair
            }

            if (!ModelState.IsValid)
            {
                // return 422 
                // Custom Error Handler => returns the Key Value pair Error of the specific property 
                //( e.g. Title, Description ) which causes error    =>  Below is example
                /*
                    {
                        "title": [
                            "The title shouldn't have more than 100 characters."
                        ],
                        "description": [
                            "The description shouldn't have more than 500 characters."
                        ],
                        "bookForCreationDto": [
                            "The provided description should be different from the title."
                        ]
                    }                 
                 */
                return new UnprocessableEntityObjectResult(ModelState);
            }

            if (!_libraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var bookEntity = Mapper.Map<Book>(book);

            _libraryRepository.AddBookForAuthor(authorId, bookEntity);

            if (!_libraryRepository.Save())
            {
                throw new Exception($"Creating a book for author {authorId} failed on save.");
            }

            var bookToReturn = Mapper.Map<BookDto>(bookEntity);

            return CreatedAtRoute("GetBookForAuthor",
                new { authorId = authorId, id=bookToReturn.Id },
                //  CreateLinksForBooks => Supporting HATEOAS (Base and Wrapper Class Approach)
                CreateLinksForBook(bookToReturn));
        }

        [HttpDelete("{id}", Name = "DeleteBookForAuthor")]
        public IActionResult DeleteBookForAuthor(Guid authorId, Guid id)
        {
            if (!_libraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var bookForAuthorFromRepo = _libraryRepository.GetBookForAuthor(authorId, id);
            if (bookForAuthorFromRepo == null)
            {
                return NotFound();
            }

            _libraryRepository.DeleteBook(bookForAuthorFromRepo);

            if (!_libraryRepository.Save())
            {
                throw new Exception($"Deleting book {id} for author {authorId} failed on save.");
            }

            _logger.LogInformation(100, $"Book {id} for author {authorId} was deleted.");

            return NoContent();
        }

        // PUT the update is on RESOURSE or ***DTO.cs file but not on Entity (Entities.Book)
        [HttpPut("{id}", Name = "UpdateBookForAuthor")]
        public IActionResult UpdateBookForAuthor(Guid authorId, Guid id, 
            [FromBody] BookForUpdateDto book)
        {
            if (book == null)
            {
                return BadRequest();
            }

            if (book.Description == book.Title)
            {
                ModelState.AddModelError(nameof(BookForUpdateDto),
                    "The provided description should be different from the title.");
            }

            if (!ModelState.IsValid)
            {
                // Custom Error Handler => returns the Key Value pair Error of the specific property 
                //( e.g. Title, Description ) which causes error    =>  Below is example
                /*
                    {
                        "title": [
                            "The title shouldn't have more than 100 characters."
                        ],
                        "description": [
                            "The description shouldn't have more than 500 characters."
                        ],
                        "bookForCreationDto": [
                            "The provided description should be different from the title."
                        ]
                    }                 
                 */
                return new UnprocessableEntityObjectResult(ModelState);
            }

            if (!_libraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var bookForAuthorFromRepo = _libraryRepository.GetBookForAuthor(authorId, id);
            if (bookForAuthorFromRepo == null)
            {
                // OPTION 1 => UPDATE
                #region UPDATE
                //  return NotFound();
                #endregion

                // OPTION 2 => UPSERTING => CREATE IF Book DOES NOT EXIST
                    #region UPSERTING => PUT Working as POST
                        var bookToAdd = Mapper.Map<Book>(book);
                        bookToAdd.Id = id;

                        _libraryRepository.AddBookForAuthor(authorId, bookToAdd);

                        if (!_libraryRepository.Save())
                        {
                            throw new Exception($"Upserting book {id} for author {authorId} failed on save.");
                        }

                        var bookToReturn = Mapper.Map<BookDto>(bookToAdd);

                        return CreatedAtRoute("GetBookForAuthor",
                            new { authorId = authorId, id = bookToReturn.Id },
                            bookToReturn);
                    #endregion

            }

            // MAP the ENTITY (Entities.Book) to BookForUpdateDto
            // APPLY the updated FIELD values to BookForUpdateDto
            // MAP the BookForUpdateDto to ENTITY (Entities.Book)
            // USE  MAPPER shown below =>

            //          SOURCE  =>  DESTINATION
            Mapper.Map(book, bookForAuthorFromRepo);
            
            // This method is empty as the above Mapper method the entity has changed to a modified state.
            // Thus executing a SAVE will write changes to database.
            _libraryRepository.UpdateBookForAuthor(bookForAuthorFromRepo);

            if (!_libraryRepository.Save())
            {
                throw new Exception($"Updating book {id} for author {authorId} failed on save.");
            }

            return NoContent();
        }

        [HttpPatch("{id}", Name = "PartiallyUpdateBookForAuthor")]
        public IActionResult PartiallyUpdateBookForAuthor(Guid authorId, Guid id,
            [FromBody] JsonPatchDocument<BookForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            if (!_libraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var bookForAuthorFromRepo = _libraryRepository.GetBookForAuthor(authorId, id);
            if (bookForAuthorFromRepo == null)
            {
                // OPTION 1 => PATCH - PARTIAL UPDATE
                #region PATCH - PARTIAL UPDATE
                //  return NotFound();
                #endregion

                // OPTION 2 => UPSERTING => CREATE IF Book DOES NOT EXIST
                #region UPSERTING => PATCH Working as POST

                    var bookDto = new BookForUpdateDto();
                    patchDoc.ApplyTo(bookDto, ModelState);

                    if (bookDto.Description == bookDto.Title)
                    {
                        ModelState.AddModelError(nameof(BookForUpdateDto), 
                            "The provided description should be different from the title.");
                    }
                    //  Trigger Validation of bookDto, without TryValidateModel !ModelState.IsValid will work for patchDoc which is JsonPatchDocument but not bookDto which is BookForUpdateDto
                    // All this because input content is of type JsonPatchDocument but not of type BookForUpdateDto
                    TryValidateModel(bookDto);

                    if (!ModelState.IsValid)
                    {
                    // Custom Error Handler => returns the Key Value pair Error of the specific property 
                    //( e.g. Title, Description ) which causes error    =>  Below is example
                    /*
                        {
                            "title": [
                                "The title shouldn't have more than 100 characters."
                            ],
                            "description": [
                                "The description shouldn't have more than 500 characters."
                            ],
                            "bookForCreationDto": [
                                "The provided description should be different from the title."
                            ]
                        }                 
                     */
                    return new UnprocessableEntityObjectResult(ModelState);
                    }

                    var bookToAdd = Mapper.Map<Book>(bookDto);
                    bookToAdd.Id = id;

                    _libraryRepository.AddBookForAuthor(authorId, bookToAdd);

                    if (!_libraryRepository.Save())
                    {
                        throw new Exception($"Upserting book {id} for author {authorId} failed on save.");
                    }

                    var bookToReturn = Mapper.Map<BookDto>(bookToAdd);
                    return CreatedAtRoute("GetBookForAuthor",
                        new { authorId = authorId, id = bookToReturn.Id },
                        bookToReturn);

                #endregion
            }

            var bookToPatch = Mapper.Map<BookForUpdateDto>(bookForAuthorFromRepo);

            patchDoc.ApplyTo(bookToPatch, ModelState);

            //  Custom / 3rd Party Validation Rule and its error
            // Another Custom Validation we can use is FluentValidation
            if (bookToPatch.Description == bookToPatch.Title)
            {
                ModelState.AddModelError(nameof(BookForUpdateDto),
                    "The provided description should be different from the title.");
            }

            //  Trigger Validation of bookToPatch, without TryValidateModel !ModelState.IsValid will work for patchDoc which is JsonPatchDocument but not bookToPatch which is BookForUpdateDto
            // All this because input content is of type JsonPatchDocument but not of type BookForUpdateDto
            TryValidateModel(bookToPatch);

            if (!ModelState.IsValid)
            {
                // Custom Error Handler => returns the Key Value pair Error of the specific property 
                //( e.g. Title, Description ) which causes error    =>  Below is example
                /*
                    {
                        "title": [
                            "The title shouldn't have more than 100 characters."
                        ],
                        "description": [
                            "The description shouldn't have more than 500 characters."
                        ],
                        "bookForCreationDto": [
                            "The provided description should be different from the title."
                        ]
                    }                 
                 */
                return new UnprocessableEntityObjectResult(ModelState);
            }

            Mapper.Map(bookToPatch, bookForAuthorFromRepo);

            _libraryRepository.UpdateBookForAuthor(bookForAuthorFromRepo);

            if (!_libraryRepository.Save())
            {
                throw new Exception($"Patching book {id} for author {authorId} failed on save.");
            }

            return NoContent();
        }

        #region Supporting HATEOAS (Static Properties Approach)
        private BookDto CreateLinksForBook(BookDto book)
        {
            book.Links.Add(new LinkDto(_urlHelper.Link("GetBookForAuthor",
                new { id = book.Id }),
                "self",
                "GET"));

            book.Links.Add(
                new LinkDto(_urlHelper.Link("DeleteBookForAuthor",
                new { id = book.Id }),
                "delete_book",
                "DELETE"));

            book.Links.Add(
                new LinkDto(_urlHelper.Link("UpdateBookForAuthor",
                new { id = book.Id }),
                "update_book",
                "PUT"));

            book.Links.Add(
                new LinkDto(_urlHelper.Link("PartiallyUpdateBookForAuthor",
                new { id = book.Id }),
                "partially_update_book",
                "PATCH"));

            return book;
        }

        private LinkedCollectionResourceWrapperDto<BookDto> CreateLinksForBooks(
            LinkedCollectionResourceWrapperDto<BookDto> booksWrapper)
        {
            // link to self
            booksWrapper.Links.Add(
                new LinkDto(_urlHelper.Link("GetBooksForAuthor", new { }),
                "self",
                "GET"));

            return booksWrapper;
        }
        #endregion
    }
}
