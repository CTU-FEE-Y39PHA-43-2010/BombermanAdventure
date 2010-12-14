using BombermanAdventure.GameObjects;
using Microsoft.Xna.Framework;

namespace BombermanAdventure.Models.GameModels.Players
{
    abstract class AbstractPlayer : AbstractGameModel
    {

        protected Profile playerProfile;

        protected int possibleBombsCount;
        public int PossibleBombsCount
        {
            get { return playerProfile.PossibleBombsCount; }
            set { playerProfile.PossibleBombsCount = value; }
        }
        protected int bombsCount;
        public int BombsCount
        {
            get { return bombsCount; }
            set { bombsCount = value; }
        }
        
        protected string name;
        public string Name
        {
            get { return playerProfile.Name; }
        }

        protected int score;
        public int Score
        {
            get { return playerProfile.Score; }
            set { playerProfile.Score = value; }
        }

        protected int level;
        public int Level
        {
            get { return playerProfile.Level; }
            set { playerProfile.Level = value; }
        }

        protected float speed;
        public float Speed
        {
            get { return playerProfile.Speed; }
            set { playerProfile.Speed = value; }

        }

        protected int armor;
        public int Armor
        {
            get { return playerProfile.Armor; }
            set { playerProfile.Armor = value; }
        }

        protected int life;
        public int Life
        {
            get { return playerProfile.Life; }
            set { playerProfile.Life = value; }
        }

        protected int bombRange;
        public int BombRange
        {
            get { return playerProfile.BombRange; }
            set { playerProfile.BombRange = value; }
        }

        protected bool hasCommonBomb;
        public bool HasCommonBomb
        {
            get { return playerProfile.HasCommonBomb; }
            set { playerProfile.HasCommonBomb = value; }
        }

        protected bool hasElectricBomb;
        public bool HasElectricBomb
        {
            get { return playerProfile.HasElectricBomb; }
            set { playerProfile.HasElectricBomb = value; }
        }

        protected bool hasMudBomb;
        public bool HasMudBomb
        {
            get { return playerProfile.HasMudBomb; }
            set { playerProfile.HasMudBomb = value; }
        }

        protected bool hasWaterBomb;
        public bool HasWaterBomb
        {
            get { return playerProfile.HasWaterBomb; }
            set { playerProfile.HasWaterBomb = value; }
        }

        protected AbstractPlayer(Game game, Profile profile, int x, int y)
            : base(game)
        {
            base.modelPosition = new Vector3(x * 20, 10, y * 20);
            playerProfile = profile;
        }

        public abstract void GoLeft();
        public abstract void GoRight();
        public abstract void GoUp();
        public abstract void GoDown();

        public abstract void PutBomb(GameTime gameTime);
        public abstract void Fire();
    }
}
