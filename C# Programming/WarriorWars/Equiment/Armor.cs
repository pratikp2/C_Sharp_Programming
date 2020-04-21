using WarriorWars.Enum;

namespace WarriorWars.Equipment
{
    class Armor
    {
        private int m_armorPoints;
        private const int M_GOOD_GUY_ARMOR = 5;
        private const int M_BAD_GUY_ARMOR = 5;

        public int ArmorPoints { get { return m_armorPoints; } }

        public Armor(Faction faction)
        {
            switch (faction)
            {
                case Faction.GoodGuy:
                    m_armorPoints = M_GOOD_GUY_ARMOR;
                    break;
                case Faction.BadGuy:
                    m_armorPoints = M_BAD_GUY_ARMOR;
                    break;
                default:
                    break;
            }
        }
    }
}
