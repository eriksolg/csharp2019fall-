using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Contact
    {
        public int ContactId { get; set; }

        [Required] public string ContactValue { get; set; } = default!;
        
        public int PersonId { get; set; }
        public Person Person { get; set; } = default!;

        public int ContactTypeId { get; set; }
        public ContactType ContactType { get; set; } = default!;

        public override string ToString()
        {
            return $"{ContactId} - {ContactValue}";
        }
    }
}