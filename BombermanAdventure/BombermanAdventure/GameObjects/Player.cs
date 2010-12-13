using System;

namespace BombermanAdventure.GameObjects
{
    [Serializable]
    public class Player
    {
        private readonly string _name;
        private int _score;
        private int _healt;
        private int _level;

        public string Name 
        {
            get { return _name; }
        }

        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }

        public int Healt
        {
            get { return _healt; }
            set { _healt = value; }
        }

        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }

        public Player(string name)
        {
            _name = name;
            _score = 0;
            _healt = 100;
            _level = 1;
        }

    }
}
