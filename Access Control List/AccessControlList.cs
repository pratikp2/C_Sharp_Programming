using System;
using System.IO;
using System.Diagnostics;
using System.Security.AccessControl;
using System.Text;
using System.Security;
using System.Security.Principal;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace Test
{
   class Program
    {
        static private string dirPath  = "D:/Practice/Git_Repos/Windows_Programming/Utils/";
        static private string filePath = "D:/Practice/Git_Repos/Windows_Programming/Utils/Sample_Text";

        static private String account = WindowsIdentity.GetCurrent().Name;
        static private String SID = WindowsIdentity.GetCurrent().User.ToString();

        static private string account1 = @"PRATIK-PC\pppra";
        static private string account2 = @"PRATIK-PC\Test User";

        static private string account3 = @"NT AUTHORITY\Authenticated Users";
        static private string account4 = @"NT AUTHORITY\SYSTEM";

        static private string account5 = @"BUILTIN\Administrators";
        static private string account6 = @"BUILTIN\Users";

        
        static private void CheckDirectorySecurity()
        {
            var DIArray = new DirectoryInfo(dirPath);
            DirectorySecurity DirSec = DIArray.GetAccessControl(AccessControlSections.Access);
            foreach (FileSystemAccessRule FSAR in DirSec.GetAccessRules(true, true, typeof(System.Security.Principal.NTAccount)))
                GetAceInformation(FSAR);    
        }

        static private void CheckFileSecurity()
        {
            var DIArray = new FileInfo(filePath);
            FileSecurity DirSec = DIArray.GetAccessControl(AccessControlSections.Access);
            foreach (FileSystemAccessRule FSAR in DirSec.GetAccessRules(true, true, typeof(System.Security.Principal.NTAccount)))
                GetAceInformation(FSAR);
        }

        static private void GetAceInformation(FileSystemAccessRule ace)
        {   
            Console.WriteLine("Account      : {0}", ace.IdentityReference.Value);
            Console.WriteLine("Type         : {0}", ace.AccessControlType);
            Console.WriteLine("Rights       : {0}", ace.FileSystemRights);
            Console.WriteLine("Inherited ACE: {0}", ace.IsInherited);
            Console.WriteLine("------------------------------------------------------------------\n");
        }

        public static void AddDirectorySecurity(string accLogin, FileSystemRights Rights, AccessControlType ControlType)
        {
            DirectoryInfo dInfo = new DirectoryInfo(dirPath);
            DirectorySecurity dSecurity = dInfo.GetAccessControl();
            //dSecurity.AddAccessRule(new FileSystemAccessRule(accLogin, Rights, ControlType)); - Non Recursive file Access permission
            dSecurity.AddAccessRule(new FileSystemAccessRule(accLogin, Rights,InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                PropagationFlags.InheritOnly, ControlType));        // - Recursive file Access permission
            dInfo.SetAccessControl(dSecurity);
        }
      
        public static void RemoveDirectorySecurity(string accLogin, FileSystemRights Rights, AccessControlType ControlType)
        {
            DirectoryInfo dInfo = new DirectoryInfo(dirPath);
            DirectorySecurity dSecurity = dInfo.GetAccessControl();
            //dSecurity.RemoveAccessRule(new FileSystemAccessRule(accLogin, Rights, ControlType)); - Non Recursive file Access permission
            dSecurity.RemoveAccessRule(new FileSystemAccessRule(accLogin, Rights, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                PropagationFlags.InheritOnly, ControlType));        // - Recursive file Access permission
            dInfo.SetAccessControl(dSecurity);
        }

        public static void AddFileSecurity(string accLogin, FileSystemRights rights, AccessControlType controlType)
        {
            FileSecurity fSecurity = File.GetAccessControl(filePath);
            fSecurity.AddAccessRule(new FileSystemAccessRule(accLogin, rights, controlType));
            File.SetAccessControl(filePath, fSecurity);
        }

        public static void RemoveFileSecurity(string accLogin, FileSystemRights rights, AccessControlType controlType)
        {
            FileSecurity fSecurity = File.GetAccessControl(filePath);
            fSecurity.RemoveAccessRule(new FileSystemAccessRule(accLogin, rights, controlType));
            File.SetAccessControl(filePath, fSecurity);
        }

        public static void StartNewProcess(string accLogin, string accPassword)
        {
            Process proc = new Process();
            SecureString ssPwd = new SecureString();
        
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.FileName = "D:/Practice/Git_Repos/Windows_Programming/C# Programming/Dummy Executable/bin/Debug/Test_Dummy";
            proc.StartInfo.Domain = string.IsNullOrEmpty(Environment.UserDomainName) ? "." : Environment.UserDomainName;
            proc.StartInfo.UserName = accLogin;

            for (int x = 0; x < accPassword.Length; x++)
                ssPwd.AppendChar(accPassword[x]);
            
            accPassword = "";

            proc.StartInfo.Password = ssPwd;
            proc.Start();
        }

        public static void CalloftheWhile()
        {
            try
            {
                FileInfo info = new FileInfo("D:/Practice/Git_Repos/Windows_Programming/Utils/Sample_Text");
                using (FileStream fileStream = info.OpenRead())
                using (StreamReader fileReader = new StreamReader(fileStream))
                    fileReader.ReadToEnd();              
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        static void Main(string[] args)
        {
            // Apply or Remove ACL to a Directory 
            //AddDirectorySecurity(account1, FileSystemRights.ReadData, AccessControlType.Deny);
            //RemoveDirectorySecurity(account, FileSystemRights.ReadData, AccessControlType.Allow);
            
            // Apply or Remove ACL to a File. 
            //AddFileSecurity(account5, FileSystemRights.ReadData, AccessControlType.Deny);
            //RemoveFileSecurity(account5, FileSystemRights.ReadData, AccessControlType.Allow);

            // Print Access Control List Entries
            CheckDirectorySecurity();
            //CheckFileSecurity();

            //CalloftheWhile();
            //StartNewProcess("pppra","Voyager@1977");
            //SecurityUtilities.RunAs("BUILTIN/Administrators", "Voyager@1977");

            //Console.WriteLine(WindowsIdentity.GetCurrent().User);
            //Console.WriteLine(new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null).Value);
            Console.ReadLine();
        }
    }

        public class SecurityUtilities
        {
            [DllImport("advapi32.dll", SetLastError = true)]
            private static extern bool LogonUser(string lpszUserName, string lpszDomain, string lpszPassword, 
                int dwLogonType, int dwLogonProvider, out IntPtr phToken);

            public static void RunAs(string username, string password)
            {
                string userName;
                string domain;
                if (username.IndexOf('/') > 0)
                {
                    //a domain name was supplied
                    string[] usernameArray = username.Split('/');
                    userName = usernameArray[1];
                    domain = usernameArray[0];
                }
                else
                {
                    //there was no domain name supplied
                    userName = username;
                    domain = ".";
                }
                RunAs(userName, password, domain);
            }

            public static void RunAs(string username, string password, string domain)
            {
                IntPtr userToken;
                WindowsIdentity adminIdentity = null;
                WindowsImpersonationContext adminImpersonationContext = null;

                try
                {
                    if (LogonUser(username, string.IsNullOrEmpty(domain) ? "." : domain, password, 2, 0, out userToken))
                    {
                        //the impersonation suceeded
                        adminIdentity = new WindowsIdentity(userToken);
                        adminImpersonationContext = adminIdentity.Impersonate();

                        // todo: Entfernen.
                        WindowsPrincipal p = new WindowsPrincipal(adminIdentity);
                        Console.WriteLine(p.IsInRole(WindowsBuiltInRole.Administrator).ToString());

                        Program.StartNewProcess("Test User", "Voyager@1977");
                }
                    else
                        throw new Exception(string.Format("Could not impersonate user {0} in domain {1} with the specified password.", username, domain));
                }
                catch (Exception se)
                {
                    int ret = Marshal.GetLastWin32Error();
                    if (adminImpersonationContext != null)
                        adminImpersonationContext.Undo();
                    throw new Exception("Error code: " + ret.ToString(), se);
                }
                finally
                {
                    //revert to self
                    if (adminImpersonationContext != null)
                        adminImpersonationContext.Undo();
                }
            }
        }
    
}


// 
