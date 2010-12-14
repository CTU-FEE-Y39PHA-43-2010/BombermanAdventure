using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using BombermanAdventure.Models.GameModels.Players;
using BombermanAdventure.Events.Bombs;
using BombermanAdventure.Events;

namespace BombermanAdventure.Models.GameModels.Bombs
{
    class ElectricBomb : AbstractBomb
    {
        public ElectricBomb(Game game, Vector3 modelPosition, Player player, GameTime gameTime) : base(game, modelPosition, player, gameTime) { }

        public override void Initialize()
        {
            base.modelName = "Models/Bombs/ElectricBomb";
            base.modelScale = 0.2f;
            base.Initialize();
        }

        protected override void RegisterEvent(GameTime gameTime)
        {
            base.models.RegisterEvent(new ElectricBombExplosionEvent(this, player), gameTime);
        }

        public override void OnEvent(CommonEvent ieEvent, GameTime gameTime)
        {
            //throw new NotImplementedException();
        }
    }
}
