using WarriorWars.Enum;

namespace WarriorWars
{
    class EntryPoint
    {
        static int Main()
        {
            warrior goodguy = new warrior("Pratik", Faction.GoodGuy);
            warrior badguy = new warrior("Kalya", Faction.BadGuy);

            goodguy.Attack(badguy);
            return 0;
        }
    }
}
