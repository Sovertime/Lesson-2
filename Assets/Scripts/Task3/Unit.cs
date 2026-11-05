using UnityEngine;

namespace Task3
{
    public class Unit
    {
        private string m_name;
        private int m_minDamage;
        private int m_maxDamage;
        private int m_health;

        public string name => m_name;
        public int health => m_health;

        public Unit(string name, int health, int minDamage, int maxDamage)
        {
            m_name = name;
            m_health = health;
            m_minDamage = minDamage;
            m_maxDamage = maxDamage;
        }

        public int RollDamage()
        {
            return Random.Range(m_minDamage, m_maxDamage + 1);
        }

        public void TakeDamage(int damage)
        {
            m_health = Mathf.Max(0, m_health - damage);
        }
    }
}
