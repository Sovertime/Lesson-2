using UnityEngine;

using DamageCalculator = Combat.DamageCalculator;

namespace Task4
{
    public class Unit
    {
        public string name { get; }
        public int health { get; private set; }
        public int armor { get; }
        public float critChance { get; }
        public DamageCalculator.Element element { get; }
        public Weapon weapon { get; set; }

        public Unit(string name, int health, int armor, float critChance, DamageCalculator.Element element, Weapon weapon)
        {
            this.name = name;
            this.health = health;
            this.armor = armor;
            this.critChance = critChance;
            this.element = element;
            this.weapon = weapon;
        }

        public void TakeDamage(int damage)
        {
            health = Mathf.Max(0, health - damage);
        }
    }
}