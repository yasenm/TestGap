namespace CodeSmells.Data
{
    using System;
    using System.Collections.Generic;
    using Models;
    using Repositories;

    public class CodeSmellsData : ICodeSmellsData
    {
        private readonly CodeSmellsDbContext context;
        private readonly IDictionary<Type, object> repositories;

        public CodeSmellsData()
            : this(new CodeSmellsDbContext())
        {
        }

        public CodeSmellsData(CodeSmellsDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<User> Users
        {
            get { return this.GetRepository<User>(); }
        }

        public IRepository<Post> Posts
        {
            get { return this.GetRepository<Post>(); }
        }

        public IRepository<Comment> Comments
        {
            get { return this.GetRepository<Comment>(); }
        }

        public IRepository<Rating> Ratings
        {
            get { return this.GetRepository<Rating>(); }
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            Type typeOfRepository = typeof(T);

            if(!this.repositories.ContainsKey(typeOfRepository))
            {
                object newRepository = Activator.CreateInstance(typeof(Repository<T>), this.context);
                this.repositories.Add(typeOfRepository, newRepository);
            }

            return (IRepository<T>)this.repositories[typeOfRepository];
        }
    }
}