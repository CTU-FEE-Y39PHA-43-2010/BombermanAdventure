using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BombermanAdventure.Events;
using Microsoft.Xna.Framework;

namespace BombermanAdventure.Models.GameModels.Players
{
    class SuperEnemy : AbstractEnemy
    {
        public SuperEnemy(Game game, int x, int y)
            : base(game, .7f, x, y)
        {
            live = 200;
        }

        public override void Initialize()
        {
            base.modelName = "Models/Enemies/enemy2";
            base.Initialize();
        }
    }
}
