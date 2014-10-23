namespace CodeSmells.Web.Administration
{
    using System;
    using System.Linq;
    using Models;

    public partial class Users : BasePage
    {
        protected void Page_PreRender(object sender, EventArgs e)
        {
            this.GridViewUsers.DataBind();
        }

        public IQueryable<User> GridViewUsers_GetData(object sender, EventArgs e)
        {
            var users = this.Data.Users
                .All()
                .Where(u => u.UserName != this.User.Identity.Name)
                .OrderBy(q => q.Id);

            return users;
        }

        protected void OnSearchButton_Click(object sender, EventArgs e)
        {
            var searchTerm = this.TbSearch.Text;
        }

        protected void ResizePages(object sender, EventArgs e)
        {
            var pageSize = this.DdlPageSize.SelectedValue;
            this.GridViewUsers.PageSize = int.Parse(pageSize);
        }
    }
}