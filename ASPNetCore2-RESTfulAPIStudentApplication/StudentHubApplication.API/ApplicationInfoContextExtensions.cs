using StudentHubApplication.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHubApplication.API
{
    public static class ApplicationInfoContextExtensions
    {
        public static void EnsureSeedDataForContext(this ApplicationInfoContext applicationInfoContext)
        {
            applicationInfoContext.Applications.RemoveRange(applicationInfoContext.Applications);
            applicationInfoContext.ApplicationQualifications.RemoveRange(applicationInfoContext.ApplicationQualifications);
            applicationInfoContext.Qualifications.RemoveRange(applicationInfoContext.Qualifications);
            applicationInfoContext.Countries.RemoveRange(applicationInfoContext.Countries);
            applicationInfoContext.Courses.RemoveRange(applicationInfoContext.Courses);
            applicationInfoContext.Campuses.RemoveRange(applicationInfoContext.Campuses);
            applicationInfoContext.CourseCampuses.RemoveRange(applicationInfoContext.CourseCampuses);
            applicationInfoContext.ApplicationCourseCampuses.RemoveRange(applicationInfoContext.ApplicationCourseCampuses);
            //applicationInfoContext.Database.EnsureDeleted();
            applicationInfoContext.SaveChanges();

            var countries = new List<Country>()
            {
                new Country()
                {
                    Code = "IN",
                    Name = "India"
                },
                new Country()
                {
                    Code = "AU",
                    Name = "Australia"
                },
                new Country()
                {
                    Code = "SA",
                    Name = "South Africa"
                },
                new Country()
                {
                    Code = "UK",
                    Name = "United Kingdom"
                },
                new Country()
                {
                    Code = "FR",
                    Name = "France"
                },
                new Country()
                {
                    Code = "NZ",
                    Name = "New Zealand"
                }
            };
            applicationInfoContext.AddRange(countries);
            applicationInfoContext.SaveChanges();

            var applications = new List<Application>()
            {
                new Application()
                {
                    FirstName = "Vaibhav",
                    LastName = "Sharma",
                    DateOfBirth = new DateTimeOffset(new DateTime(1988, 9, 21)),
                    Gender = "Male",
                    CountryOfResidenceId = countries[0].Id,
                    CountryOfBirthId = countries[0].Id
                },
                new Application()
                {
                    FirstName = "Varun",
                    LastName = "Bhargava",
                    DateOfBirth = new DateTimeOffset(new DateTime(1987, 4, 13)),
                    Gender = "Male",
                    CountryOfResidenceId =countries[1].Id,
                    CountryOfBirthId = countries[0].Id
                },
                new Application()
                {
                    FirstName = "Vijay",
                    LastName = "Chauhan",
                    DateOfBirth = new DateTimeOffset(new DateTime(1985, 5, 19)),
                    Gender = "Male",
                    CountryOfResidenceId =countries[2].Id,
                    CountryOfBirthId = countries[1].Id
                },
                new Application()
                {
                    FirstName = "Asha",
                    LastName = "Chauhan",
                    DateOfBirth = new DateTimeOffset(new DateTime(1987, 8, 22)),
                    Gender = "Female",
                    CountryOfResidenceId =countries[0].Id,
                    CountryOfBirthId = countries[1].Id
                },
                new Application()
                {
                    FirstName = "Bhavna",
                    LastName = "Bhargava",
                    DateOfBirth = new DateTimeOffset(new DateTime(1988, 7, 11)),
                    Gender = "Female",
                    CountryOfResidenceId =countries[2].Id,
                    CountryOfBirthId = countries[0].Id
                },
                new Application()
                {
                    FirstName = "Chitra",
                    LastName = "Sharma",
                    DateOfBirth = new DateTimeOffset(new DateTime(1989, 3, 18)),
                    Gender = "Female",
                    CountryOfResidenceId =countries[0].Id,
                    CountryOfBirthId = countries[1].Id
                },
                new Application()
                {
                    FirstName = "Alex",
                    LastName = "Mendes",
                    DateOfBirth = new DateTimeOffset(new DateTime(1989, 3, 18)),
                    Gender = "Other",
                    CountryOfResidenceId =countries[2].Id,
                    CountryOfBirthId = countries[1].Id
                },
                new Application()
                {
                    FirstName = "Murli Prasad",
                    LastName = "Sharma",
                    DateOfBirth = new DateTimeOffset(new DateTime(1988, 9, 28)),
                    Gender = "Male",
                    CountryOfResidenceId = countries[3].Id,
                    CountryOfBirthId = countries[4].Id
                },
                new Application()
                {
                    FirstName = "Amit",
                    LastName = "Bhargava",
                    DateOfBirth = new DateTimeOffset(new DateTime(1983, 4, 18)),
                    Gender = "Male",
                    CountryOfResidenceId =countries[4].Id,
                    CountryOfBirthId = countries[5].Id
                },
                new Application()
                {
                    FirstName = "Ajay",
                    LastName = "Chauhan",
                    DateOfBirth = new DateTimeOffset(new DateTime(1980, 5, 19)),
                    Gender = "Male",
                    CountryOfResidenceId =countries[5].Id,
                    CountryOfBirthId = countries[3].Id
                },
                new Application()
                {
                    FirstName = "Anita",
                    LastName = "Chauhan",
                    DateOfBirth = new DateTimeOffset(new DateTime(1987, 6, 17)),
                    Gender = "Female",
                    CountryOfResidenceId =countries[5].Id,
                    CountryOfBirthId = countries[4].Id
                },
                new Application()
                {
                    FirstName = "Rajni",
                    LastName = "Bhargava",
                    DateOfBirth = new DateTimeOffset(new DateTime(1988, 3, 19)),
                    Gender = "Female",
                    CountryOfResidenceId =countries[3].Id,
                    CountryOfBirthId = countries[5].Id
                },
                new Application()
                {
                    FirstName = "Priyanka",
                    LastName = "Sharma",
                    DateOfBirth = new DateTimeOffset(new DateTime(1979, 3, 15)),
                    Gender = "Female",
                    CountryOfResidenceId =countries[5].Id,
                    CountryOfBirthId = countries[1].Id
                },
                new Application()
                {
                    FirstName = "Peter",
                    LastName = "Jackson",
                    DateOfBirth = new DateTimeOffset(new DateTime(1989, 3, 18)),
                    Gender = "Other",
                    CountryOfResidenceId =countries[2].Id,
                    CountryOfBirthId = countries[5].Id
                }
            };
            applicationInfoContext.AddRange(applications);
            applicationInfoContext.SaveChanges();

            var qualifications = new List<Qualification>()
            {
                new Qualification()
                {
                    Name = "Certificate I"
                },
                new Qualification()
                {
                    Name = "Certificate II"
                },
                new Qualification()
                {
                    Name = "Certificate III"
                },
                new Qualification()
                {
                    Name = "Certificate IV or Advanced Certificate"
                },
                new Qualification()
                {
                    Name = "Diploma or Associate Diploma"
                },
                new Qualification()
                {
                    Name = "Advanced Diploma or Associate Degree"
                },
                new Qualification()
                {
                    Name = "Bachelor Degree or Higher Degree"
                }
            };
            applicationInfoContext.AddRange(qualifications);
            applicationInfoContext.SaveChanges();

            var courses = new List<Course>()
            {
                new Course()
                {
                    Active = true,
                    CourseCode = "CPC30211",
                    Name = "Certificate III in Carpentry"
                },
                new Course()
                {
                    Active = true,
                    CourseCode = "SIH30111",
                    Name = "Certificate II in Hairdressing"
                },
                new Course()
                {
                    Active = true,
                    CourseCode = "SIT30116",
                    Name = "Certificate III in Tourism"
                },
                new Course()
                {
                    Active = false,
                    CourseCode = "SHB30516",
                    Name = "Certificate III in Hairdressing"
                }
            };
            applicationInfoContext.AddRange(courses);
            applicationInfoContext.SaveChanges();

            var campuses = new List<Campus>()
            {
                new Campus() { Name = "ACE Docklands", CampusCode = "AD" },
                new Campus() { Name = "Bendigo City", CampusCode = "BC" },
                new Campus() { Name = "Broadmeadows", CampusCode = "BR" },
                new Campus() { Name = "BTEC", CampusCode = "BT" },
                new Campus() { Name = "Castlemaine", CampusCode = "CA" },
                new Campus() { Name = "Charleston", CampusCode = "CH" },
                new Campus() { Name = "Echuca", CampusCode = "EC" },
                new Campus() { Name = "Essendon", CampusCode = "ES" },
                new Campus() { Name = "Moonee Ponds", CampusCode = "MP" },
                new Campus() { Name = "Richmond", CampusCode = "RI" }

            };

            applicationInfoContext.AddRange(campuses);
            applicationInfoContext.SaveChanges();

            var courseCampus = new List<CourseCampus>() {
                    new CourseCampus { Course = courses[0], Campus = campuses[0] },
                    new CourseCampus { Course = courses[0], Campus = campuses[1] },
                    new CourseCampus { Course = courses[0], Campus = campuses[2] },
                    new CourseCampus { Course = courses[1], Campus = campuses[2] },
                    new CourseCampus { Course = courses[1], Campus = campuses[3] },
                    new CourseCampus { Course = courses[1], Campus = campuses[4] },
                    new CourseCampus { Course = courses[2], Campus = campuses[3] },
                    new CourseCampus { Course = courses[2], Campus = campuses[4] },
                    new CourseCampus { Course = courses[2], Campus = campuses[5] },
                    new CourseCampus { Course = courses[2], Campus = campuses[6] },
                    new CourseCampus { Course = courses[3], Campus = campuses[6] },
                    new CourseCampus { Course = courses[3], Campus = campuses[7] },
                    new CourseCampus { Course = courses[3], Campus = campuses[8] }
            };

            applicationInfoContext.AddRange(courseCampus);
            applicationInfoContext.SaveChanges();


            applicationInfoContext.AddRange(
                    new ApplicationQualification { Qualification = qualifications[0], Application = applications[0], CompletionDate = new DateTime(2000, 4, 29, 11, 40, 39), IsPrimaryQualification = false, Notes="New Qualification has been added" },
                    new ApplicationQualification { Qualification = qualifications[1], Application = applications[0], CompletionDate = new DateTime(2001, 4, 29, 11, 40, 39), IsPrimaryQualification = true , Notes="New Qualification has been added"},
                    new ApplicationQualification { Qualification = qualifications[0], Application = applications[1], CompletionDate = new DateTime(2002, 4, 29, 11, 40, 39), IsPrimaryQualification = false, Notes="New Qualification has been added" },
                    new ApplicationQualification { Qualification = qualifications[1], Application = applications[1], CompletionDate = new DateTime(2003, 4, 29, 11, 40, 39), IsPrimaryQualification = true , Notes="New Qualification has been added"},
                    new ApplicationQualification { Qualification = qualifications[2], Application = applications[1], CompletionDate = new DateTime(2004, 4, 29, 11, 40, 39), IsPrimaryQualification = false, Notes="New Qualification has been added" },
                    new ApplicationQualification { Qualification = qualifications[3], Application = applications[1], CompletionDate = new DateTime(2005, 4, 29, 11, 40, 39), IsPrimaryQualification = true , Notes="New Qualification has been added"},
                    new ApplicationQualification { Qualification = qualifications[4], Application = applications[1], CompletionDate = new DateTime(2006, 4, 29, 11, 40, 39), IsPrimaryQualification = true , Notes="New Qualification has been added"},
                    new ApplicationQualification { Qualification = qualifications[5], Application = applications[1], CompletionDate = new DateTime(2007, 4, 29, 11, 40, 39), IsPrimaryQualification = false, Notes="New Qualification has been added" },
                    new ApplicationQualification { Qualification = qualifications[6], Application = applications[1], CompletionDate = new DateTime(2008, 8, 13, 11, 40, 39), IsPrimaryQualification = true , Notes="New Qualification has been added"},
                    new ApplicationQualification { Qualification = qualifications[4], Application = applications[2], CompletionDate = new DateTime(2004, 3, 26, 11, 40, 39), IsPrimaryQualification = false, Notes="New Qualification has been added" },
                    new ApplicationQualification { Qualification = qualifications[1], Application = applications[2], CompletionDate = new DateTime(2005, 1, 11, 11, 40, 39), IsPrimaryQualification = true, Notes = "New Qualification has been added" },
                    new ApplicationQualification { Qualification = qualifications[5], Application = applications[3], CompletionDate = new DateTime(2006, 1, 11, 11, 40, 39), IsPrimaryQualification = true, Notes = "New Qualification has been added" },
                    new ApplicationQualification { Qualification = qualifications[3], Application = applications[3], CompletionDate = new DateTime(2007, 1, 11, 11, 40, 39), IsPrimaryQualification = true, Notes = "New Qualification has been added" },
                    new ApplicationQualification { Qualification = qualifications[4], Application = applications[4], CompletionDate = new DateTime(2003, 1, 11, 11, 40, 39), IsPrimaryQualification = true, Notes = "New Qualification has been added" },
                    new ApplicationQualification { Qualification = qualifications[2], Application = applications[4], CompletionDate = new DateTime(2001, 1, 11, 11, 40, 39), IsPrimaryQualification = true, Notes = "New Qualification has been added" },
                    new ApplicationQualification { Qualification = qualifications[3], Application = applications[5], CompletionDate = new DateTime(2002, 1, 11, 11, 40, 39), IsPrimaryQualification = true, Notes = "New Qualification has been added" },
                    new ApplicationQualification { Qualification = qualifications[6], Application = applications[5], CompletionDate = new DateTime(2007, 1, 11, 11, 40, 39), IsPrimaryQualification = true, Notes = "New Qualification has been added" },
                    new ApplicationQualification { Qualification = qualifications[5], Application = applications[5], CompletionDate = new DateTime(2005, 1, 11, 11, 40, 39), IsPrimaryQualification = true, Notes = "New Qualification has been added" },
                    new ApplicationQualification { Qualification = qualifications[3], Application = applications[6], CompletionDate = new DateTime(2009, 1, 11, 11, 40, 39), IsPrimaryQualification = true, Notes = "New Qualification has been added" },
                    new ApplicationQualification { Qualification = qualifications[6], Application = applications[6], CompletionDate = new DateTime(2008, 1, 11, 11, 40, 39), IsPrimaryQualification = true, Notes = "New Qualification has been added" },
                    new ApplicationQualification { Qualification = qualifications[5], Application = applications[6], CompletionDate = new DateTime(2005, 1, 11, 11, 40, 39), IsPrimaryQualification = true, Notes = "New Qualification has been added" },
                    new ApplicationQualification { Qualification = qualifications[0], Application = applications[7], CompletionDate = new DateTime(2000, 4, 29, 11, 40, 39), IsPrimaryQualification = false, Notes = "New Qualification has been added" },
                    new ApplicationQualification { Qualification = qualifications[1], Application = applications[7], CompletionDate = new DateTime(2001, 4, 29, 11, 40, 39), IsPrimaryQualification = true, Notes = "New Qualification has been added" },
                    new ApplicationQualification { Qualification = qualifications[0], Application = applications[8], CompletionDate = new DateTime(2002, 4, 29, 11, 40, 39), IsPrimaryQualification = false, Notes = "New Qualification has been added" },
                    new ApplicationQualification { Qualification = qualifications[1], Application = applications[8], CompletionDate = new DateTime(2003, 4, 29, 11, 40, 39), IsPrimaryQualification = true, Notes = "New Qualification has been added" },
                    new ApplicationQualification { Qualification = qualifications[2], Application = applications[8], CompletionDate = new DateTime(2004, 4, 29, 11, 40, 39), IsPrimaryQualification = false, Notes = "New Qualification has been added" },
                    new ApplicationQualification { Qualification = qualifications[3], Application = applications[8], CompletionDate = new DateTime(2005, 4, 29, 11, 40, 39), IsPrimaryQualification = true, Notes = "New Qualification has been added" },
                    new ApplicationQualification { Qualification = qualifications[4], Application = applications[8], CompletionDate = new DateTime(2006, 4, 29, 11, 40, 39), IsPrimaryQualification = true, Notes = "New Qualification has been added" },
                    new ApplicationQualification { Qualification = qualifications[5], Application = applications[8], CompletionDate = new DateTime(2007, 4, 29, 11, 40, 39), IsPrimaryQualification = false, Notes = "New Qualification has been added" },
                    new ApplicationQualification { Qualification = qualifications[6], Application = applications[8], CompletionDate = new DateTime(2008, 8, 13, 11, 40, 39), IsPrimaryQualification = true, Notes = "New Qualification has been added" },
                    new ApplicationQualification { Qualification = qualifications[4], Application = applications[9], CompletionDate = new DateTime(2004, 3, 26, 11, 40, 39), IsPrimaryQualification = false, Notes = "New Qualification has been added" },
                    new ApplicationQualification { Qualification = qualifications[1], Application = applications[9], CompletionDate = new DateTime(2005, 1, 11, 11, 40, 39), IsPrimaryQualification = true, Notes = "New Qualification has been added" },
                    new ApplicationQualification { Qualification = qualifications[5], Application = applications[10], CompletionDate = new DateTime(2006, 1, 11, 11, 40, 39), IsPrimaryQualification = true, Notes = "New Qualification has been added" },
                    new ApplicationQualification { Qualification = qualifications[3], Application = applications[10], CompletionDate = new DateTime(2007, 1, 11, 11, 40, 39), IsPrimaryQualification = true, Notes = "New Qualification has been added" },
                    new ApplicationQualification { Qualification = qualifications[4], Application = applications[11], CompletionDate = new DateTime(2003, 1, 11, 11, 40, 39), IsPrimaryQualification = true, Notes = "New Qualification has been added" },
                    new ApplicationQualification { Qualification = qualifications[2], Application = applications[11], CompletionDate = new DateTime(2001, 1, 11, 11, 40, 39), IsPrimaryQualification = true, Notes = "New Qualification has been added" },
                    new ApplicationQualification { Qualification = qualifications[3], Application = applications[12], CompletionDate = new DateTime(2002, 1, 11, 11, 40, 39), IsPrimaryQualification = true, Notes = "New Qualification has been added" },
                    new ApplicationQualification { Qualification = qualifications[6], Application = applications[12], CompletionDate = new DateTime(2007, 1, 11, 11, 40, 39), IsPrimaryQualification = true, Notes = "New Qualification has been added" },
                    new ApplicationQualification { Qualification = qualifications[5], Application = applications[12], CompletionDate = new DateTime(2005, 1, 11, 11, 40, 39), IsPrimaryQualification = true, Notes = "New Qualification has been added" },
                    new ApplicationQualification { Qualification = qualifications[3], Application = applications[13], CompletionDate = new DateTime(2009, 1, 11, 11, 40, 39), IsPrimaryQualification = true, Notes = "New Qualification has been added" },
                    new ApplicationQualification { Qualification = qualifications[6], Application = applications[13], CompletionDate = new DateTime(2008, 1, 11, 11, 40, 39), IsPrimaryQualification = true, Notes = "New Qualification has been added" },
                    new ApplicationQualification { Qualification = qualifications[5], Application = applications[13], CompletionDate = new DateTime(2005, 1, 11, 11, 40, 39), IsPrimaryQualification = true, Notes = "New Qualification has been added" }
                );
            applicationInfoContext.SaveChanges();

            applicationInfoContext.AddRange(
                    new ApplicationCourseCampus { CourseCampus = courseCampus[0], Application = applications[0], IsPrimaryLocation = true ,Notes = "New Course and Campus choice has been added"},
                    new ApplicationCourseCampus { CourseCampus = courseCampus[1], Application = applications[0], IsPrimaryLocation = true ,Notes = "New Course and Campus choice has been added"},
                    new ApplicationCourseCampus { CourseCampus = courseCampus[2], Application = applications[0], IsPrimaryLocation = false,Notes = "New Course and Campus choice has been added" },
                    new ApplicationCourseCampus { CourseCampus = courseCampus[2], Application = applications[1], IsPrimaryLocation = true ,Notes = "New Course and Campus choice has been added"},
                    new ApplicationCourseCampus { CourseCampus = courseCampus[5], Application = applications[1], IsPrimaryLocation = false,Notes = "New Course and Campus choice has been added" },
                    new ApplicationCourseCampus { CourseCampus = courseCampus[6], Application = applications[1], IsPrimaryLocation = true ,Notes = "New Course and Campus choice has been added"},
                    new ApplicationCourseCampus { CourseCampus = courseCampus[7], Application = applications[1], IsPrimaryLocation = false,Notes = "New Course and Campus choice has been added" },
                    new ApplicationCourseCampus { CourseCampus = courseCampus[4], Application = applications[2], IsPrimaryLocation = true ,Notes = "New Course and Campus choice has been added"},
                    new ApplicationCourseCampus { CourseCampus = courseCampus[7], Application = applications[2], IsPrimaryLocation = true, Notes = "New Course and Campus choice has been added" },
                    new ApplicationCourseCampus { CourseCampus = courseCampus[1], Application = applications[3], IsPrimaryLocation = true, Notes = "New Course and Campus choice has been added" },
                    new ApplicationCourseCampus { CourseCampus = courseCampus[2], Application = applications[3], IsPrimaryLocation = true, Notes = "New Course and Campus choice has been added" },
                    new ApplicationCourseCampus { CourseCampus = courseCampus[5], Application = applications[4], IsPrimaryLocation = true, Notes = "New Course and Campus choice has been added" },
                    new ApplicationCourseCampus { CourseCampus = courseCampus[7], Application = applications[4], IsPrimaryLocation = true, Notes = "New Course and Campus choice has been added" },
                    new ApplicationCourseCampus { CourseCampus = courseCampus[0], Application = applications[5], IsPrimaryLocation = true, Notes = "New Course and Campus choice has been added" },
                    new ApplicationCourseCampus { CourseCampus = courseCampus[3], Application = applications[5], IsPrimaryLocation = true, Notes = "New Course and Campus choice has been added" },
                    new ApplicationCourseCampus { CourseCampus = courseCampus[6], Application = applications[5], IsPrimaryLocation = true, Notes = "New Course and Campus choice has been added" },
                    new ApplicationCourseCampus { CourseCampus = courseCampus[0], Application = applications[6], IsPrimaryLocation = true, Notes = "New Course and Campus choice has been added" },
                    new ApplicationCourseCampus { CourseCampus = courseCampus[3], Application = applications[6], IsPrimaryLocation = true, Notes = "New Course and Campus choice has been added" },
                    new ApplicationCourseCampus { CourseCampus = courseCampus[6], Application = applications[6], IsPrimaryLocation = true, Notes = "New Course and Campus choice has been added" },
                    new ApplicationCourseCampus { CourseCampus = courseCampus[0], Application = applications[7], IsPrimaryLocation = true, Notes = "New Course and Campus choice has been added" },
                    new ApplicationCourseCampus { CourseCampus = courseCampus[1], Application = applications[7], IsPrimaryLocation = true, Notes = "New Course and Campus choice has been added" },
                    new ApplicationCourseCampus { CourseCampus = courseCampus[2], Application = applications[7], IsPrimaryLocation = false, Notes = "New Course and Campus choice has been added" },
                    new ApplicationCourseCampus { CourseCampus = courseCampus[2], Application = applications[8], IsPrimaryLocation = true, Notes = "New Course and Campus choice has been added" },
                    new ApplicationCourseCampus { CourseCampus = courseCampus[5], Application = applications[8], IsPrimaryLocation = false, Notes = "New Course and Campus choice has been added" },
                    new ApplicationCourseCampus { CourseCampus = courseCampus[6], Application = applications[8], IsPrimaryLocation = true, Notes = "New Course and Campus choice has been added" },
                    new ApplicationCourseCampus { CourseCampus = courseCampus[7], Application = applications[8], IsPrimaryLocation = false, Notes = "New Course and Campus choice has been added" },
                    new ApplicationCourseCampus { CourseCampus = courseCampus[4], Application = applications[9], IsPrimaryLocation = true, Notes = "New Course and Campus choice has been added" },
                    new ApplicationCourseCampus { CourseCampus = courseCampus[7], Application = applications[9], IsPrimaryLocation = true, Notes = "New Course and Campus choice has been added" },
                    new ApplicationCourseCampus { CourseCampus = courseCampus[1], Application = applications[10], IsPrimaryLocation = true, Notes = "New Course and Campus choice has been added" },
                    new ApplicationCourseCampus { CourseCampus = courseCampus[2], Application = applications[10], IsPrimaryLocation = true, Notes = "New Course and Campus choice has been added" },
                    new ApplicationCourseCampus { CourseCampus = courseCampus[5], Application = applications[11], IsPrimaryLocation = true, Notes = "New Course and Campus choice has been added" },
                    new ApplicationCourseCampus { CourseCampus = courseCampus[7], Application = applications[11], IsPrimaryLocation = true, Notes = "New Course and Campus choice has been added" },
                    new ApplicationCourseCampus { CourseCampus = courseCampus[0], Application = applications[12], IsPrimaryLocation = true, Notes = "New Course and Campus choice has been added" },
                    new ApplicationCourseCampus { CourseCampus = courseCampus[3], Application = applications[12], IsPrimaryLocation = true, Notes = "New Course and Campus choice has been added" },
                    new ApplicationCourseCampus { CourseCampus = courseCampus[6], Application = applications[12], IsPrimaryLocation = true, Notes = "New Course and Campus choice has been added" },
                    new ApplicationCourseCampus { CourseCampus = courseCampus[0], Application = applications[13], IsPrimaryLocation = true, Notes = "New Course and Campus choice has been added" },
                    new ApplicationCourseCampus { CourseCampus = courseCampus[3], Application = applications[13], IsPrimaryLocation = true, Notes = "New Course and Campus choice has been added" },
                    new ApplicationCourseCampus { CourseCampus = courseCampus[6], Application = applications[13], IsPrimaryLocation = true, Notes = "New Course and Campus choice has been added" }
                );
            applicationInfoContext.SaveChanges();


        }
    }
}
