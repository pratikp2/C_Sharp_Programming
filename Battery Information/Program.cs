using System;
using System.Management;
using System.Windows.Forms;
using ObjectQuery = System.Management.ObjectQuery;

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

        static int Main()
        {
            //CheckBatteryStatus1();
            CheckBatteryStatus2();
            Console.WriteLine("Program End ...!");
            Console.ReadLine();
            return 0;
        }
    }
}