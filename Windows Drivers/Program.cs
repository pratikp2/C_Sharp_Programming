using System;
using System.Diagnostics;

namespace Driver_Installation
{
    static class Program
    {
        /*[DllImport("Setupapi.dll", EntryPoint = "InstallHinfSection", CallingConvention = CallingConvention.StdCall)]
        public static extern void InstallHinfSection(
            [In] IntPtr hwnd,
            [In] IntPtr ModuleHandle,
            [In, MarshalAs(UnmanagedType.LPWStr)] string CmdLineBuffer,
            int nCmdShow);*/

        static void Main()
        {
            try
            {
                string driverPath = "C:\\Utils\\hpanalyticscomp.inf";
                var process = new Process();
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.FileName = "C:\\Windows\\System32\\cmd.exe";
                string a = @"" + driverPath + "";
                process.StartInfo.Arguments = "/c C:\\Windows\\Sysnative\\PnPutil.exe -i -a \"" + driverPath + "\"";
                process.Start();
                process.WaitForExit();
                process.Dispose();
                Console.WriteLine(@"Driver has been installed");
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex.Message);
            }

            Console.ReadLine();
        }
    }
}
