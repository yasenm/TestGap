namespace CodeSmells.Web.Public
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Models;

    public partial class ProfileDetails : BasePage
    {
        public User CurrentUser
        {
            get
            {
                return this.GetUserById();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.CurrentUser.ProfileImage))
            {
                this.AvatarContainer.ImageUrl = ("~/Uploads/Images/" + this.CurrentUser.ProfileImage);
            }
            else
            {
                this.AvatarContainer.ImageUrl = ("~/Uploads/Images/default.jpg");
            }
            this.Page.DataBind();
        }


        private User GetUserById()
        {
            if (Request.Params["id"] == null)
            {
                Response.Redirect("Home.aspx");
            }

            var itemId = Request.Params["id"];
            var user = this.Data.Users.Find(itemId);
            return user;
        }

        public IQueryable<Post> GetUserPosts()
        {
            return this.CurrentUser.Posts.AsQueryable();
        }

        public int GetUserRatings
        {
            get
            {
                int ratingSum = 0;

                foreach (var item in this.GetUserPosts())
                {
                    ratingSum += item.Rating;
                }

                return ratingSum;
            }
        }
    }
}