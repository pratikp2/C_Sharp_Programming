using System;
using System.IO;

namespace Test
{
   class Program
    {
        static void Main(string[] args)
        {
            try
            {
      
                FileInfo info = new FileInfo("D:/Practice/Git_Repos/Windows_Programming/Utils/Sample_Text");
                Console.WriteLine("Access Credentials : {0}", (System.Security.Principal.WindowsIdentity.GetCurrent()).Name);

                using (FileStream fileStream = info.OpenRead())
                using (StreamReader fileReader = new StreamReader(fileStream))
                {
                    fileReader.ReadToEnd();
                }
                Console.WriteLine("Data Read Successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Data while REading Data");
                Console.WriteLine(ex);
            }

            Console.ReadLine();
        }
    }
}
