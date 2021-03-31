using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class ContactType
    {
        public int ContactTypeId { get; set; }
        
        [Required] public string ContactTypeValue { get; set; } = default!;

        public ICollection<Contact>? Contacts { get; set; }

        public override string ToString()
        {
            return $"{ContactTypeId} - {ContactTypeValue} - Contacts: {(Contacts == null ? "null" : Contacts.Count)}";
        }
    }
}