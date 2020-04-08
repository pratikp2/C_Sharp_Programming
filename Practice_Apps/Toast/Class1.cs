using System;
using System.Windows.Forms;
using System.Linq.Expressions;
using System.Management.Automation;
using System.Runtime;
using System.Management.Automation.Runspaces;


namespace Toast
{
    class Program
    {
        System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();

        public Program()
        {
            timer1.Interval = 1000; //Period of Tick
            timer1.Tick += timer1_Tick;
        }

        static void Main()
        {
            PowerShell ps = PowerShell.Create();

            // add command
            

            // run command(s)
            ps.AddScript(@"D:\PSScripts\MyScript.ps1", true).Invoke();
            ps.AddScript()
            //Console.WriteLine("Date: {0}", ps.Invoke().First());

            Console.WriteLine(SystemInformation.PowerStatus.BatteryLifePercent.ToString());
            Console.ReadLine();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            CheckBatteryStatus();
        }
        private void CheckBatteryStatus()
        {
          
        }
    }
}
