namespace CodeSmells.Web.Account
{
    using System;
    using System.Web;
    using System.Web.UI;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Models;

    public partial class ManagePassword : Page
    {
        protected string SuccessMessage { get; private set; }

        private bool HasPassword(ApplicationUserManager manager)
        {
            return manager.HasPassword(this.User.Identity.GetUserId());
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var manager = this.Context.GetOwinContext().GetUserManager<ApplicationUserManager>();

            if(!this.IsPostBack)
            {
                // Determine the sections to render
                if(this.HasPassword(manager))
                {
                    this.changePasswordHolder.Visible = true;
                }
                else
                {
                    this.setPassword.Visible = true;
                    this.changePasswordHolder.Visible = false;
                }

                // Render success message
                string message = this.Request.QueryString["m"];
                if(message != null)
                {
                    // Strip the query string from action
                    this.Form.Action = this.ResolveUrl("~/Account/Manage");
                }
            }
        }

        protected void ChangePassword_Click(object sender, EventArgs e)
        {
            if(this.IsValid)
            {
                var manager = this.Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                IdentityResult result = manager.ChangePassword(this.User.Identity.GetUserId(), this.CurrentPassword.Text,
                    this.NewPassword.Text);
                if(result.Succeeded)
                {
                    User user = manager.FindById(this.User.Identity.GetUserId());
                    IdentityHelper.SignIn(manager, user, false);
                    this.Response.Redirect("~/Account/Manage?m=ChangePwdSuccess");
                }
                else
                {
                    this.AddErrors(result);
                }
            }
        }

        protected void SetPassword_Click(object sender, EventArgs e)
        {
            if(this.IsValid)
            {
                // Create the local login info and link the local account to the user
                var manager = this.Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                IdentityResult result = manager.AddPassword(this.User.Identity.GetUserId(), this.password.Text);
                if(result.Succeeded)
                {
                    this.Response.Redirect("~/Account/Manage?m=SetPwdSuccess");
                }
                else
                {
                    this.AddErrors(result);
                }
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach(string error in result.Errors)
            {
                this.ModelState.AddModelError("", error);
            }
        }
    }
}