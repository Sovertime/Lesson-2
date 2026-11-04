
using UnityEngine;
using Element = Combat.DamageCalculator.Element;

namespace Characters
{
    [CreateAssetMenu(fileName = "Character", menuName = "Game/Character")]
    public class CharacterData : ScriptableObject
    {
        [Header("General")]
        [SerializeField] private string m_characterName;
        [SerializeField, Min(1)] private int m_health = 100;

        [Header("Combat")]
        [SerializeField, Min(0)] private int m_minDamage = 1;
        [SerializeField, Min(0)] private int m_maxDamage = 10;
        [SerializeField, Min(0)] private int m_armor;
        [SerializeField, Range(0f, 1f)] private float m_critChance;
        [SerializeField] private Element m_element;

        [Header("Weapon")]
        [SerializeField] private string m_weaponName;
        [SerializeField, Min(0)] private int m_weaponMinDamage = 1;
        [SerializeField, Min(0)] private int m_weaponMaxDamage = 10;
        [SerializeField] private Element m_weaponElement;

        public string characterName => m_characterName;
        public int health => m_health;
        public int minDamage => m_minDamage;
        public int maxDamage => m_maxDamage;
        public int armor => m_armor;
        public float critChance => m_critChance;
        public Element element => m_element;

        public string weaponName => m_weaponName;
        public int weaponMinDamage => m_weaponMinDamage;
        public int weaponMaxDamage => m_weaponMaxDamage;
        public Element weaponElement => m_weaponElement;
    }
}
