using System;
using Microsoft.Xna.Framework;
using BombermanAdventure.Events.Explosions;
using BombermanAdventure.Models.GameModels.Players;

namespace BombermanAdventure.Models.GameModels.Explosions
{
    class ElectricExplosion : AbstractExplosion
    {
        public ElectricExplosion(Game game, Player player, Vector3 position, GameTime gameTime)
            : base(game, player, position, gameTime)
        {
            color = new Vector3(255, 255, 0);
            Initialize();
        }

        protected override void RegisterEvent(GameTime gameTime)
        {
            models.RegisterEvent(new ElectricExplosionEvent(this, player), gameTime);
        }
    }
}
