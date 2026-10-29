using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Task4
{
    public class TeamArena : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float roundDelay = 1.5f;
        
        private List<Unit> _enemies = new List<Unit>();
        private List<Unit> _heroes = new List<Unit>();

        private const int Rounds = 30;

        private void Awake()
        {
           CreateEnemies();
           CreateHeroes();
           
           PrintFight();
        }

        private void CreateEnemies()
        {
            while (_enemies.Count < 3)
            {
                int randomEnemy = Random.Range(0, 3);

                switch (randomEnemy)
                {
                    case 0:
                        _enemies.Add(new Unit("Гоблин", 50, 4, 0.15f, DamageCalculator.Element.None, 
                            new Weapon("Кинжал", 4, 8, DamageCalculator.Element.None)));
                        break;
                    case 1:
                        _enemies.Add(new Unit("Орк", 140, 8, 0.25f, DamageCalculator.Element.Fire,
                            new Weapon("Топор", 8, 14, DamageCalculator.Element.Fire)));
                        break;
                    case 2:
                        _enemies.Add(new Unit("Тролль", 80, 5, 0.2f, DamageCalculator.Element.Ice, 
                            new Weapon("Копье", 6, 12, DamageCalculator.Element.Ice)));
                        break;
                }
            }
        }

        private void CreateHeroes()
        {
            while (_heroes.Count < 3)
            {
                int randomHero = Random.Range(0, 2);
            
                switch (randomHero)
                {
                    case 0:
                        _heroes.Add(new Unit("Воин", 100, 10, 0.2f, DamageCalculator.Element.Fire, 
                            new Weapon("Меч", 6, 12, DamageCalculator.Element.Fire)));
                        break;
                    case 1:
                        _heroes.Add(new Unit("Маг", 130, 3, 0.35f, DamageCalculator.Element.Ice,
                            new Weapon("Посох", 4, 20, DamageCalculator.Element.Ice)));
                        break;
                }
            }
        }

        void Start()
        {
            StartCoroutine(Fight());
        }

        IEnumerator Fight()
        {
            int move = 1;
            
            while (move <= Rounds)
            {
                Debug.LogWarning($"Раунд: {move}");

                for (int i = 0; i < _heroes.Count; i++)
                {
                    yield return new WaitForSeconds(roundDelay);
                    
                    Attack(_heroes[i], _enemies, "green");

                    if (GetRandomUnit(_enemies) == null)
                    {
                        Debug.LogWarning("Герои победили!");
                        PrintResults();
                        
                        yield break;
                    }
                    
                    Attack(_enemies[i], _heroes, "red");

                    if (GetRandomUnit(_heroes) == null)
                    {
                        Debug.LogWarning("Враги победили!");
                        PrintResults();
                        
                        yield break;
                    }
                }
                
                move++;
            }
            Debug.LogWarning("Ничья! Победила дружба");
            
            PrintResults();
        }

        private void Attack(Unit attacker, List<Unit> targets, string color)
        {
            if (attacker.Health <= 0 || attacker.Weapon == null)
                return;
            
            Unit target = GetRandomUnit(targets);

            if (target == null)
                return;

            int baseDamage = Random.Range(attacker.Weapon.MinDamage, attacker.Weapon.MaxDamage + 1);
            
            int damage = DamageCalculator.Calculate(baseDamage, target.Armor, attacker.Weapon.Element, target.Element, attacker.CritChance);
            target.TakeDamage(damage);

            Debug.Log($"<color={color}>{attacker.Name} [{attacker.Weapon.Name}] атакует {target.Name}, урон: {damage} | осталось HP: {target.Health}</color>");

            if (target.Health == 0)
                Debug.LogWarning($"{target.Name} погиб!");
            
            TryLootWeapon(attacker, target);
        }

        private Unit GetRandomUnit(List<Unit> units)
        {
            List<Unit> aliveUnits = new List<Unit>();
            
            foreach (Unit unit in units)
            {
                if (unit.Health > 0)
                {
                    aliveUnits.Add(unit);
                }
            }
            
            return aliveUnits.Count == 0 ? null : aliveUnits[Random.Range(0, aliveUnits.Count)];
        }

        private void TryLootWeapon(Unit attacker, Unit target)
        {
            if (target.Health > 0)
                return;

            Weapon currentWeapon = attacker.Weapon;
            Weapon droppedWeapon = target.Weapon;

            if (droppedWeapon == null)
                return;

            int currentDamage = currentWeapon.MinDamage + currentWeapon.MaxDamage;
            int droppedDamage = droppedWeapon.MinDamage + droppedWeapon.MaxDamage;

            if (droppedDamage > currentDamage)
            {
                attacker.Weapon = droppedWeapon;
                target.Weapon = null;
                
                Debug.LogWarning($"{attacker.Name} подобрал {droppedWeapon.Name} вместо {currentWeapon.Name}!");
            }
        }

        private void PrintFight()
        {
            Debug.LogWarning("=== КОМАНДА ГЕРОЕВ ===");

            foreach (Unit hero in _heroes)
            {
                Debug.Log($"<color=green>{hero.Name} | HP: {hero.Health} | Броня: {hero.Armor} | Оружие: {hero.Weapon.Name}</color>");
            }
            
            Debug.LogWarning("=== КОМАНДА ВРАГОВ ===");

            foreach (Unit enemy in _enemies)
            {
                Debug.Log($"<color=red>{enemy.Name} | HP: {enemy.Health} | Броня: {enemy.Armor} | Оружие: {enemy.Weapon.Name}</color>");
            }
        }

        private void PrintResults()
        {
            int aliveHeroes = 0;
            int aliveEnemies = 0;
            
            Debug.LogWarning("=== ИТОГИ БОЯ ===");
            
            Debug.LogWarning("=== КОМАНДА ГЕРОЕВ ===");

            foreach (Unit hero in _heroes)
            {
                if (hero.Health > 0)
                    aliveHeroes++;
                
                Debug.Log($"<color=green>{hero.Name} | HP: {hero.Health} | Оружие: {hero.Weapon?.Name ?? "Без оружия"}</color>");
            }
            
            Debug.LogWarning("=== ВРАГИ ===");

            foreach (Unit enemy in _enemies)
            {
                if (enemy.Health > 0)
                    aliveEnemies++;
                
                Debug.Log($"<color=red>{enemy.Name} | HP: {enemy.Health} | Оружие: {enemy.Weapon?.Name ?? "Без оружия"}</color>");
            }
            
            Debug.LogWarning($"Выжило героев: {aliveHeroes}/{_heroes.Count} | " +
                             $"Выжило врагов: {aliveEnemies}/{_enemies.Count}");
        }
    }
}
