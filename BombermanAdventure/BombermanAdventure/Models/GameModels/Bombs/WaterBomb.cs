using System;
using Microsoft.Xna.Framework;
using BombermanAdventure.Models.GameModels.Players;
using BombermanAdventure.Events.Bombs;
using BombermanAdventure.Events;
using BombermanAdventure.Models.GameModels.Explosions;

namespace BombermanAdventure.Models.GameModels.Bombs
{
    class WaterBomb : AbstractBomb
    {
        public WaterBomb(Game game, Vector3 modelPosition, Player player, GameTime gameTime) : base(game, modelPosition, player, gameTime) { }

        public override void Initialize()
        {
            modelName = "Models/Bombs/WaterBomb";
            modelScale = 0.2f;
            base.Initialize();
        }

        protected override void RegisterEvent(GameTime gameTime)
        {
            var waterExplosion = new WaterExplosion(game, player, modelPosition, gameTime);
            models.AddExplosion(waterExplosion);
            models.RegisterEvent(new WaterBombExplosionEvent(this, player), gameTime);
        }

        public override void OnEvent(CommonEvent ieEvent, GameTime gameTime)
        {
            RegisterEvent(gameTime);
        }
    }
}
