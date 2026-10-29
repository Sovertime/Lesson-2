using UnityEngine;

namespace Task4
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

        public static int Calculate(int damage, int armor, Element weapon, Element target, float crit)
        {
            int result = Mathf.Max(1, damage - armor);
        
            switch (weapon, target)
            {
                case (Element.None, _) or (_, Element.None):
                    break;
            
                case(Element.Fire, Element.Fire) or (Element.Ice, Element.Ice):
                    result = 0;
                    break;
            
                case(Element.Fire, Element.Ice) or (Element.Ice, Element.Fire):
                    result = Mathf.FloorToInt(result * ElementMultiplier);
                    break;
            }

            bool isCrit = Random.Range(0f, 1f) < crit;
            int finalResult = isCrit ? result * 2 : result;
            
            return finalResult;
        }
    }
}
