using Microsoft.AspNet.Identity;

namespace CodeSmells.Web.Posts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using CodeSmells.Models;

    public partial class PostDetails : BasePage
    {
        private int itemId;

        public IQueryable<Post> GetPostById()
        {
            var query = this.Data.Posts.All().Where(x => x.PostId == itemId);
            return query;
        }

        public void AddComment()
        {
            var comment = new Comment();
            comment.AuthorId = this.User.Identity.GetUserId();
            
            this.TryUpdateModel(comment);
            if (this.ModelState.IsValid)
            {
                this.Data.Posts.Find(itemId).Comments.Add(comment);
                this.Data.SaveChanges();
            }
        }

        protected void AddCommentForm_ItemInserted(object sender, FormViewInsertedEventArgs e)
        {
            this.Response.Redirect("PostDetails.aspx?id=" + itemId);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["id"] == null)
            {
                Response.Redirect("GetPosts.aspx");
            }

            itemId = int.Parse(Request.Params["id"]);
            var query = this.GetPostById();

            if (query.First().Ratings.FirstOrDefault(r => r.UserId == this.User.Identity.GetUserId()) != null)
            {
                this.UpVoteButton.Enabled = false;
                this.DownVoteButton.Enabled = false;
            }

            this.CommentsRepeater.DataSource = query.First().Comments.ToList();
            this.CommentsRepeater.DataBind();
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("PostDetails.aspx?id=" + itemId);
        }

        protected void UpVoteButton_Click(object sender, EventArgs e)
        {
            this.RegisterVote(RatingType.Positive);
        }

        protected void DownVoteButton_Click(object sender, EventArgs e)
        {
            this.RegisterVote(RatingType.Negative);
        }

        private void RegisterVote(RatingType voteType)
        {
            var rating = new Rating();
            rating.PostId = itemId;
            rating.UserId = this.User.Identity.GetUserId();
            rating.Type = voteType;
            this.Data.Posts.Find(itemId).Ratings.Add(rating);
            this.Data.SaveChanges();
            this.Response.Redirect("PostDetails.aspx?id=" + itemId);
        }
    }
}