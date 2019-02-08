//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;



using PointAndLines;        // changes Syntsx
using StaticClasses;

class EntryPoint
{
    static int Main()
        {
        User Objuser = new User("Pratik",Race.Marsian);

        Utilities.SetTextColour("Pratik", System.ConsoleColor.Green);


        System.Console.ReadLine();      //OR Press ctrl + F5
        return 0;
    }
}
