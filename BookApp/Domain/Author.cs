using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Author
    {
        public int AuthorId { get; set; }
        [Required] public string FirstName { get; set; } = default!;
        public string? LastName { get; set; }
        public DateTime? Birth { get; set; }
        public DateTime? Death { get; set; }

        public ICollection<Book>? Books { get; set; }
    }
}