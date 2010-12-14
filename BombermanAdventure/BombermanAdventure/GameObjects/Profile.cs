using System;

namespace BombermanAdventure.GameObjects
{
    [Serializable]
    public class Profile
    {
        private readonly string _name;
        private int _score;
        private int _life;
        private int _level;
        private int _possibleBombsCount;

        public int PossibleBombsCount
        {
            get { return _possibleBombsCount; }
            set { _possibleBombsCount = value; }
        }

        public string Name
        {
            get { return _name; }
        }

        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }

        public int Life
        {
            get { return _life; }
            set { _life = value; }
        }

        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }

        private float _speed;
        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        private int _armor;
        public int Armor
        {
            get { return _armor; }
            set { _armor = value; }
        }

        private int _bombRange;
        public int BombRange
        {
            get { return _bombRange; }
            set { _bombRange = value; }
        }

        private bool _hasCommonBomb;
        public bool HasCommonBomb
        {
            get { return _hasCommonBomb; }
            set { _hasCommonBomb = value; }
        }

        private bool _hasElectricBomb;
        public bool HasElectricBomb
        {
            get { return _hasElectricBomb; }
            set { _hasElectricBomb = value; }
        }

        private bool _hasMudBomb;
        public bool HasMudBomb
        {
            get { return _hasMudBomb; }
            set { _hasMudBomb = value; }
        }

        private bool _hasWaterBomb;
        public bool HasWaterBomb
        {
            get { return _hasWaterBomb; }
            set { _hasWaterBomb = value; }
        }

        public Profile(string name)
        {
            _name = name;
            _score = 0;
            _life = 100;
            _level = 1;
            _possibleBombsCount = 1;
            _armor = 0;
            _speed = 1;
            _bombRange = 1;
            _hasCommonBomb = true;
            _hasElectricBomb = true;
            _hasMudBomb = true;
            _hasWaterBomb = true;
        }

    }
}
