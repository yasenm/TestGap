namespace CodeSmells.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Post
    {
        public Post()
        {
            this.Ratings = new HashSet<Rating>();
            this.Comments = new HashSet<Comment>();
        }

        [Key]
        public int PostId { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime DateCreated { get; set; }

        public Category Category { get; set; }

        [ForeignKey("Author")]
        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public int Rating
        {
            //get { return 1; }
            get { return this.Ratings.Sum(r => (int)r.Type); }
        }
    }
}