namespace CodeSmells.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class Comment
    {
        public int CommentId { get; set; }

        public string Body { get; set; }

        [ForeignKey("Author")]
        public string AuthorId { get; set; }

        public virtual User Author { get; set; }
    }
}