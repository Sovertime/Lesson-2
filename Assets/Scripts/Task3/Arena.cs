using UnityEngine;
using System.Collections;

namespace Task3
{
    public class Arena : MonoBehaviour
    {
        private Unit m_hero;
        private Unit m_enemy;
    
        [Header("Arena Settings")]
        [SerializeField] private HeroClass m_heroClass;
        [SerializeField, Min(0f)] private float m_roundDelay = 3f;
        [SerializeField, Min(1)] private int m_rounds = 15;

        public enum HeroClass
        {
            Warrior,
            Mage
        }

        private void Awake()
        {
            switch (m_heroClass)
            {
                case HeroClass.Warrior:
                    m_hero = UnitFactory.CreateWarrior();
                    break;
            
                case HeroClass.Mage:
                    m_hero = UnitFactory.CreateMage();
                    break;
            }
        
            int randomEnemy = Random.Range(0, 3);

            switch (randomEnemy)
            {
                case 0:
                    m_enemy = UnitFactory.CreateGoblin();
                    break;
            
                case 1:
                    m_enemy = UnitFactory.CreateOrc();
                    break;
            
                case 2:
                    m_enemy = UnitFactory.CreateTroll();
                    break;
            }
        }

        private void Start()
        {
            StartCoroutine(Fight());
        }

        private IEnumerator Fight()
        {
            Debug.LogWarning($"{m_hero.name} ({m_hero.health} HP) против {m_enemy.name} ({m_enemy.health} HP)");
            int currentRound = 1;
        
            while (m_hero.health > 0 && m_enemy.health > 0)
            {
                int heroDamage = m_hero.RollDamage();
                m_enemy.TakeDamage(heroDamage);

                Debug.Log($"Раунд {currentRound}: {m_hero.name} наносит {heroDamage} урона, у противника {m_enemy.health} HP");
            
                if (m_enemy.health <= 0)
                {
                    Debug.LogWarning($"Победа! У героя осталось {m_hero.health} HP");
                    yield break;
                }

                int enemyDamage = m_enemy.RollDamage();
                m_hero.TakeDamage(enemyDamage);

                Debug.Log($"{m_enemy.name} наносит {enemyDamage} урона, у героя {m_hero.health} HP");
            
                if (m_hero.health <= 0)
                {
                    Debug.LogWarning($"Поражение! У врага осталось {m_enemy.health} HP");
                    yield break;
                }
            
                if (currentRound >= m_rounds)
                {
                    Debug.LogWarning($"Ничья! У героя осталось {m_hero.health} HP; У врага осталось {m_enemy.health} HP");
                    yield break;
                }
            
                currentRound++;
            
                yield return new WaitForSeconds(m_roundDelay);
            }
        }
    }
}
