namespace CodeSmells.DataSeeder.DataGenerators
{
    using CodeSmells.Models;
    using CodeSmells.Data;

    public class PostsGenerator : DataGenerator
    {
        public PostsGenerator(ICodeSmellsData data) : base(data) { }

        public Post[] PostsToInsert { get; set; }

        public override void Generate()
        {
            foreach (Post post in this.PostsToInsert)
            {
                this.data.Posts.Add(post);
            }
            //
            this.data.Posts.SaveChanges();
        }
    }
}
