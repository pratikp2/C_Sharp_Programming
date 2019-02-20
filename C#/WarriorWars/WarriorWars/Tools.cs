using System;

namespace WarriorWars
{
    static class Tools
    {
        static public void selectColour(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
