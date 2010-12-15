using System;
using Microsoft.Xna.Framework;

namespace BombermanAdventure.Models.GameModels.Walls
{
    class WaterWall : AbstractWall
    {
        public WaterWall(Game game, int x, int y) : base(game, x, y) { }

        public override void Initialize()
        {
            modelName = "Models/Walls/waterWall";
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
