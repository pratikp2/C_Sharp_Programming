using System;
using System.Collections.Generic;

namespace Generics
{

    class Program
    {
        static void Main() 
        {
            var obj1 = new Sample<int>();
            var obj2 = new Sample<string>();

            obj1.SampleMethod(4);
            obj2.SampleMethod("Hello World");

            Console.ReadLine();
        }
    }

    class Sample<typename>
    {
        public void SampleMethod(typename a) { Console.WriteLine(a); }
    }
}

