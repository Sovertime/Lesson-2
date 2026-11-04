using UnityEngine;

namespace Task1
{
    public class DamageCalculator : MonoBehaviour
    {
        [Header("Base stats")]
        [SerializeField, Min(0)] private int m_baseDamage = 10;
        [SerializeField, Min(0)] private int m_baseArmor = 3;
        [SerializeField, Range(0f, 1f)] private float m_critChance = 0.3f;
    
        [Header("Elements")]
        [SerializeField] private Element m_weaponElement;
        [SerializeField] private Element m_targetElement;
    
        private const float ElementMultiplier = 1.5f;
        private int m_attackCount;

        private void Start()
        {
            Debug.Log("Нажмите [Space] для удара");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Calculate(m_baseDamage, m_baseArmor, m_weaponElement, m_targetElement, m_critChance);
        }

        private enum Element
        {
            None,
            Fire,
            Ice
        }

        private void Calculate(int damage, int armor, Element weapon, Element target, float crit)
        {
            int result = Mathf.Max(1, damage - armor);
            string elementText = "без бонуса стихии";
        
            switch (weapon, target)
            {
                case (Element.None, _) or (_, Element.None):
                    break;
            
                case(Element.Fire, Element.Fire) or (Element.Ice, Element.Ice):
                    result = 0;
                    elementText = "одинаковые стихии";
                    break;
            
                case(Element.Fire, Element.Ice) or (Element.Ice, Element.Fire):
                    result = Mathf.FloorToInt(result * ElementMultiplier);
                    elementText = "стихия х1.5";
                    break;
            }

            m_attackCount++;

            bool isCrit = Random.Range(0f, 1f) < crit;
            int finalResult = isCrit ? result * 2 : result;

            string critText = isCrit ? "крит х2" : "без крита";
        
            Debug.Log($"Удар {m_attackCount}: {finalResult} урона ({elementText}, {critText})");
        }
    }
}
