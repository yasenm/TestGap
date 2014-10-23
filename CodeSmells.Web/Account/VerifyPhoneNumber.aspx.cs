namespace CodeSmells.Web.Account
{
    using System;
    using System.Web;
    using System.Web.UI;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Models;

    public partial class VerifyPhoneNumber : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var manager = this.Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            string phonenumber = this.Request.QueryString["PhoneNumber"];
            string code = manager.GenerateChangePhoneNumberToken(this.User.Identity.GetUserId(), phonenumber);
            this.PhoneNumber.Value = phonenumber;
        }

        protected void Code_Click(object sender, EventArgs e)
        {
            if(!this.ModelState.IsValid)
            {
                this.ModelState.AddModelError("", "Invalid code");
                return;
            }

            var manager = this.Context.GetOwinContext().GetUserManager<ApplicationUserManager>();

            IdentityResult result = manager.ChangePhoneNumber(this.User.Identity.GetUserId(), this.PhoneNumber.Value,
                this.Code.Text);

            if(result.Succeeded)
            {
                User user = manager.FindById(this.User.Identity.GetUserId());

                if(user != null)
                {
                    IdentityHelper.SignIn(manager, user, false);
                    this.Response.Redirect("/Account/Manage?m=AddPhoneNumberSuccess");
                }
            }

            // If we got this far, something failed, redisplay form
            this.ModelState.AddModelError("", "Failed to verify phone");
        }
    }
}