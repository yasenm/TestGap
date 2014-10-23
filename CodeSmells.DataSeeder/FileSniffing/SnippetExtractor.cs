namespace CodeSmells.DataSeeder.FileSniffing
{
    using System;
    using System.IO;
    using System.Text;

    public class SnippetExtractor
    {
        public string FilenamesDumpFile { get; private set; }

        public string SnippetDumpFile { get; private set; }

        public int SnippetMinLength { get; set; }

        private StringBuilder snippetAccum = new StringBuilder();

        private Random randomProvider;

        public SnippetExtractor(string filenamesFile, string snippetDumpFile, Random randomProvider)
        {
            this.FilenamesDumpFile = filenamesFile;
            this.SnippetDumpFile = snippetDumpFile;
            this.randomProvider = randomProvider;
        }

        public void ExtractSnippets()
        {
            try
            {
                using (StreamReader filenamesDumpFile = new StreamReader(FilenamesDumpFile))
                {
                    string smellyFile;
                    // Read and display lines from the file until the end of  
                    // the file is reached. 
                    while ((smellyFile = filenamesDumpFile.ReadLine()) != null)
                    {
                        string[] smellyFileExploded = smellyFile.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                        string extension = "file";
                        if (smellyFileExploded.Length > 0)
                        {
                            extension = smellyFileExploded[smellyFileExploded.Length - 1];
                        }
                        //
                        string snippet = this.ExtractSnippetFromFile(smellyFile);
                        //
                        if (this.SnippetMinLength > 0 && this.SnippetMinLength < snippet.Length)
                        {
                            this.snippetAccum.AppendLine();
                            this.snippetAccum.AppendLine(extension);
                            this.snippetAccum.AppendLine(snippet);
                        }
                    }
                }
                //write result
                using (StreamWriter snippetWriter = new StreamWriter(this.SnippetDumpFile))
                {
                    snippetWriter.Write(this.snippetAccum.ToString());
                }

            }
            catch (IOException IOboom)
            {
                throw new Exception("An IO error occurred. " + IOboom.Message);
            }
            catch (Exception boom)
            {
                throw new Exception("A generic error occured. " + boom.Message);
            }
        }

        public string ExtractSnippetFromFile(string filename)
        {
            string result = "";
            string smellyFile = "";
            using (StreamReader smellyFileReader = new StreamReader(filename))
            {
                smellyFile = smellyFileReader.ReadToEnd();
            }
            //
            if (smellyFile.Length > 3)
            {
                int start = this.randomProvider.Next(0, smellyFile.Length - 2);
                int end = this.randomProvider.Next(start, smellyFile.Length - 1);
                result = smellyFile.Substring(start, end-start);               
            }
            //            
            return result;
        }
    }
}
