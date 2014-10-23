namespace CodeSmells.DataSeeder.DataGenerators
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNet.Identity.EntityFramework;
    using CodeSmells.Models;
    using CodeSmells.Data;

    public class UsersGenerator : DataGenerator
    {
        public string[] UsernamesToInsert { get; set; }

        public UsersGenerator(ICodeSmellsData data)
            : base(data){}        

        public override void Generate()
        {
            //add usernames
            foreach (string username in this.UsernamesToInsert)
            {
                data.Users.Add(new User() { UserName=username});
            }
            //
            data.Users.SaveChanges();
        }
    }
}
