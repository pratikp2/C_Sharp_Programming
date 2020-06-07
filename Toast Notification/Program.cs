using System;
using System.Management.Automation;

namespace Toast_Notification
{
    static class Program
    {        
       
        static void ToastViaBurntToast()
        {
            try
            {
                PowerShell ps = PowerShell.Create();
                ps.AddCommand("New-BurntToastNotification");
                ps.AddParameter("-Text", "My Title");
                ps.AddParameter("-AppLogo", @"C:\Users\pppra\Desktop\index.jpg");
                //ps.AddParameter("-Sound", @"");       Provide Path to audio file
                Console.WriteLine(ps.Invoke());
            }
            catch (Exception ex){ Console.WriteLine("Exception thrown : {0}", ex);}
        }

        static int Main()
        {
            ToastViaBurntToast();
            Console.WriteLine("Program End ...!");
            Console.ReadLine();
            return 0;
        }
    }
}