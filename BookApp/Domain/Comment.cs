using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Comment
    {
        public int CommentId { get; set; }
        [Required] public string CommentBody { get; set; } = default!;
        [Required] public string CommentAuthor { get; set; } = default!;
        public int BookId { get; set; }
        public Book? Book { get; set; } = default!;
    }
}