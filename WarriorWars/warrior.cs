using System;
using System.Threading;
using WarriorWars.Enum;
using WarriorWars.Equipment;

namespace WarriorWars
{
    class warrior
    {
        private const int M_GOOD_GUY_STARTING_HEALTH = 20;
        private const int M_BAD_GUY_STARTING_HEALTH = 20;

        private int m_health;
        private string m_name;
        private bool m_isAlive;

        private Weapon m_weapon;
        private Armor m_armor;

        private readonly Faction M_FACTION;

        public bool IsAlive { get { return m_isAlive; } }

        public warrior(string name, Faction faction)
        {
            this.m_name = name;
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

        public void Attack(warrior enemy)
        {
            int damage = m_weapon.Damage / enemy.m_armor.ArmorPoints;
            enemy.m_health = enemy.m_health - damage;

            AttackResult(enemy, damage);
            Thread.Sleep(100);
        }

        private void AttackResult(warrior enemy, int damage)
        {
            if (enemy.m_health < 0)
            {
                enemy.m_isAlive = false;
                Tools.selectColour($"{enemy.m_name} is dead.", ConsoleColor.Red);
                Tools.selectColour($"{this.m_name} is Victorious", ConsoleColor.Green);
            }
            else
            {
                Console.WriteLine($"{this.m_name} Attacked {enemy.m_name} for {damage} points Damage ");
                Console.WriteLine($"{this.m_name}'s Current Health is {m_health}.");
                Console.WriteLine($"{enemy.m_name}'s Current Health is {enemy.m_health}.");
                Console.WriteLine($"_ _ _ _ _ _ _ _ _  _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _  _ _ _ _ _");
            }
        }
    }
}
