namespace CodeSmells.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<CodeSmellsDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(CodeSmellsDbContext context)
        {
            if(!context.Roles.Any(r => r.Name == UserRoleNames.Administrator))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole {Name = UserRoleNames.Administrator};

                manager.Create(role);
            }

            if(!context.Roles.Any(r => r.Name == UserRoleNames.Banned))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole {Name = UserRoleNames.Banned};

                manager.Create(role);
            }

            if(!context.Users.Any(u => u.Email == "admin@codesmells.com"))
            {
                var store = new UserStore<User>(context);
                var manager = new UserManager<User>(store);
                var user = new User {UserName = "admin@codesmells.com", Email = "admin@codesmells.com"};

                manager.Create(user, "123456");
                manager.AddToRole(user.Id, UserRoleNames.Administrator);
            }

            if(!context.Users.Any(u => u.Email == "user@codesmells.com"))
            {
                var store = new UserStore<User>(context);
                var manager = new UserManager<User>(store);
                var user = new User {UserName = "user@codesmells.com", Email = "user@codesmells.com"};

                manager.Create(user, "123456");
            }
        }
    }
}