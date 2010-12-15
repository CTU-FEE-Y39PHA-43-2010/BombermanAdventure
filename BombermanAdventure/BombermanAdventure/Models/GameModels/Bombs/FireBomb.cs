using System;
using Microsoft.Xna.Framework;
using BombermanAdventure.Models.GameModels.Players;
using BombermanAdventure.Models.GameModels.Explosions;
using BombermanAdventure.Events.Bombs;
using BombermanAdventure.Events;

namespace BombermanAdventure.Models.GameModels.Bombs
{
    class FireBomb : AbstractBomb
    {
        public FireBomb(Game game, Vector3 modelPosition, Player player, GameTime gameTime) : base(game, modelPosition, player, gameTime) { }

        public override void Initialize()
        {
            modelName = "Models/Bombs/FireBomb";
            modelScale = 0.2f;
            base.Initialize();
        }

        protected override void RegisterEvent(GameTime gameTime)
        {
            var fireExplosion = new FireExplosion(game, player, modelPosition, gameTime);
            models.AddExplosion(fireExplosion);
            models.RegisterEvent(new FireBombExplosionEvent(this, player), gameTime);
        }

        public override void OnEvent(CommonEvent ieEvent, GameTime gameTime)
        {
            RegisterEvent(gameTime);
        }
    }
}
