using Microsoft.Xna.Framework;

namespace BombermanAdventure.Models.GameModels.Walls
{
    class BrickWall : AbstractWall
    {
        public BrickWall(Game game, int x, int y) : base(game, x, y) { }

        public override void Initialize()
        {
            modelName = "Models/Walls/commonWall";
            modelScale = 1f;
            modelRotation = new Vector3();
            base.Initialize();
        }

        public override void OnEvent(Events.CommonEvent ieEvent, GameTime gameTime)
        {
            //throw new NotImplementedException();
        }
    }
}
