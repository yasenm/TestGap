namespace CodeSmells.DataSeeder
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;
    using FileSniffing;
    using CodeSmells.Models;
    using CodeSmells.Data;
    using CodeSmells.DataSeeder.DataGenerators;

    class EntryPoint
    {                       
        private static Random randomProvider = new Random();

        private static Dictionary<string, List<string>> snippets = new Dictionary<string, List<string>>();

        private static CodeSmellsData codeSmellsData = new CodeSmellsData();

        static void Main(string[] args)
        {
            try
            {
                //set some parameters
                string[] smellyDirs = new string[]{
                     @"E:\xampp\htdocs\IdeaStar\",
                     @"E:\GitHub\personal\personal\TelerikAcademy\AspNetWebPages"
                };
                //
                string[] allowedExtensions = new string[] { "*.php", "*.js", "*.html", "*.cs", "*.css" };
                string[] categories = new string[] { "PHP", "JS", "HTML", "CS", "CSS" };
                //
                string fileNamesDumpFile = "smellyFiles.txt";
                string snippetsDumpFile = "snippets.txt";
                string postTitlesFile = "postTitles.txt";
                //usernames to generate
                string[] usernamesForUsersToInsert = new string[] {
                    "lalna",
                    "honeydew",
                    "xephos",
                    "sips",
                    "sjin",
                    "kjoravia"
                };
                //start work
                Console.WriteLine("CodeSmells data seeder");
                Console.WriteLine("- - -");
                //First: Sniff for code files.
                //SniffForCodeFiles(smellyDirs,allowedExtensions,fileNamesDumpFile);
                //Second: Extract code snippets.
                //ExtractSnippetsFromFiles(fileNamesDumpFile,snippetsDumpFile);
                //Third: Generate some users.
                //GenerateFakeUsers(usernamesForUsersToInsert);
                //Forth: Get required info and generate posts
                GenerateFakePosts(snippetsDumpFile,postTitlesFile,categories);
            }
            catch (Exception boom)
            {
                Console.WriteLine("[!] Ups, something went wrong! " + boom.Message);
            }
        }

        static void SniffForCodeFiles(string[] smellyDirs, string[] allowedExtensions,string dumpFile,int maxFileLength=100)
        {
            CodeFileSniffer fileSniffer = new CodeFileSniffer(smellyDirs, allowedExtensions, dumpFile);
            fileSniffer.MaxFileName = maxFileLength;
            Console.WriteLine("Started file sniffing...");
            fileSniffer.Sniff();
            Console.WriteLine("File sniffing finished!");
        }

        static void ExtractSnippetsFromFiles(string filenamesSource, string snippetsDumpFile,int snippetMinLength=100)
        {
            SnippetExtractor snippetExtractor = new SnippetExtractor(filenamesSource, snippetsDumpFile, randomProvider);
            Console.WriteLine("Started extracting snippets...");
            snippetExtractor.SnippetMinLength = snippetMinLength;
            snippetExtractor.ExtractSnippets();
            Console.WriteLine("Snippet extracting finished!");
        }

        static void GenerateFakeUsers(string[] usernamesForUsersToInsert)
        {
            Console.WriteLine("Generating fake users...");
            UsersGenerator userGenerator = new UsersGenerator(codeSmellsData);
            //
            userGenerator.UsernamesToInsert = usernamesForUsersToInsert;
            //
            userGenerator.Generate();
            Console.WriteLine("Fake users generated!");
        }

        static Category SeederToModelCategory(string seederCateg)
        {
            Category result = Category.ActionScript;
            switch (seederCateg.ToLower())
            {
                case "php":
                    result = Category.PHP;
                    break;
                case "js":
                    result = Category.JavaScript;
                    break;
                case "html":
                    result = Category.HTML;
                    break;
                case "cs":
                    result = Category.CSharp;
                    break;
                case "css":
                    result = Category.CSS;
                    break;
                default:
                    result = Category.ActionScript;
                    break;
            }
            return result;
        }

        static void GenerateFakePosts(string snippetsDumpFile, string postTitlesFile, string[] categories)
        {
            List<Post> posts=new List<Post>();
            //load snippets
            Console.WriteLine("Generating fake posts...");
            Console.WriteLine("->loading snippets");
            foreach (string categ in categories)
            {
                snippets.Add(categ.ToLower(), new List<string>());
            }

            using (StreamReader snippetsReader = new StreamReader(snippetsDumpFile))
            {
                string line = "";
                //assign language to post title
                string currentKey = "";
                string currentSnippet = "";
                while ((line = snippetsReader.ReadLine()) != null)
                {
                    string trimmedLine = line.Trim();
                    if (snippets.ContainsKey(trimmedLine))
                    {
                        if (currentKey != "" && currentSnippet != "")
                        {
                            snippets[currentKey].Add(currentSnippet);
                            currentKey = "";
                            currentSnippet = "";
                        }
                        //
                        currentKey = trimmedLine;
                    }
                    else if (currentKey != "")
                    {
                        currentSnippet += trimmedLine;
                    }
                }
            }
            //assign snippets to post titles
            //load users and assign random user to a post
            Console.WriteLine("->selecting users from db");
            User[] users = codeSmellsData.Users.All().ToArray();
            //load post titles
            Console.WriteLine("->generating posts");
            using (StreamReader postTitlesReader = new StreamReader(postTitlesFile))
            {
                string phrase = "";
                //assign language to post title
                while ((phrase = postTitlesReader.ReadLine()) != null)
                {
                    if (phrase != "")
                    {
                        string category = categories[randomProvider.Next(0, categories.Length - 1)];
                        string title = String.Format(phrase, category);
                        string catToLower = category.ToLower();
                        if (snippets[catToLower].Count > 0)
                        {
                            string snippet = snippets[catToLower][randomProvider.Next(0, snippets[catToLower].Count - 1)];
                            //create post objects 
                            posts.Add(new Post()
                            {
                                Title = title,
                                AuthorId = users[randomProvider.Next(0, users.Length - 1)].Id,
                                Body = snippet,
                                Category = SeederToModelCategory(category)
                            });
                            //remove snippet, to avoid doubles
                            snippets[catToLower].Remove(snippet);
                        }
                    }
                }
            }
            //save post objects
            Console.WriteLine("->saving posts");
            PostsGenerator postsGenerator = new PostsGenerator(codeSmellsData);
            postsGenerator.PostsToInsert = posts.ToArray();
            postsGenerator.Generate();
            Console.WriteLine("Fake posts generated!");
        }
    }
}
