using System;
using System.IO;

namespace Keywords
{
    class Program
    {
        // readonly
        private readonly int Var1 = 10;     // Can be modified at the time Initiliazation or in Constructor.
                                            // Value change even via member functions is not alllowd.
        // const
        public const int var2 = 20;         // Value Can be set only at the time of instantiation.
                                            // It can not be changed via any menber function or even via Constructor.

        // using
        public void TestMethod1()
        {
            // The Code inside the bracket of using statement generates resources 
            // as per written inside the bracket whose scope is limited to using
            // {} braces.
            // Usually used with files, Sockets or some in cases which has 
            // underlaying resources.
            using (var writer = File.AppendText("Sample.txt"))
            {
                // Write the code to execute operation on resources 
                // Instantiated in using statement.
                writer.WriteLine("Hello Sample Test");
            }

            // After using statement is Executed the resources are freed immediately
            // instead for freeing after program termination.

            // Instead of using can also use : writer.Dispose();
            // Dispose API also calls garbage collector on used class.
        }

        static void Main()
        {
            Console.WriteLine(Program.var2); // No need to create object while referencing can be referenced as a static 
                                             // field as the instantiation is not object dependent.
        }
    }
}

// similar to Concept in C++
// keyword ref must be used while defining and calling functions.
