using Microsoft.Win32;
using System;

namespace Windows_Registry
{
    static class Program
    {          
        static void Main()
        {
            try
            {
                // Creation of Registry Key
                // HKEY_LOCAL_MACHINE - Needs Admin access and exe elevated at the time of installation
                RegistryKey key = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\PratikUtils");

                // HKEY_CURRENT_USER - Do not need application Elevated.
                RegistryKey key1 = Registry.CurrentUser.CreateSubKey(@"Software\PratikUtils\RegistryStudy");

                // Open Existing key
                RegistryKey key2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\HP\HP Touchpoint Manager");
                RegistryKey key3 = null;

                key3 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default);
                key3 = key3.OpenSubKey("SOFTWARE//Microsoft//Windows//CurrentVersion//Uninstall//TruePianos 40-day Test Version_is1", true);
                // Storing the values  
                key1.SetValue("Setting1", "This is our setting 1");
                key1.SetValue("Setting2", "This is our setting 2");

                var path = key3.GetValue("UninstallString");
                // Close opened keys
                key.Close();
                key1.Close();
                key2.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Occured : {0}", ex);
            }


            //RegistryKey key1 = (Registry.LocalMachine).OpenSubKey(@"SOFTWARE\Policies\HP\HP Analytics");
            //RegistryKey key = (Registry.LocalMachine).OpenSubKey(@"Computer\HKEY_LOCAL_MACHINE\SOFTWARE\Policies\HP\HP Analytics", true);
            //if it does exist, retrieve the stored values  
            //if (key1 != null)
            //{
            //    Console.WriteLine(key1.GetValue("TargetContent"));
            //    Console.WriteLine(key.GetValue("Setting2"));
            //    key1.Close();
            //}
            //else
            //{
            //    Console.WriteLine("Key not found");
            //}
            Console.WriteLine("Program End ...!");
            Console.ReadLine();
        }
    }
}


// Just because a application is running with Administrator privileges (or using an account with administrative
// privileges) does not mean that those administrative privileges are always in effect. This is a security measure,
// preventing malware from exploiting users who foolishly use their computer all the time with administrative
// privileges.

// To wield your administrative privileges, you need to elevate the process.There are two ways to do this:

// 1.
// Use a manifest that indicates your application requires administrative privileges, and thus demand
// elevation at startup.
// This means your application will always run elevated and should only be used when your application needs
// this. The Windows Registry Editor (RegEdit), for example, does this, because there's little you can do 
// there without administrative privileges.
// Find information about how to accomplish this here on MSDN, or in my answer here.Basically, you just 
// want to add the following line to your manifest:
// <requestedExecutionLevel level = "requireAdministrator" />

// 2
// If you only need administrative privileges for certain tasks (i.e., saving a particular setting to the 
// registry) and the rest of your application does not require it, you should launch a new elevated process
// to do this. There is no way to temporarily elevate the current process, so you actually need to spin off
// a new process.
// The advantage of this method is that your application does not have to run with administrative privileges
// all the time (which increases security), and that users who do not have administrative privileges will 
// still be able to run your app to do everything else (i.e., everything but the one or two tasks that do
// require elevation).