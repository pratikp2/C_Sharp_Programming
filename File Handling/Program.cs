using System;
using System.Collections.Generic;
using System.IO;

namespace CollectionsData
{
    class Program
    {
        static public string RootDirectory = @"D:/Practice/Git_Repos/Windows_Programming";
        static public string InputFilePath = @"/Utils/data.txt";
        static public string BkupDirectory = @"backup";
        static public string path = @"../../../../Utils/data.txt";
        static public DirectoryInfo dirInfo = new DirectoryInfo(path);
        
        static void Main()
        {
            InputFilePath = Path.GetDirectoryName(path);
            RootDirectory = dirInfo.Parent.Parent.FullName;
            string BkupDirectoryPath = Path.Combine(InputFilePath, BkupDirectory);

            // Create Directory 
            Directory.CreateDirectory(BkupDirectoryPath);

            // Copy File 
            string bkupPath = Path.Combine(BkupDirectoryPath, path);
            Console.WriteLine(bkupPath);
            Console.ReadLine();
        }
    }

    class ProcessFile
    {
        static public bool CheckFile(string path)
        {
            try { return File.Exists(path); }
            catch(Exception Ex) { Console.WriteLine(Ex); }
            return false;
        }
    }
}

