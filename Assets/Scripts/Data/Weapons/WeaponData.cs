using UnityEngine;
using Element = Combat.DamageCalculator.Element;

namespace Data.Weapons
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Game/Weapon")]
    public class WeaponData : ScriptableObject
    {
        [SerializeField] private string m_weaponName;
        [SerializeField, Min(0)] private int m_minDamage = 1;
        [SerializeField, Min(0)] private int m_maxDamage = 10;
        [SerializeField] private Element m_element;

        public string weaponName => m_weaponName;
        public int minDamage => m_minDamage;
        public int maxDamage => m_maxDamage;
        public Element element => m_element;
    }
}