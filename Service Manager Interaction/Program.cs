using System;
using System.IO;
using System.Linq;
using System.Management;
using System.ServiceProcess;

namespace Service_Manager_Interaction
{
    static class Program
    {
        static public bool serviceExists(string ServiceName)
        {
            return ServiceController.GetServices().Any(serviceController => serviceController.ServiceName.Equals(ServiceName));
        }
        static private void CheckStatusViaManagementObject()
        {
            ManagementObject wmiService = new ManagementObject("Win32_Service.Name='HpTouchpointAnalyticsService'");
            string path = "";
            try
            {
                wmiService.Get();
                FileInfo f = new FileInfo(wmiService["PathName"].ToString().Trim('"'));
                path = Path.GetPathRoot(f.FullName) + "Program Files (x86)\\HP\\HP Touchpoint Analytics Client\\TouchpointAnalyticsClientService.exe";
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            Console.WriteLine(wmiService["PathName"].ToString().Trim('"'));
            Console.WriteLine(path);
            Console.WriteLine(wmiService["PathName"].ToString().Trim('"') == path);
        }

        static private void CheckStatusViaServiceControlManager()
        {
            try
            {
                if (serviceExists("TouchpointAnalyticsClientService"))
                {
                    ServiceController handle = new ServiceController("TouchpointAnalyticsClientService");
                    Console.WriteLine(handle.ServiceName);
                    Console.WriteLine(handle.Status);
                    //AssertState.Equal(ServiceControllerStatus.Running, ctl.Status);
                }
                else
                    Console.WriteLine("Service does not Exist");
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
        static int Main()
        {
            //CheckStatusViaManagementObject();
            CheckStatusViaServiceControlManager();
            Console.ReadLine();
            return 0;
        }
    }
}
