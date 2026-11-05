using DamageCalculator = Combat.DamageCalculator;

namespace Task4
{
    public class Weapon
    {
        public string name { get; }
        public int minDamage { get; }
        public int maxDamage { get; }
        public DamageCalculator.Element element { get; }

        public Weapon(string name, int minDamage, int maxDamage, DamageCalculator.Element element)
        {
            this.name = name;
            this.minDamage = minDamage;
            this.maxDamage = maxDamage;
            this.element = element;
        }
    }
}

