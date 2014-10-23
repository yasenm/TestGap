namespace CodeSmells.Models
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser
    {
        public User(string userName = "") : base(userName)
        {
            this.Posts = new HashSet<Post>();
            this.Ratings = new HashSet<Rating>();
        }

        public User()
        {
            this.Posts = new HashSet<Post>();
            this.Ratings = new HashSet<Rating>();
        }

        public string ProfileImage { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }

        public ClaimsIdentity GenerateUserIdentity(UserManager<User> manager)
        {
            ClaimsIdentity userIdentity = manager.CreateIdentity(this, DefaultAuthenticationTypes.ApplicationCookie);

            return userIdentity;
        }

        public Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            return Task.FromResult(this.GenerateUserIdentity(manager));
        }
    }
}