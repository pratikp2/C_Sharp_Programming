//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;



using PointAndLines;        // changes Syntsx
using StaticClasses;

class EntryPoint
{
    static public int Main()
        {
        User Objuser = new User("Pratik",Race.Marsian);

        Utilities.SetTextColour("Pratik", System.ConsoleColor.Green);


        System.Console.ReadLine();      //OR Press ctrl + F5
        return 0;
    }
}


// The Main() method is the entry point a C# program from where the execution starts.
// Main() method must be static because it is a class level method.To invoked without any instance of the class it must be static. Non-static Main() method will give a compile-time error.
// Main() Method cannot be overridden because it is the static method.Also, the static method cannot be virtual or abstract.
// Overloading of Main() method is allowed.But in that case, only one Main() method is considered as one entry point to start the execution of the program.