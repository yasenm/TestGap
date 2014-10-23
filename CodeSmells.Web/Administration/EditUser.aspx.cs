namespace CodeSmells.Web.Administration
{
    using System;
    using System.Web;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Models;

    public partial class EditUser : BasePage
    {
        private string userId;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.userId = this.Request.QueryString["userId"];
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            var user = this.Data.Users.Find(this.userId);

            this.TbUserName.Text = user.UserName;
            this.TbEmail.Text = user.Email;

            var manager = this.Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var roles = manager.GetRoles(this.userId);

            if(roles.Contains(UserRoleNames.Administrator))
            {
                this.BtnToggleAdminRole.Text = "Remove Admin Rights";
            }
            else if(roles.Contains(UserRoleNames.Banned))
            {
                this.BtnBanUser.Text = "Unban";
            }

            if(!string.IsNullOrEmpty(user.ProfileImage))
            {
                this.ImgProfileImage.ImageUrl = ("/Uploads/Images/" + user.ProfileImage);
            }
            else
            {
                this.ImgProfileImage.ImageUrl = ("/Uploads/Images/default.jpg");
            }
        }

        protected void BtnToggleAdminRole_Click(object sender, EventArgs e)
        {
            var manager = this.Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var roles = manager.GetRoles(this.userId);

            if(roles.Contains(UserRoleNames.Administrator))
            {
                manager.RemoveFromRole(this.userId, UserRoleNames.Administrator);
            }
            else
            {
                manager.AddToRole(this.userId, UserRoleNames.Administrator);
            }
            this.Data.SaveChanges();
            this.Response.Redirect(this.Request.RawUrl);
        }

        protected void BtnBanUser_Click(object sender, EventArgs e)
        {
            var manager = this.Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var roles = manager.GetRoles(this.userId);

            if(roles.Contains(UserRoleNames.Banned))
            {
                manager.RemoveFromRole(this.userId, UserRoleNames.Banned);
            }
            else
            {
                manager.AddToRole(this.userId, UserRoleNames.Banned);
            }
            this.Data.SaveChanges();
            this.Response.Redirect(this.Request.RawUrl);
        }

        protected void LinkBtnSaveUser_Click(object sender, EventArgs e)
        {
            var user = this.Data.Users.Find(this.userId);
            user.UserName = this.TbUserName.Text;
            user.Email = this.TbEmail.Text;

            this.Data.SaveChanges();
            this.Response.Redirect("~/Administration/Users", false);
        }
    }
}