using UnityEngine;

namespace Combat
{
    public static class DamageCalculator
    {
        private const float ElementMultiplier = 1.5f;

        public enum Element
        {
            None,
            Fire,
            Ice
        }

        public enum ElementEffect
        {
            None,
            Same,
            Opposite
        }

        public static int Calculate(int damage, int armor, Element weapon, Element target, float crit)
        {
            return Calculate(damage, armor, weapon, target, crit, out _, out _);
        }

        public static int Calculate(int damage, int armor, Element weapon, Element target, float crit, 
            out bool isCrit, out ElementEffect elementEffect)
        {
            int result = Mathf.Max(1, damage - armor);
            elementEffect = ElementEffect.None;
        
            switch (weapon, target)
            {
                case (Element.None, _) or (_, Element.None):
                    break;
            
                case(Element.Fire, Element.Fire) or (Element.Ice, Element.Ice):
                    result = 0;
                    elementEffect = ElementEffect.Same;
                    break;
            
                case(Element.Fire, Element.Ice) or (Element.Ice, Element.Fire):
                    result = Mathf.FloorToInt(result * ElementMultiplier);
                    elementEffect = ElementEffect.Opposite;
                    break;
            }

            isCrit = Random.Range(0f, 1f) < crit;
            
            return isCrit ? result * 2 : result;
        }
    }

}

