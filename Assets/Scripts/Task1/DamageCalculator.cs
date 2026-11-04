using UnityEngine;

using Element = Combat.DamageCalculator.Element;
using ElementEffect = Combat.DamageCalculator.ElementEffect;

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
        
        private int m_attackCount;

        private void Start()
        {
            Debug.Log("Нажмите [Space] для удара");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Calculate();
        }

        private void Calculate()
        {
            int damage = Combat.DamageCalculator.Calculate(m_baseDamage, m_baseArmor, m_weaponElement, m_targetElement, m_critChance, out bool isCrit, out ElementEffect elementEffect);

            m_attackCount++;

            string elementText = elementEffect switch
            {
                ElementEffect.Same => "одинаковые стихии", 
                ElementEffect.Opposite => "стихия х1.5", 
                _ => "без бонуса стихии"
            };

            string critText = isCrit ? "крит х2" : "без крита";
            
            Debug.Log($"Удар {m_attackCount}: {damage} урона ({elementText}, {critText})");
        }
    }
}
