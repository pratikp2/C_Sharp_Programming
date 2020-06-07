using System;
using System.Diagnostics;
using System.IO;
using System.Management;

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

        static void InstallDriver()
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
        }

        public static void showDriversList()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPSignedDriver");
            foreach (ManagementObject obj in searcher.Get())
            {
                if (obj.GetPropertyValue("DeviceName") == null)
                    continue;

                string s = string.IsNullOrEmpty(obj.GetPropertyValue("DeviceName").ToString()) ?
                    string.Empty : obj.GetPropertyValue("DeviceName").ToString();
                Console.WriteLine(s);
            }
        }

        static void Main()
        {
            showDriversList();
            //InstallDriver();
            Console.ReadLine();
        }
    }
}


/*
 class Win32_PnPSignedDriver : CIM_Service
{
  string   ClassGuid;
  string   CompatID;
  string   Description;
  string   DeviceClass;
  string   DeviceID;
  string   DeviceName;
  string   DevLoader;
  string   DriverDate;
  string   DriverName;
  string   DriverVersion;
  string   FriendlyName;
  string   HardWareID;
  string   InfName;
  datetime InstallDate;
  boolean  IsSigned;
  string   Location;
  string   Manufacturer;
  string   Name;
  string   PDO;
  string   DriverProviderName;
  string   Signer;
  boolean  Started;
  string   StartMode;
  string   Status;
  string   SystemCreationClassName;
  string   SystemName;
};
 */
