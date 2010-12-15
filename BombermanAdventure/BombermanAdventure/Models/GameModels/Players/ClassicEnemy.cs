using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BombermanAdventure.Events;
using Microsoft.Xna.Framework;

namespace BombermanAdventure.Models.GameModels.Players
{
    class ClassicEnemy : AbstractEnemy 
    {
        public ClassicEnemy(Game game, int x, int y) : base(game, .3f, x, y)
        {
            live = 100;
        }

        public override void Initialize()
        {
            base.modelName = "Models/Enemies/enemy1";
            base.Initialize();
        }

        public override void OnEvent(CommonEvent ieEvent, GameTime gameTime)
        {
            if (ieEvent is Events.Collisions.WallCollisionEvent)
            {
                var newOrientation = GetNextOrientation();
                SetOrientation(orientation, newOrientation);
                orientation = newOrientation;
                modelPosition = prevModelPosition;
            }
            base.OnEvent(ieEvent, gameTime);
        }
    }
}
