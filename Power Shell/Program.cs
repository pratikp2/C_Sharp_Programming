using System;
using System.IO;
using System.Management.Automation;

namespace Power_Shell
{
    static class Program
    {        
        static void ExecutePowerShellRoutone1()
        {
            try
            {
                PowerShell ps = PowerShell.Create();
                ps.AddCommand("Get-ExecutionPolicy");
                ps.Invoke();
                //ps.AddParameter("-list");
                //Console.WriteLine(ps.Invoke().ToString());
                /*PowerShell ps = PowerShell.Create();
                ps.AddScript(@"C:\Users\pppra\Desktop\Indicator.ps1",true);
                ps.Invoke();*/
            }
            catch (Exception ex) { Console.WriteLine("Exception thrown : {0}", ex); }
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

        static int Main()
        {
            ExecutePowerShellRoutone1();
            //ExecutePowerShellRoutone2();
            //Console.WriteLine("Program End ...!");
            Console.ReadLine();
            return 0;
        }
    }
}