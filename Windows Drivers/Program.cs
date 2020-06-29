using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;
using System.Runtime.InteropServices;
using Microsoft.Management.Infrastructure;

namespace Driver_Installation
{
    static class Program
    {
        [DllImport("Setupapi.dll", EntryPoint = "InstallHinfSection", CallingConvention = CallingConvention.StdCall)]
        public static extern void InstallHinfSection(
            [In] IntPtr hwnd,
            [In] IntPtr ModuleHandle,
            [In, MarshalAs(UnmanagedType.LPWStr)] string CmdLineBuffer,
            int nCmdShow);

        [DllImport("Kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "EnumDeviceDrivers", CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumDeviceDrivers(
            [param: MarshalAs(UnmanagedType.AsAny)]
            out IntPtr lpImageBase,
            [param: MarshalAs(UnmanagedType.U4)]
            uint cb,
            [param: MarshalAs(UnmanagedType.U4),Out()]
            out uint lpcbNeeded);

        static void retriveData()
        { 
            /*byte[] databuffer = new byte[1024];
            GCHandle handle = GCHandle.Alloc(databuffer);
            IntPtr ptr = (IntPtr)handle;
            uint cb;
            handle.Free();*/
        }
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
        static void UnInstallDriver()
        {
            try
            {
                string DriverName = GetIntendedDriver();
                if (DriverName == string.Empty)
                    throw new Exception("Driver Not Found. Uninstallation Faied ..!");

                var process = new Process();
                process.StartInfo.Verb = "runas";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.FileName = "C:\\Windows\\System32\\cmd.exe";
                process.StartInfo.Arguments = "/c C:\\Windows\\Sysnative\\PnPutil.exe /delete-driver " + DriverName + " /uninstall";

                process.Start();
                process.WaitForExit();

                if (process.ExitCode != 0)
                    throw new Exception("Null INF Driver Un installation failed");
               
                process.Dispose();
                Console.WriteLine(@"Driver has been Uninstalled");
            }
            catch (Exception ex){ Console.WriteLine("{0}", ex.Message);}
        }
        static bool UninstallProgram(string ProgramName)
        {
            try
            {
                ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT * FROM Win32_PnPSignedDriver WHERE Name = '" + ProgramName + "'");
                foreach (ManagementObject mo in mos.Get())
                {
                    try
                    {
                        if (mo["InfName"].ToString() == ProgramName)
                        {
                            object hr = mo.InvokeMethod("Uninstall", null);
                            return (bool)hr;
                        }
                    }
                    catch (Exception ex) { Console.WriteLine("Exception Thrown : {0}", ex.Message); }
                }
                Console.WriteLine("Driver Not Found");
                return false;
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Exception Thrown : {0}", ex.Message);
                return false;
            }
        }
        public static void showDriversList1()
        {
            //Win32_PnPSignedDriver

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
        public static void showDriversList2()
        {
            // WIN32_PNPEntity

            string txt = "SELECT * FROM win32_PNPEntity";

            ManagementObjectSearcher deviceSearch = new ManagementObjectSearcher("root\\CIMV2", txt);
            foreach (ManagementObject device in deviceSearch.Get())
            {
                try
                {
                    foreach (var item in device.Properties)
                        Console.WriteLine(item.Name + ": " + item.Value);

                    Console.WriteLine("HardwareIDs:");
                    foreach (string id in (string[])device["HardwareID"])
                        if (id != null)
                            Console.WriteLine(id);

                    Console.WriteLine("--------------------------------------------------------------------------------------------");
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
        }
        public static void showDriversList3()
        {
            // WIN32_PnPDeviceProperty
            try
            {
                SelectQuery query = new SelectQuery("SELECT * FROM meta_class WHERE __class = 'win32_PNPDeviceProperty'");
                //ObjectQuery query = new ObjectQuery("win32_PNPDeviceProperty.DeviceID='SWD\\DRIVERENUM\\{DBF3223D-8A64-4DAE-903E-E2FAD8D45BC8}#HPANALYTICSSOFTWARE&3&26699ED6&0'");
                //ObjectQuery query = new ObjectQuery("win32_PNPDeviceProperty");
                ManagementObjectSearcher deviceSearch = new ManagementObjectSearcher(query);
                foreach (ManagementObject device in deviceSearch.Get())
                {
                    Console.WriteLine("Driver ----------------------------------------");
                    foreach (var item in device.Properties)
                        Console.WriteLine("{0}      :   {1}", item.Name, item.Value);

                    Console.WriteLine();
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
        public static void showDriversList4()
        {
            try
            {
                CimSession cimSession = CimSession.Create("localhost");
                IEnumerable<CimInstance> enumeratedInstances = cimSession.EnumerateInstances(@"root\cimv2", "win32_PNPDeviceProperty");

                foreach (CimInstance cimInstance in enumeratedInstances) 
                   Console.WriteLine("{0}", cimInstance.CimInstanceProperties["Name"].Value.ToString());   
            }
            catch (CimException ex){Console.WriteLine(ex.Message);}
        }
        public static void CheckDriverInstallation()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPSignedDriver");
            foreach (ManagementObject obj in searcher.Get())
            {
                if (obj.GetPropertyValue("DeviceName") == null)
                    continue;

                if (obj.GetPropertyValue("Manufacturer") != null && obj.GetPropertyValue("Manufacturer").ToString().Contains("HP"))
                {
                    try
                    {
                        Console.WriteLine("Driver ----------------------------------------");
                        Console.WriteLine("Driver Name      : {0}", obj.GetPropertyValue("DeviceName").ToString());
                        Console.WriteLine("Inf Name         : {0}", obj.GetPropertyValue("InfName").ToString());
                        Console.WriteLine("Provider Name    : {0}", obj.GetPropertyValue("DriverProviderName").ToString());
                        Console.WriteLine("Manufacturer     : {0}", obj.GetPropertyValue("Manufacturer").ToString());
                        Console.WriteLine("Device Version   : {0}", obj.GetPropertyValue("DriverVersion").ToString());
                        Console.WriteLine("Driver Date      : {0}", obj.GetPropertyValue("DriverDate").ToString());
                        Console.WriteLine();
                    }
                    catch (Exception ex)
                    { Console.WriteLine("Exception Thrown : {0}", ex.Message); }
                }
            }
        }
        public static string GetIntendedDriver()
        {
            try
            {
                string DriverName = string.Empty;
                //SelectQuery selectQuery = new SelectQuery("SELECT * FROM Win32_PnPSignedDriver WHERE (HardWareID = '%HPAnalytics%')");
                //SelectQuery selectQuery = new SelectQuery("SELECT * FROM Win32_PnPSignedDriver WHERE (HardWareID = '%HPAnalytics%' OR HardWareID = '%HPA000C%')");
                SelectQuery selectQuery = new SelectQuery("SELECT * FROM Win32_PnPSignedDriver WHERE (HardWareID LIKE '%HPAnalytics%' OR HardWareID LIKE '%HPA000C%')");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(selectQuery);

                foreach (ManagementObject obj in searcher.Get())
                {
                    if (obj.GetPropertyValue("DriverVersion").ToString() == "4.1.4.9001" &&
                        obj.GetPropertyValue("DriverDate").ToString().Contains("20301231"))
                    {
                        DriverName = obj.GetPropertyValue("InfName").ToString();
                        Console.WriteLine("Inf Name         : {0}", obj.GetPropertyValue("InfName").ToString());
                        Console.WriteLine("Hardware ID      : {0}", obj.GetPropertyValue("HardWareID").ToString());
                        Console.WriteLine("Driver Version   : {0}", obj.GetPropertyValue("DriverVersion").ToString());
                        Console.WriteLine("Driver Date      : {0}", obj.GetPropertyValue("DriverDate").ToString());
                    }
                    else
                    {
                        Console.WriteLine("Driver Version   : {0}", obj.GetPropertyValue("DriverVersion").ToString());
                        Console.WriteLine("Driver Date      : {0}", obj.GetPropertyValue("DriverDate").ToString());
                    }

                }
                if (DriverName == String.Empty)
                    Console.WriteLine("Driver Nor Found");
                return DriverName;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception thrown : {0}",ex);
                return String.Empty;
            }
        }

        static int Main()
        {
            //retriveData();
            showDriversList3();
            Console.WriteLine("Program End");
            Console.ReadLine();
            return 0;
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
