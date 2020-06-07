using System;
using System.IO;
using System.Reflection;

namespace Embed_Files_In_Binary
{
    static class Program
    {
        public static void ExtractResource1(String destinationPath)
        {
            try
            {
                var currentAssembly = Assembly.GetExecutingAssembly();
                var arrResources = currentAssembly.GetManifestResourceNames();
                foreach (var resourceName in arrResources)
                {
                    using (var resourceToSave = currentAssembly.GetManifestResourceStream(resourceName))
                    {
                        using (var output = File.OpenWrite(destinationPath))
                            resourceToSave.CopyTo(output);
                        resourceToSave.Close();
                    }
                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine("Exception Occured : {0}", ex);
            }
        }

        public static void ExtractResource2()
        {
            try
            {
                var currentAssembly = Assembly.GetExecutingAssembly();
                var arrResources = currentAssembly.GetManifestResourceNames();
                string assemblyname = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
                foreach (var resourceName in arrResources)
                {
                    string filename = resourceName.ToString().Remove(0, assemblyname.Length + 1);                   
                    using (var resourceToSave = currentAssembly.GetManifestResourceStream(resourceName))
                    {
                        if (!File.Exists(filename))
                        {
                            FileStream fileStreams = new FileStream(filename, FileMode.CreateNew);
                            for (int i = 0; i < resourceToSave.Length; i++)
                                fileStreams.WriteByte((byte)resourceToSave.ReadByte());
                            fileStreams.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Occured : {0}", ex);
            }
        }


        static int Main()
        {
            //ExtractResource1(@"C:\Utils\EmbedInExe");
            //ExtractResource1(@"D:\Practice\Git_Repos\C# Programming\Embed Files in Binary\LocationDestination");
            ExtractResource2();

            Console.WriteLine(Environment.ExpandEnvironmentVariables("%ProgramW6432%"));
            Console.WriteLine(Environment.ExpandEnvironmentVariables("%ProgramFiles(x86)%"));

            Console.ReadLine();
            return 0;
        }
    }
}
