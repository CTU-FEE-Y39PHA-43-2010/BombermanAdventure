using System;
using Microsoft.Xna.Framework;

namespace BombermanAdventure.Models.GameModels.Walls
{
    class ElectricWall : AbstractWall
    {
        public ElectricWall(Game game, int x, int y) : base(game, x, y) { }

        public override void Initialize()
        {
            modelName = "Models/Walls/tornadoWall";
            modelScale = 1f;
            modelRotation = new Vector3();
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            modelRotation.Y += 10f;
        }

        public override void OnEvent(Events.CommonEvent ieEvent, GameTime gameTime)
        {
            //throw new NotImplementedException();
        }
    }
}
