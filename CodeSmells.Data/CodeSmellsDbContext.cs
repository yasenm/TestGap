namespace CodeSmells.Data
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Migrations;
    using Models;

    public class CodeSmellsDbContext : IdentityDbContext<User>
    {
        public CodeSmellsDbContext()
            : base("CodeSmellsDb", false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CodeSmellsDbContext, Configuration>());
        }

        public IDbSet<Post> Posts { get; set; }

        public IDbSet<Rating> Ratings { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        public static CodeSmellsDbContext Create()
        {
            return new CodeSmellsDbContext();
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
    }
}