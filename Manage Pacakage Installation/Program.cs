using System;
using System.Diagnostics.PerformanceData;
using System.IO;

namespace Manage_Package_Installation
{ 
    static class Program
    {      
        static void Main()
        {
            string str = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "SamplLib\\SampleLib1");
            Console.WriteLine(System.Reflection.Assembly.GetExecutingAssembly().Location);
            Console.WriteLine(str);
            Console.ReadLine();
        }
    }
}
