using System.Data.Entity;

namespace CodeSmells.Web.Posts
{
    using System;
    using System.Linq;
    using Models;

    public partial class GetPosts : BasePage
    {
        public string[] collection = {"a", "b", "c", "d"};
        public Post test;

        public IQueryable<Post> GetAllPosts()
        {
            var query = this.Data.Posts.All();
            return query;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Post[] posts = this.GetAllPosts().ToArray();
            this.test = posts[0];
        }
    }
}