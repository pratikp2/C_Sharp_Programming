using System;
using System.Threading;
using System.Threading.Tasks;

namespace HP
{
    class Program
    {
        static Barrier barrier = new Barrier(participantCount: 0);
        static void Main(string[] args)
        {
            int totalRecords = GetNumberOfRecords();
            Task[] tasks = new Task[totalRecords];

            for (int i = 0; i < totalRecords; ++i)
            {
                barrier.AddParticipant();
                int j = i;
                tasks[j] = Task.Factory.StartNew(() =>
                {
                    GetDataAndStoreData(j);
                });
            }

            Task.WaitAll(tasks);
            Console.WriteLine("Backup completed");
            Console.ReadLine();
        }

        static int GetNumberOfRecords()
        {
            return 5;
        }

        static void GetDataAndStoreData(int index)
        {
            Console.WriteLine("Getting data from server: " + index);
            barrier.SignalAndWait();
            Console.WriteLine("Send data to Backup server: " + index);
            barrier.SignalAndWait();
        }
    }
}