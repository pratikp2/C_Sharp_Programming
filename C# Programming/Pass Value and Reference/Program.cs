using System;

namespace ValuesAndReferences
{
    class Program
    {
        static public void PassbyValue(int val)
        {
            val = val * val;
        }

        static public void PassbyReference(ref int val)
        {
            val = val * val;
        }

        static void Main()
        {
            int check1 = 10;
            int check2 = 10;

            PassbyValue(check1);
            PassbyReference(ref check2);

            Console.WriteLine("Value for check1 : {0}", check1);
            Console.WriteLine("Value for check2 : {0}", check2);

            Console.ReadLine();
        }
    }
}

// similar to Concept in C++
// keyword ref must be used while defining and calling functions.
