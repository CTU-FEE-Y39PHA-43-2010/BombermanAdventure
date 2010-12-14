using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using BombermanAdventure.Models.GameModels.Players;
using BombermanAdventure.Models.GameModels.Explosions;
using BombermanAdventure.Events.Bombs;
using BombermanAdventure.Events;

namespace BombermanAdventure.Models.GameModels.Bombs
{
    class CommonBomb : AbstractBomb
    {
        public CommonBomb(Game game, Vector3 modelPosition, Player player, GameTime gameTime) : base(game, modelPosition, player, gameTime) { }

        public override void Initialize()
        {
            base.modelName = "Models/Bombs/CommonBomb";
            base.modelScale = 0.2f;
            base.Initialize();
        }

        protected override void RegisterEvent(GameTime gameTime)
        {
            CommonExplosion ex = new CommonExplosion(game, player, modelPosition, gameTime);
            base.models.AddExplosion(ex);
            base.models.RegisterEvent(new CommonBombExplosionEvent(this, player), gameTime);
        }

        public override void OnEvent(CommonEvent ieEvent, GameTime gameTime)
        {
            RegisterEvent(gameTime);
        }
    }
}
