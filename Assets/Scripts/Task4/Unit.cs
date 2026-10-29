using UnityEngine;

namespace Task4
{
    public class Unit
    {
        public string Name;
        public int Health { get; private set; }
        public int Armor;
        public float CritChance;
        public DamageCalculator.Element Element;
        public Weapon Weapon;

        public Unit(string name, int health, int armor, float critChance, DamageCalculator.Element element, Weapon weapon)
        {
            Name = name;
            Health = health;
            Armor = armor;
            CritChance = critChance;
            Element = element;
            Weapon = weapon;
        }

        public void TakeDamage(int damage)
        {
            Health = Mathf.Max(0, Health - damage);
        }
    }
}