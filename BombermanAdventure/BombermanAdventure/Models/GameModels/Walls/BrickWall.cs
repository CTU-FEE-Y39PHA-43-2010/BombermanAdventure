using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BombermanAdventure.Models.GameModels.Walls
{
    class BrickWall : AbstractWall
    {
        public BrickWall(Game game, int x, int y) : base(game, x, y) { }

        public override void Initialize()
        {
            base.modelName = "Models/Walls/commonWall";
            base.modelScale = 1f;
            modelRotation = new Vector3();
            base.Initialize();
        }

        public override void OnEvent(Events.CommonEvent ieEvent, GameTime gameTime)
        {
            //throw new NotImplementedException();
        }
    }
}
