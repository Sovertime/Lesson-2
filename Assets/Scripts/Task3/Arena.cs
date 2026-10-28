using UnityEngine;
using System.Collections;

public class Arena : MonoBehaviour
{
    private Unit _hero;
    private Unit _enemy;
    
    [SerializeField] private HeroClass heroClass;

    private const int Rounds = 15;

    public enum HeroClass
    {
        Warrior,
        Mage
    }

    private void Awake()
    {
        switch (heroClass)
        {
            case HeroClass.Warrior:
                _hero = new Unit("Воин", 100, 10, 16);
                break;
            
            case HeroClass.Mage:
                _hero = new Unit("Маг", 130, 6, 12);
                break;
        }
        
        int randomEnemy = Random.Range(0, 3);

        switch (randomEnemy)
        {
            case 0:
                _enemy = new Unit("Гоблин", 50, 4, 8);
                break;
            
            case 1:
                _enemy = new Unit("Орк", 80, 5, 10);
                break;
            
            case 2:
                _enemy = new Unit("Тролль", 140, 8, 12);
                break;
        }
    }

    void Start()
    {
        StartCoroutine(Fight());
    }

    IEnumerator Fight()
    {
        Debug.LogWarning($"{_hero.Name} ({_hero.Health} HP) против {_enemy.Name} ({_enemy.Health} HP)");
        int move = 1;
        
        while (_hero.Health > 0 && _enemy.Health > 0)
        {
            int heroDamage = Random.Range(_hero.MinDamage, _hero.MaxDamage + 1);
            _enemy.TakeDamage(heroDamage);

            Debug.Log($"Раунд {move}: {_hero.Name} наносит {heroDamage} урона, у противника {_enemy.Health} HP");
            
            if (_enemy.Health <= 0)
            {
                Debug.LogWarning($"Победа! У героя осталось {_hero.Health} HP");
                yield break;
            }
            
            int enemyDamage = Random.Range(_enemy.MinDamage, _enemy.MaxDamage + 1);
            _hero.TakeDamage(enemyDamage);

            Debug.Log($"{_enemy.Name} наносит {enemyDamage} урона, у героя {_hero.Health} HP");
            
            if (_hero.Health <= 0)
            {
                Debug.LogWarning($"Поражение! У врага осталось {_enemy.Health} HP");
                yield break;
            }
            
            if (move >= Rounds)
            {
                Debug.LogWarning($"Ничья! У героя осталось {_hero.Health} HP; У врага осталось {_enemy.Health} HP");
                yield break;
            }
            
            move++;
            
            yield return new WaitForSeconds(3f);
        }
    }
}
