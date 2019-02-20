using WarriorWars.Enum;
using WarriorWars.Equipment;

namespace WarriorWars
{
    class warrior
    {
        private const int M_GOOD_GUY_STARTING_HEALTH = 100;
        private const int M_BAD_GUY_STARTING_HEALTH = 100;

        private int m_health;
        private string m_name;
        private bool m_isAlive;

        private Weapon m_weapon;
        private Armor m_armor;

        private readonly Faction M_FACTION;

        public bool IsAlive { get { return m_isAlive; } }

        public warrior(string name, Faction faction)
        {
            this.m_name =name;
            this.M_FACTION = faction;
            m_isAlive = true;

            switch (M_FACTION)
            {
                case Faction.GoodGuy:
                    m_weapon = new Weapon(faction);
                    m_armor = new Armor(faction);
                    m_health = M_GOOD_GUY_STARTING_HEALTH;
                    break;
                case Faction.BadGuy:
                    m_weapon = new Weapon(faction);
                    m_armor = new Armor(faction);
                    m_health = M_BAD_GUY_STARTING_HEALTH;
                    break;
                default:
                    break;
            }
        }

        public void Attack(warrior Enemy)
        {

        }
    }
}
