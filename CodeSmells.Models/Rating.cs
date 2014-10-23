namespace CodeSmells.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Rating
    {
        [Key]
        public int RatingId { get; set; }

        public RatingType Type { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        [ForeignKey("Post")]
        public int PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}