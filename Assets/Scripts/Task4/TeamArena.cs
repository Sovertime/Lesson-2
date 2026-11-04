using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using DamageCalculator = Combat.DamageCalculator;
using Data.Characters;

namespace Task4
{
    public class TeamArena : MonoBehaviour
    {
        [Header("Arena Settings")]
        [SerializeField, Min(0f)] private float m_roundDelay = 1.5f;
        [SerializeField, Min(1)] private int m_rounds = 30;
        [SerializeField, Min(1)] private int m_heroCount = 3;
        [SerializeField, Min(1)] private int m_enemyCount = 3;
        
        [Header("Characters")]
        [SerializeField] private CharacterData[] m_heroData;
        [SerializeField] private CharacterData[] m_enemyData;
        
        private List<Unit> m_enemies = new List<Unit>();
        private List<Unit> m_heroes = new List<Unit>();
        private WaitForSeconds m_roundDelayWait;

        private void Awake()
        {
           m_roundDelayWait = new WaitForSeconds(m_roundDelay);
            
           CreateEnemies();
           CreateHeroes();
           
           PrintFight();
        }
        
        private void CreateEnemies()
        {
            m_enemies = new List<Unit>(UnitFactory.CreateRandom(m_enemyData, m_enemyCount));
        }

        private void CreateHeroes()
        {
            m_heroes = new List<Unit>(UnitFactory.CreateRandom(m_heroData, m_heroCount));
        }
        
        private void Start()
        {
            StartCoroutine(Fight());
        }

        private IEnumerator Fight()
        {
            int currentRound = 1;

            while (currentRound <= m_rounds)
            {
                Debug.LogWarning($"Раунд: {currentRound}");
                
                int maxCount = Mathf.Max(m_heroes.Count, m_enemies.Count);

                for (int i = 0; i < maxCount; i++)
                {
                    yield return m_roundDelayWait;
                    
                    if (i < m_heroes.Count)
                    {
                        Attack(m_heroes[i], m_enemies, "green");

                        if (GetRandomUnit(m_enemies) == null)
                        {
                            Debug.LogWarning("Герои победили!");
                            PrintResults();
                            
                            yield break;
                        }
                    }
                    
                    if (i < m_enemies.Count)
                    {
                        Attack(m_enemies[i], m_heroes, "red");

                        if (GetRandomUnit(m_heroes) == null)
                        {
                            Debug.LogWarning("Враги победили!");
                            PrintResults();
                            
                            yield break;
                        }
                    }
                }
                
                currentRound++;
            }
            
            Debug.LogWarning("Ничья! Победила дружба");
            PrintResults();
        }

        private void Attack(Unit attacker, List<Unit> targets, string color)
        {
            if (attacker.health <= 0 || attacker.weapon == null)
                return;
            
            Unit target = GetRandomUnit(targets);

            if (target == null)
                return;

            int baseDamage = Random.Range(attacker.weapon.minDamage, attacker.weapon.maxDamage + 1);
            
            int damage = DamageCalculator.Calculate(baseDamage, target.armor, attacker.weapon.element, target.element, attacker.critChance);
            target.TakeDamage(damage);

            Debug.Log($"<color={color}>{attacker.name} [{attacker.weapon.name}] атакует {target.name}, урон: {damage} | осталось HP: {target.health}</color>");

            if (target.health == 0)
                Debug.LogWarning($"{target.name} погиб!");
            
            TryLootWeapon(attacker, target);
        }

        private Unit GetRandomUnit(List<Unit> units)
        {
            List<Unit> aliveUnits = new List<Unit>();
            
            foreach (Unit unit in units)
            {
                if (unit.health > 0)
                {
                    aliveUnits.Add(unit);
                }
            }
            
            return aliveUnits.Count == 0 ? null : aliveUnits[Random.Range(0, aliveUnits.Count)];
        }

        private void TryLootWeapon(Unit attacker, Unit target)
        {
            if (target.health > 0)
                return;

            Weapon currentWeapon = attacker.weapon;
            Weapon droppedWeapon = target.weapon;

            if (droppedWeapon == null)
                return;

            int currentDamage = currentWeapon.minDamage + currentWeapon.maxDamage;
            int droppedDamage = droppedWeapon.minDamage + droppedWeapon.maxDamage;

            if (droppedDamage > currentDamage)
            {
                attacker.weapon = droppedWeapon;
                target.weapon = null;
                
                Debug.LogWarning($"{attacker.name} подобрал {droppedWeapon.name} вместо {currentWeapon.name}!");
            }
        }

        private void PrintFight()
        {
            Debug.LogWarning("=== КОМАНДА ГЕРОЕВ ===");

            foreach (Unit hero in m_heroes)
            {
                Debug.Log($"<color=green>{hero.name} | HP: {hero.health} | Броня: {hero.armor} | Оружие: {hero.weapon?.name ?? "Без оружия"}</color>");
            }
            
            Debug.LogWarning("=== КОМАНДА ВРАГОВ ===");

            foreach (Unit enemy in m_enemies)
            {
                Debug.Log($"<color=red>{enemy.name} | HP: {enemy.health} | Броня: {enemy.armor} | Оружие: {enemy.weapon?.name ?? "Без оружия"}</color>");
            }
        }

        private void PrintResults()
        {
            int aliveHeroes = 0;
            int aliveEnemies = 0;
            
            Debug.LogWarning("=== ИТОГИ БОЯ ===");
            
            Debug.LogWarning("=== КОМАНДА ГЕРОЕВ ===");

            foreach (Unit hero in m_heroes)
            {
                if (hero.health > 0)
                    aliveHeroes++;
                
                Debug.Log($"<color=green>{hero.name} | HP: {hero.health} | Оружие: {hero.weapon?.name ?? "Без оружия"}</color>");
            }
            
            Debug.LogWarning("=== ВРАГИ ===");

            foreach (Unit enemy in m_enemies)
            {
                if (enemy.health > 0)
                    aliveEnemies++;
                
                Debug.Log($"<color=red>{enemy.name} | HP: {enemy.health} | Оружие: {enemy.weapon?.name ?? "Без оружия"}</color>");
            }
            
            Debug.LogWarning($"Выжило героев: {aliveHeroes}/{m_heroes.Count} | " +
                             $"Выжило врагов: {aliveEnemies}/{m_enemies.Count}");
        }
    }
}
