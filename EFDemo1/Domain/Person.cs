using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Person
    {
        public int PersonId { get; set; }
        [Required] public string FirstName { get; set; } = default!;
        [Required] public string LastName { get; set; } = default!;

        public ICollection<Contact>? Contacts { get; set; }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}