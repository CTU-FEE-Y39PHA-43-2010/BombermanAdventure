﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BombermanAdventure.Models.GameModels.Walls;
using Microsoft.Xna.Framework;

namespace BombermanAdventure.Models.GameModels.Bunuses
{
    class SpeedBonus : AbstractBonus
    {
        public SpeedBonus(Game game, AbstractWall wall) : base(game, wall) { }

        public override void Initialize()
        {
            base.modelName = "Models/Bonuses/blade";
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
