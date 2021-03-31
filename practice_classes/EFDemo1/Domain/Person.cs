using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Person
    {
        public int PersonId { get; set; }
        [Required]
        [MaxLength(255)]    
        public string FirstName { get; set; } = default!;
        [Required] public string LastName { get; set; } = default!;

        public ICollection<Contact>? Contacts { get; set; }

        public string FirstLastName => FirstName + " " + LastName;
        public string LastFirstName => LastName + " " + FirstName;
        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}