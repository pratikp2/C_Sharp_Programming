using System;
using System.Collections.Generic;
using System.Management;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Windows.Forms;

namespace Battery_Status
{
    static class Program
    {        
        static void CheckBatteryStatus1()
        {
            ObjectQuery query = new ObjectQuery("Select * FROM Win32_Battery");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

            ManagementObjectCollection collection = searcher.Get();

            foreach (ManagementObject mo in collection)
                foreach (PropertyData property in mo.Properties)          
                    Console.WriteLine("Property {0}: Value is {1}", property.Name, property.Value);       
        }

        static void CheckBatteryStatus2()
        {
            PowerStatus pwr = SystemInformation.PowerStatus;

            int hr = 0;
            int min = 0;
            int sec = pwr.BatteryLifeRemaining;

            min = sec / 60;
            sec = sec % 60;

            hr = min / 60;
            min = min % 60;

            Console.WriteLine("Power Line Status        :   {0}", pwr.PowerLineStatus.ToString());
            Console.WriteLine("Battery Charege Status   :   {0}", pwr.BatteryChargeStatus.ToString());
            Console.WriteLine("Battery Full lift time   :   {0}", pwr.BatteryFullLifetime);
            Console.WriteLine("Battery Life Percent     :   {0} %", pwr.BatteryLifePercent * 100);
            Console.WriteLine("Battery Life Remaining   :   {0}:{1}:{2} ", hr,min,sec);
        }

        static void ExecutePowerShellRoutone1()
        {
            try
            {
                PowerShell ps = PowerShell.Create();
                ps.AddCommand("New-BurntToastNotification");
                ps.AddParameter("-Text", "Remove the Charger");
                //ps.AddParameter("-Message", "Charging Above 95%");
                ps.AddParameter("-AppLogo", @"C:\Users\pppra\Desktop\index.jpg");
                Console.WriteLine(ps.Invoke());

                /*PowerShell ps = PowerShell.Create();
                ps.AddScript(@"C:\Users\pppra\Desktop\Indicator.ps1",true);
                ps.Invoke();*/

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception thrown : {0}", ex);
            }
        }


        static void ExecutePowerShellRoutone2()
        {
            try
            {
                PowerShell ps = PowerShell.Create().AddCommand("Get-Process");
                IAsyncResult async = ps.BeginInvoke();

                foreach (PSObject result in ps.EndInvoke(async))
                    Console.WriteLine("{0,-20}{1}",result.Members["ProcessName"].Value,result.Members["Id"].Value);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception thrown : {0}", ex);
            }
        }


        static void ExecutePowerShellRoutone3()
        {
            try
            {
                //pipeline.AddScript("Import-Module BurntToast");
                //pipeline.AddCommand("pwd");
                //pipeline.Invoke();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception thrown : {0}", ex);
            }
        }

        static int Main()
        {
            //CheckBatteryStatus1();
            //CheckBatteryStatus2();
            ExecutePowerShellRoutone1();
            //ExecutePowerShellRoutone2();
            Console.WriteLine("Program End ...!");
            Console.ReadLine();
            return 0;
        }
    }
}