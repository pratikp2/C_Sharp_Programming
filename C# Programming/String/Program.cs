using System;

namespace String
{
    class Program
    {
        static void Main(string[] args)
        {
            string fname, lname;
            fname = "Rowan";
            lname = "Atkinson";

            char[] letters = { 'H', 'e', 'l', 'l', 'o' };
            string[] sarray = { "Hello", "From", "Tutorials", "Point" };

            string fullname = fname + lname;
            Console.WriteLine("Full Name: {0}", fullname);

            string greetings = new string(letters);
            Console.WriteLine("Greetings: {0}", greetings);

            string message = System.String.Join(" ", sarray);
            Console.WriteLine("Message: {0}", message);

            DateTime waiting = new DateTime(2012, 10, 10, 17, 58, 1);
            string chat = System.String.Format("Message sent at {0:t} on {0:D}", waiting);
            Console.WriteLine("Message: {0}", chat);
        }
    }
}
