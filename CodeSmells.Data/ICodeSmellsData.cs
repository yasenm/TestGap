namespace CodeSmells.Data
{
    using Models;
    using Repositories;

    public interface ICodeSmellsData
    {
        IRepository<User> Users { get; }

        IRepository<Post> Posts { get; }

        IRepository<Comment> Comments { get; }

        IRepository<Rating> Ratings { get; }

        void SaveChanges();
    }
}