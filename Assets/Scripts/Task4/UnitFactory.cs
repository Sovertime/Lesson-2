using Random = UnityEngine.Random;

using Data.Characters;
using Data.Weapons;

namespace Task4
{
    public static class UnitFactory
    {
        public static Unit Create(CharacterData data)
        {
            WeaponData weaponData = data.startWeapon;

            Weapon weapon = new Weapon(
                weaponData.weaponName,
                weaponData.minDamage,
                weaponData.maxDamage,
                weaponData.element
            );

            return new Unit(
                data.characterName,
                data.health,
                data.armor,
                data.critChance,
                data.element,
                weapon
            );
        }

        public static Unit[] CreateRandom(CharacterData[] characters, int count)
        {
            if (characters == null || characters.Length == 0)
                throw new System.Exception("Массив персонажей пуст!");
            
            Unit[] units = new Unit[count];

            for (int i = 0; i < units.Length; i++)
            {
                int randomIndex = Random.Range(0, characters.Length);
                units[i] = Create(characters[randomIndex]);
            }
            return units;
        }
    }
}