using System;
using System.Activities.Presentation;
using System.Management;
using System.ServiceProcess;

namespace Service_Manager_Interaction
{
    static class Program
    {
        static void Main()
        {
            try
            {
                using (ManagementObject wmiService = new ManagementObject("Win32_Service.Name='HpTouchpointAnalyticsService'"))
                {
                    wmiService.Get();
                    Console.WriteLine(wmiService["PathName"].ToString());
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            Console.ReadLine();
        }
    }
}
