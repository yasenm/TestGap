using System.Data.Entity;

namespace CodeSmells.Web.Posts
{
    using System;
    using System.Linq;
    using Models;
    using System.Web;

    public partial class GetPosts : BasePage
    {
        public string[] collection = {"a", "b", "c", "d"};
        public Post test;

        //public IQueryable<Post> GetAllPosts()
        //{
        //    var query = this.Data.Posts.All();
        //    return query;
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            this.DataBind();
        }

        public IQueryable<Post> GetAllPosts()
        {
            var chosenCategory = Request.Params["category"];

            var query = this.Data.Posts.All();

            if (chosenCategory != null)
            {
                query = this.Data.Posts.All().Where(p => p.Category.ToString().ToLower() == chosenCategory.ToLower());
            }

            return query;
        }
    }
}