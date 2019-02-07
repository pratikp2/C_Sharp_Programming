//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using PointAndLines;        // changes Syntsx

class EntryPoint
{
    static int Main()
        {
        User Objuser = new User();

        Objuser.Username = "abcd";
        Objuser.Password = 5;


        System.Console.WriteLine(Objuser.Username);
        System.Console.WriteLine(Objuser.Password);


        //System.Console.WriteLine("Press Enter To Close");
        //System.Console.ReadLine();

        //OR Press ctrl + F5

        return 0;
    }
}
