using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Author
    {
        public int AuthorId { get; set; }
        [Display(Name="Given name", Prompt = "Enter the given name here...")]
        [MaxLength(128, ErrorMessage = "Max length for {0} is {1}")]
        [MinLength(1, ErrorMessage = "Min length for {0} is {1}")]
        public string FirstName { get; set; } = default!;
        [Display(Name="Family name", Prompt = "Enter the family name here...")]
        [MaxLength(128, ErrorMessage = "Max length for {0} is {1}")]
        public string? LastName { get; set; }
        [Display(Name="Birth date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Birth { get; set; }
        [Display(Name="Death date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Death { get; set; }

        public ICollection<Book>? Books { get; set; }

        [Display(Name="Full name")]
        public string FirstLastName => FirstName + " " + LastName;
        [Display(Name="Full name")]
        public string LastFirstName => LastName + " " + FirstName;
    }
}