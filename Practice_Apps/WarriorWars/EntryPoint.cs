using System;
using WarriorWars.Enum;

namespace WarriorWars
{
    class EntryPoint
    {
        static Random rng = new Random();

        static int Main()
        {
            warrior goodguy = new warrior("Pratik", Faction.GoodGuy);
            warrior badguy = new warrior("Kalya", Faction.BadGuy);

            while (goodguy.IsAlive && badguy.IsAlive)
            {
                if (rng.Next(0, 10) < 5)
                {
                    goodguy.Attack(badguy);
                }
                else
                {
                    badguy.Attack(goodguy);
                }
            }
            Console.ReadLine();
            return 0;
        }
    }
}
