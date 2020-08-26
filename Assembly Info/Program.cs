using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Assembly_Info
{
    class Program
    {
        static void Main(string[] args)
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            try
            {
                Console.WriteLine(FileVersionInfo.GetVersionInfo("C:\\Program Files (x86)\\VB\\Voicemeeter\\voicemeeter.exe").FileVersion);
                Console.WriteLine(FileVersionInfo.GetVersionInfo("C:\\Program Files (x86)\\GOG Galaxy\\unins000.exe").FileVersion);
                Console.WriteLine(FileVersionInfo.GetVersionInfo("C:\\Program Files\\TruePianos\\TruePianos.exe").FileVersion);
                Console.WriteLine(FileVersionInfo.GetVersionInfo("C:\\Program Files (x86)\\Wondershare\\Wondershare Filmora (CPC)\\unins000.exe").FileVersion);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            watch.Stop();
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");

            Console.WriteLine("Program End");
            Console.ReadLine();
        }
    }
}
