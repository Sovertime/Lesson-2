using Data.Characters;

namespace Task3
{
    public static class UnitFactory
    {
        public static Unit Create(CharacterData data)
        {
            if (data == null)
                throw new System.ArgumentNullException(nameof(data));
            
            return new Unit(
                data.characterName,
                data.health,
                data.minDamage,
                data.maxDamage
            );
        }
    }
}