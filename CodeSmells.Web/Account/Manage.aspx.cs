namespace CodeSmells.Web.Account
{
    using System;
    using System.Web;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using Models;

    public partial class Manage : BasePage
    {
        protected string SuccessMessage { get; private set; }

        public bool HasPhoneNumber { get; private set; }

        public bool TwoFactorEnabled { get; private set; }

        public bool TwoFactorBrowserRemembered { get; private set; }

        public int LoginsCount { get; set; }

        private bool HasPassword(ApplicationUserManager manager)
        {
            return manager.HasPassword(this.User.Identity.GetUserId());
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            var user = this.Data.Users.Find(this.User.Identity.GetUserId());

            this.TbUserName.Text = user.UserName;
            this.TbEmail.Text = user.Email;

            if(!string.IsNullOrEmpty(user.ProfileImage))
            {
                this.ImgProfileImage.ImageUrl = ("/Uploads/Images/" + user.ProfileImage);
            }
            else
            {
                this.ImgProfileImage.ImageUrl = ("/Uploads/Images/default.jpg");
            }
        }

        protected void Page_Load()
        {
            var manager = this.Context.GetOwinContext().GetUserManager<ApplicationUserManager>();

            this.HasPhoneNumber = String.IsNullOrEmpty(manager.GetPhoneNumber(this.User.Identity.GetUserId()));

            // Enable this after setting up two-factor authentientication
            //PhoneNumber.Text = manager.GetPhoneNumber(User.Identity.GetUserId()) ?? String.Empty;

            this.TwoFactorEnabled = manager.GetTwoFactorEnabled(this.User.Identity.GetUserId());

            this.LoginsCount = manager.GetLogins(this.User.Identity.GetUserId()).Count;

            IAuthenticationManager authenticationManager = HttpContext.Current.GetOwinContext().Authentication;

            if(!this.IsPostBack)
            {
                // Determine the sections to render
                if(this.HasPassword(manager))
                {
                    this.ChangePassword.Visible = true;
                }
                else
                {
                    this.CreatePassword.Visible = true;
                    this.ChangePassword.Visible = false;
                }

                // Render success message
                string message = this.Request.QueryString["m"];
                if(message != null)
                {
                    // Strip the query string from action
                    this.Form.Action = this.ResolveUrl("~/Account/Manage");

                    this.SuccessMessage =
                        message == "ChangePwdSuccess"
                            ? "Your password has been changed."
                            : message == "SetPwdSuccess"
                                ? "Your password has been set."
                                : message == "RemoveLoginSuccess"
                                    ? "The account was removed."
                                    : message == "AddPhoneNumberSuccess"
                                        ? "Phone number has been added"
                                        : message == "RemovePhoneNumberSuccess"
                                            ? "Phone number was removed"
                                            : String.Empty;
                    this.successMessage.Visible = !String.IsNullOrEmpty(this.SuccessMessage);
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

        // Remove phonenumber from user
        protected void RemovePhone_Click(object sender, EventArgs e)
        {
            var manager = this.Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            IdentityResult result = manager.SetPhoneNumber(this.User.Identity.GetUserId(), null);
            if(!result.Succeeded)
            {
                return;
            }
            User user = manager.FindById(this.User.Identity.GetUserId());
            if(user != null)
            {
                IdentityHelper.SignIn(manager, user, false);
                this.Response.Redirect("/Account/Manage?m=RemovePhoneNumberSuccess");
            }
        }

        // DisableTwoFactorAuthentication
        protected void TwoFactorDisable_Click(object sender, EventArgs e)
        {
            var manager = this.Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            manager.SetTwoFactorEnabled(this.User.Identity.GetUserId(), false);

            this.Response.Redirect("/Account/Manage");
        }

        //EnableTwoFactorAuthentication 
        protected void TwoFactorEnable_Click(object sender, EventArgs e)
        {
            var manager = this.Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            manager.SetTwoFactorEnabled(this.User.Identity.GetUserId(), true);

            this.Response.Redirect("/Account/Manage");
        }

        protected void LinkBtnSaveUser_Click(object sender, EventArgs e)
        {
            var user = this.Data.Users.Find(this.User.Identity.GetUserId());
            user.UserName = this.TbUserName.Text;
            user.Email = this.TbEmail.Text;

            this.Data.SaveChanges();
        }
    }
}