using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using BombermanAdventure.Events.Explosions;
using BombermanAdventure.Models.GameModels.Players;

namespace BombermanAdventure.Models.GameModels.Explosions
{
    class CommonExplosion : AbstractExplosion
    {
        public CommonExplosion(Game game, AbstractPlayer player, Vector3 position, GameTime gameTime)
            : base(game, player, position, gameTime)
        {
            color = new Vector3(150, 0, 0);
            Initialize();
        }

        protected override void RegisterEvent(GameTime gameTime)
        {
            base.models.RegisterEvent(new CommonExplosionEvent(this, player), gameTime);
        }
    }
}
