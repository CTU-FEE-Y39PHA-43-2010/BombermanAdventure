using System;
using Microsoft.Xna.Framework;
using BombermanAdventure.Events.Explosions;
using BombermanAdventure.Models.GameModels.Players;

namespace BombermanAdventure.Models.GameModels.Explosions
{
    class WaterExplosion : AbstractExplosion
    {
        public WaterExplosion(Game game, Player player, Vector3 position, GameTime gameTime)
            : base(game, player, position, gameTime)
        {
            color = new Vector3(0, 0, 255);
            Initialize();
        }

        protected override void RegisterEvent(GameTime gameTime)
        {
            models.RegisterEvent(new WaterExplosionEvent(this, player), gameTime);
        }

        public override Player.Bombs BombType()
        {
            return Player.Bombs.Water;
        }
    }
}
