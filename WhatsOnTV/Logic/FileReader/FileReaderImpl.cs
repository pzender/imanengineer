using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Logic
{
    public class FileReaderImpl : IFileReader
    {
        const string PROJECT_DIRECTORY_NAME = "WhatsOnTV";

        public string ReadFile(string filename)
        {
            string path = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory);

            if (path.Contains(PROJECT_DIRECTORY_NAME))
            {
                path = Path.Combine(path.Substring(0, path.LastIndexOf(PROJECT_DIRECTORY_NAME)), PROJECT_DIRECTORY_NAME, filename);
                return string.Join(" ", File.ReadAllLines(path));
            }
            else throw new DirectoryNotFoundException($"directory {PROJECT_DIRECTORY_NAME} not found in path {path}");
        }
    }
}
