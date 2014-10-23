namespace CodeSmells.DataSeeder.FileSniffing
{
    using System;
    using System.IO;
    using System.Text;

    public class CodeFileSniffer
    {
        public string[] DirectoriesToSniff { get; private set; }

        public string[] AllowedExtensions { get; private set; }

        public int MaxFileName { get; set; }

        public string OutputFile { get; private set; }

        public CodeFileSniffer(string[] directoriesToSniff, string[] allowedExtensions, string outputFile)
        {
            this.DirectoriesToSniff = directoriesToSniff;
            this.OutputFile = outputFile;
            this.AllowedExtensions = allowedExtensions;
        }

        private StringBuilder gatheredFiles = new StringBuilder();

        public void Sniff()
        {
            using (StreamWriter outputWriter = new StreamWriter(this.OutputFile))
            {
                foreach (string directory in this.DirectoriesToSniff)
                {
                    this.SniffDirectory(directory);
                }
                outputWriter.Write(this.gatheredFiles.ToString());
            }
        }

        private void SniffDirectory(string directory)
        {
            try
            {
                //string[] subDirs = Directory.GetDirectories(directory);
                foreach (string extension in AllowedExtensions)
                {
                    string[] files = Directory.GetFiles(directory, extension, SearchOption.AllDirectories);
                    foreach (string file in files)
                    {
                        if (this.MaxFileName > 0 && file.Length <= this.MaxFileName)
                        {
                            gatheredFiles.AppendLine(file);
                        }
                    }
                }
            }
            catch (IOException IOboom)
            {
                throw new Exception("An error occured when trying to read directory! " + IOboom.Message);
            }
            catch (Exception boom)
            {
                throw new Exception("A generic error occurred! " + boom.Message);
            }
        }
    }
}
