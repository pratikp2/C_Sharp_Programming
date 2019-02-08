using System;

namespace StaticClasses
{
    static class Utilities
    {
        public static void SetTextColour(string Message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(Message);
            Console.ResetColor(); 
        }
    }
}


// Static classes can not have constructor. On writing Constructor it will throw error.
// Each Method of the static class has to be static.
// Static Class is equivalent of the Singleton Class.