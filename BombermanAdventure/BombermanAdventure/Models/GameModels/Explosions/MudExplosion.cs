﻿using System;
using Microsoft.Xna.Framework;
using BombermanAdventure.Events.Explosions;
using BombermanAdventure.Models.GameModels.Players;

namespace BombermanAdventure.Models.GameModels.Explosions
{
    class MudExplosion : AbstractExplosion
    {
        public MudExplosion(Game game, Player player, Vector3 position, GameTime gameTime)
            : base(game, player, position, gameTime)
        {
            color = new Vector3(128, 0, 128);
            Initialize();
        }

        protected override void RegisterEvent(GameTime gameTime)
        {
            models.RegisterEvent(new EarthExplosionEvent(this, player), gameTime);
        }

        public override Player.Bombs BombType()
        {
            return Player.Bombs.Mud;
        }
    }
}
