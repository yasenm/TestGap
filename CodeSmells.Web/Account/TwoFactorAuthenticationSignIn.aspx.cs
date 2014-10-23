namespace CodeSmells.Web.Account
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Models;

    public partial class TwoFactorAuthenticationSignIn : Page
    {
        private readonly ApplicationUserManager manager;
        private readonly ApplicationSignInManager signinManager;

        public TwoFactorAuthenticationSignIn()
        {
            this.manager = this.Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            this.signinManager = this.Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string userId = this.signinManager.GetVerifiedUserId();
            if(userId == null)
            {
                this.Response.Redirect("/Account/Error", true);
            }
            IList<string> userFactors = this.manager.GetValidTwoFactorProviders(userId);
            this.Providers.DataSource = userFactors.Select(x => x).ToList();
            this.Providers.DataBind();
        }

        protected void CodeSubmit_Click(object sender, EventArgs e)
        {
            bool rememberMe = false;
            bool.TryParse(this.Request.QueryString["RememberMe"], out rememberMe);

            SignInStatus result = this.signinManager.TwoFactorSignIn(this.SelectedProvider.Value, this.Code.Text,
                rememberMe, this.RememberBrowser.Checked);
            switch(result)
            {
                case SignInStatus.Success:
                    IdentityHelper.RedirectToReturnUrl(this.Request.QueryString["ReturnUrl"], this.Response);
                    break;
                case SignInStatus.LockedOut:
                    this.Response.Redirect("/Account/Lockout");
                    break;
                case SignInStatus.Failure:
                default:
                    this.FailureText.Text = "Invalid code";
                    this.ErrorMessage.Visible = true;
                    break;
            }
        }

        protected void ProviderSubmit_Click(object sender, EventArgs e)
        {
            if(!this.signinManager.SendTwoFactorCode(this.Providers.SelectedValue))
            {
                this.Response.Redirect("/Account/Error");
            }

            User user = this.manager.FindById(this.signinManager.GetVerifiedUserId());
            if(user != null)
            {
                string code = this.manager.GenerateTwoFactorToken(user.Id, this.Providers.SelectedValue);
            }

            this.SelectedProvider.Value = this.Providers.SelectedValue;
            this.sendcode.Visible = false;
            this.verifycode.Visible = true;
        }
    }
}