using System;

namespace BombermanAdventure.GameObjects
{
    [Serializable]
    public class Profile
    {
        public bool InGame { get; set; }

        public int PossibleBombsCount { get; set; }

        public string Name { get; private set; }

        public int Score { get; set; }

        public int Life { get; set; }

        public int Level { get; set; }

        public float Speed { get; set; }

        public int Armor { get; set; }

        public int BombRange { get; set; }

        public bool HasCommonBomb { get; set; }

        public bool HasElectricBomb { get; set; }

        public bool HasMudBomb { get; set; }

        public bool HasWaterBomb { get; set; }

        public Profile(string name)
        {
            Name = name;
            Score = 0;
            Life = 100;
            Level = 1;
            PossibleBombsCount = 1;
            Armor = 0;
            Speed = 1;
            BombRange = 1;
            HasCommonBomb = true;
            HasElectricBomb = true;
            HasMudBomb = true;
            HasWaterBomb = true;
            InGame = false;
        }

    }
}
