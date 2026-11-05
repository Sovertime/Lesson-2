using UnityEngine;

using Element = Combat.DamageCalculator.Element;
using Data.Weapons;

namespace Data.Characters
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
        [SerializeField] private WeaponData m_startWeapon;

        public WeaponData startWeapon => m_startWeapon;
        public string characterName => m_characterName;
        public int health => m_health;
        public int minDamage => m_minDamage;
        public int maxDamage => m_maxDamage;
        public int armor => m_armor;
        public float critChance => m_critChance;
        public Element element => m_element;
    }
}
