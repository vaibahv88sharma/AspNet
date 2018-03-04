﻿using System.ComponentModel.DataAnnotations;

namespace Library.API.Models
{                                   //  Parent Abstract Class containing Model + Validation for other DTOs
    public class BookForUpdateDto : BookForManipulationDto
    {
        [Required(ErrorMessage = "You should fill out a description.")]
        public override string Description
        {
            get
            {
                return base.Description;
            }

            set
            {
                base.Description = value;
            }
        }
    }
}
