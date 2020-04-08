using System;

namespace ValuesAndReferences
{
    class Program
    {
        // readonly
        private readonly int Var1 = 10;     // Can be modified at the time Initiliazation or in Constructor.
                                            // Value change even via member functions is not alllowd.
        // const
        public const int var2 = 20;         // Value Can be set only at the time of instantiation.
                                            // It can not be changed via any menber function or even via Constructor.

        static void Main()
        {
            Console.WriteLine(Program.var2); // No need to create object while referencing can be referenced as a static 
                                             // field as the instantiation is not object dependent.
        }
    }
}

// similar to Concept in C++
// keyword ref must be used while defining and calling functions.
