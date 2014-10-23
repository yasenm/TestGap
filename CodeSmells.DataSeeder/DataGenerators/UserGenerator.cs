namespace CodeSmells.DataSeeder.DataGenerators
{
    using CodeSmells.Models;
    using CodeSmells.Data;
    
    public class UserGenerator:IDataGenerator
    {
        private ICodeSmellsData data;

        public UserGenerator(ICodeSmellsData data) 
        {
            this.data = data;
        }

        //TODO: Refactor

        public void Generate()
        {
            //Extract usernames and other info out of the generator!
            string[] usernames = new string[] 
            {
                "caesaro",
                "beta",
                "omega",
                "codeLover",
                "honeydew",
                "lalna",
                "epicMouseOver"
            };
            //
            foreach (string username in usernames)
            {
                data.Users.Add(new User(username));
            }
            //
            data.SaveChanges();
        }
    }
}
