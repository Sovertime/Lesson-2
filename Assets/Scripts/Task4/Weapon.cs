using UnityEngine;

namespace Task4
{
    public class Weapon
    {
        public string Name;
        public int MinDamage;
        public int MaxDamage;
        public DamageCalculator.Element Element;

        public Weapon(string name, int minDamage, int maxDamage, DamageCalculator.Element element)
        {
            Name = name;
            MinDamage = minDamage;
            MaxDamage = maxDamage;
            Element = element;
        }
    }
}

