using System;
using Microsoft.Xna.Framework;
using BombermanAdventure.Models.GameModels.Players;
using BombermanAdventure.Events.Bombs;
using BombermanAdventure.Events;
using BombermanAdventure.Models.GameModels.Explosions;

namespace BombermanAdventure.Models.GameModels.Bombs
{
    class ElectricBomb : AbstractBomb
    {
        public ElectricBomb(Game game, Vector3 modelPosition, Player player, GameTime gameTime) : base(game, modelPosition, player, gameTime) { }

        public override void Initialize()
        {
            modelName = "Models/Bombs/ElectricBomb";
            modelScale = 0.2f;
            base.Initialize();
        }

        protected override void RegisterEvent(GameTime gameTime)
        {
            var electricExplosion = new ElectricExplosion(game, player, modelPosition, gameTime);
            models.AddExplosion(electricExplosion);
            models.RegisterEvent(new ElectricBombExplosionEvent(this, player), gameTime);
        }

        public override void OnEvent(CommonEvent ieEvent, GameTime gameTime)
        {
            RegisterEvent(gameTime);
        }

    }
}
