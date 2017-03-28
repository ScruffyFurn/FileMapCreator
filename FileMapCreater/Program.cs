using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileMapCreater
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"FileMap";

            Console.WriteLine("Enter the parent folder path: ");
            string location = Console.ReadLine();

            // Open the stream and write to it.
            using (FileStream fs = File.OpenWrite(path))
            {
                string writeString;
                //Create String of files and paths
                string[] filePaths = Directory.GetFiles(location, "*.*", SearchOption.AllDirectories);

                //Add the "files" header
                writeString = "[Files] \n";
                Byte[] header = new UTF8Encoding(true).GetBytes(writeString);
                // Add some information to the file.
                fs.Write(header, 0, header.Length);

                foreach (var paths in filePaths)
                {
                    writeString = "\"" + paths + "\"" + "\t" + "\"" +
                                  paths.Replace(location, string.Empty) + "\"" + "\n";
                    Byte[] info = new UTF8Encoding(true).GetBytes(writeString);
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                    
                    Console.Write(writeString);
                }

                Console.WriteLine("Filemap created successfully, press any key to close ");
                Console.ReadKey();   
            }
        }
    }
}
