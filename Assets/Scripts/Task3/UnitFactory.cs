namespace Task3
{
    public static class UnitFactory
    {
        public static Unit CreateWarrior()
        {
            return new Unit("Воин", 100, 10, 16);
        }

        public static Unit CreateMage()
        {
            return new Unit("Маг", 130, 6, 12);
        }

        public static Unit CreateGoblin()
        {
            return new Unit("Гоблин", 50, 4, 8);
        }

        public static Unit CreateOrc()
        {
            return new Unit("Орк", 80, 5, 10);
        }

        public static Unit CreateTroll()
        {
            return new Unit("Тролль", 140, 8, 12);
        }
    }
}

