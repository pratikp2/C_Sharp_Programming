using WarriorWars.Enum;


namespace WarriorWars.Equipment
{
    class Weapon
    {
        private int m_damage;
        private const int M_GOOD_GUY_Damage = 10;
        private const int M_BAD_GUY_Damage = 10;

        public int Damage { get {return m_damage;} }

        public Weapon(Faction faction)
        {
            switch (faction)
            {
                case Faction.GoodGuy:
                    m_damage = M_GOOD_GUY_Damage;
                    break;
                case Faction.BadGuy:
                    m_damage = M_BAD_GUY_Damage;
                    break;
                default:
                    break;
            }
        }
    }
}
