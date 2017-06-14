using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FileMapCreater
{
    public class PathEntry
    {
        public string SourcePath { get; set; }
        public string DestPath { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"FileMap";

            var pathList = new List<PathEntry>();

            string sourcePath = "Start";

            while (sourcePath != "")
            {
                Console.WriteLine("Enter the source folder path (or hit enter to continue): ");
                sourcePath = Console.ReadLine();
                if (sourcePath == "")
                    break;
                Console.WriteLine("Enter enter destination path (default is the same as parent, hit enter): ");
                string destPath = Console.ReadLine();
                if (destPath == "")
                {
                    destPath = sourcePath;
                }
                PathEntry newPath = new PathEntry();
                newPath.SourcePath = sourcePath;
                newPath.DestPath = destPath;
                pathList.Add(newPath);
                Console.WriteLine("Path Added! ");
            }

            if (pathList.Any())
            {
                // Open the stream and write to it.
                using (FileStream fs = File.OpenWrite(path))
                {
                    string writeString;
                    //Add the "files" header
                    writeString = "[Files] \n";
                    Byte[] header = new UTF8Encoding(true).GetBytes(writeString);
                    // Add some information to the file.
                    fs.Write(header, 0, header.Length);

                    foreach (PathEntry filePath in pathList)
                    {

                        //Create String of files and paths
                        string[] filePaths = Directory.GetFiles(filePath.SourcePath, "*.*", SearchOption.AllDirectories);

                        if (filePath.DestPath == filePath.SourcePath)
                        {
                            foreach (var paths in filePaths)
                            {
                                writeString = "\"" + paths + "\"" + "\t" + "\"" +
                                              paths.Replace(filePath.SourcePath, string.Empty) + "\"" + "\n";
                                Byte[] info = new UTF8Encoding(true).GetBytes(writeString);
                                // Add some information to the file.
                                fs.Write(info, 0, info.Length);

                                Console.Write(writeString);
                            }
                        }
                        else
                        {
                            foreach (var paths in filePaths)
                            {
                                writeString = "\"" + paths + "\"" + "\t" + "\"" +
                                             paths.Replace(filePath.SourcePath, filePath.DestPath) + "\"" + "\n";
                                Byte[] info = new UTF8Encoding(true).GetBytes(writeString);
                                // Add some information to the file.
                                fs.Write(info, 0, info.Length);

                                Console.Write(writeString);
                            }
                        }
                    }
                }

                Console.WriteLine("Filemap created successfully, press any key to close ");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("No directory specified, press any key to close");
                Console.ReadKey();
            }
        }
    }
}
