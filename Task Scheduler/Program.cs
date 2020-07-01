using System;
using Microsoft.Win32.TaskScheduler;

namespace Task_Scheduler
{
    static class Program
    {
        private static string TaskName = "Delete File";
        private static void CreateTask(string taskName)
        {
            try
            {
                using (TaskService taskService = new TaskService())
                {
                    // 1. Create a new task definition and assign properties
                    TaskDefinition defination = taskService.NewTask();
                    defination.RegistrationInfo.Description = "Pratik Test Deletion Task";
                    defination.Settings.StopIfGoingOnBatteries = false;
                    defination.Settings.DisallowStartIfOnBatteries = false;
                    defination.Settings.RunOnlyIfIdle = false;
                    defination.Settings.RunOnlyIfNetworkAvailable = false;
                    defination.Settings.ExecutionTimeLimit = TimeSpan.Zero;
                    defination.Settings.StartWhenAvailable = true;
                    defination.Settings.Hidden = false;
                    defination.Settings.Enabled = true;
                    //defination.Principal.RunLevel = TaskRunLevel.Highest;
                    //defination.Principal.UserId = WindowsIdentity.GetCurrent().Name;
                    //defination.Principal.LogonType = TaskLogonType.InteractiveToken;

                    // 2. Add a trigger 
                    TimeTrigger trigger = new TimeTrigger();
                    //trigger.StartBoundary = DateTime.Now.AddHours(1);
                    //trigger.StartBoundary = DateTime.Now.AddMinutes(20);
                    trigger.StartBoundary = DateTime.Now.AddSeconds(20);
                    defination.Triggers.Add(trigger);

                    // 3. Add actions in Defination
                    //defination.Actions.Add(new ExecAction(@"ForFiles", "/p \"C:\\Utils\\Task_Scheduler_POC\" /c \"cmd /c del @File1.txt\"", null));
                    //defination.Actions.Add(new ExecAction(@"forfiles", "/p \"C:\\Utils\\Task_Scheduler_POC\" /c \"cmd /c del @File1.txt\"", null));
                    defination.Actions.Add(new ExecAction(@"forfiles", "/p \"C:\\Utils\\Task_Scheduler_POC\" /m File1.txt /c \"cmd /c del @file\"", null));
                    //del "C:\Utils\Task_Scheduler_POC\File1.txt"
                    // 4. Register the task in root folder
                    taskService.RootFolder.RegisterTaskDefinition(taskName, defination, TaskCreation.CreateOrUpdate, "SYSTEM", null, TaskLogonType.ServiceAccount);

                    defination.Dispose();
                }
            }
            catch (Exception ex) { Console.WriteLine("Exception Thrown : {0}", ex.Message); }
        }
        private static void DeleteTask(string taskName)
        {
            using (TaskService ts = new TaskService())
            using (Task task = ts.FindTask(taskName))
            {
                ts.RootFolder.DeleteTask(taskName);
            }
        }
    
        static int Main()
        {
            CreateTask(TaskName);

            Console.WriteLine("Program End");
            Console.ReadLine();
            return 0;
        }
    }
}