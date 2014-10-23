namespace CodeSmells.Web
{
    using CodeSmells.Models;
    using CodeSmells.Web.Posts;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public partial class _Default : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.PostsCategories.DataSource = this.GetCategories;
            this.LatestPosts.DataSource = this.GetLatestPosts();
            this.DataBind();
        }

        public IEnumerable<Post> GetLatestPosts()
        {
            var result = this.Data.Posts
                                  .All()
                                  .OrderByDescending(p => p.DateCreated)
                                  .Take(10)
                                  .ToList();

            return result;
        }

        public ICollection<CategoryModel> GetCategories
        {
            get
            {
                var categories = Enumeration.GetAll<Category>();
                var result = new List<CategoryModel>();

                foreach (var category in categories)
                {
                    var newCat = new CategoryModel() { Name = category.Value };
                    result.Add(newCat);
                }

                return result;
            }
        }
    }
}