using System;
using System.IO;
using System.Reflection;

namespace Embed_Files_In_Binary
{
    static class Program
    {
        private static string[] TextFiles = { "File1.txt", "File2.txt", "SampleExe.exe", };
        private static string FileLocation = "EmbeddedFiles.";
        private static string Destination = @"C:\Utils\ExtractEmbeddedResource";

        private static bool ExtractEmbeddedResources()
        {
            bool status = true;
            foreach (string txtFile in TextFiles)
            {
                try
                {
                    Console.WriteLine(Assembly.GetExecutingAssembly().Location);
                    using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(FileLocation + txtFile))
                    {
                        SaveStreamToFile(Path.Combine(Destination, txtFile), stream);
                    }
                }
                catch (Exception ex) { Console.WriteLine("Exception thrown while extracting exe : {0}", ex.Message); }
            }
            return status;
        }

        public static void SaveStreamToFile(string fileFullPath, Stream stream)
        {
            if (stream.Length == 0) return;

            // Create a FileStream object to write a stream to a file
            using (FileStream fileStream = System.IO.File.Create(fileFullPath, (int)stream.Length))
            {
                // Fill the bytes[] array with the stream data
                byte[] bytesInStream = new byte[stream.Length];
                stream.Read(bytesInStream, 0, (int)bytesInStream.Length);

                // Use FileStream object to write to the specified file
                fileStream.Write(bytesInStream, 0, bytesInStream.Length);
            }
        }


        static int Main()
        {
            ExtractEmbeddedResources();

            Console.WriteLine("Program End");
            Console.ReadLine();
            return 0;
        }
    }
}
