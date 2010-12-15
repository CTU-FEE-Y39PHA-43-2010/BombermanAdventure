using System;
using Microsoft.Xna.Framework;
using BombermanAdventure.Models.GameModels.Players;
using BombermanAdventure.Events.Bombs;
using BombermanAdventure.Events;
using BombermanAdventure.Models.GameModels.Explosions;

namespace BombermanAdventure.Models.GameModels.Bombs
{
    class MudBomb : AbstractBomb
    {
        public MudBomb(Game game, Vector3 modelPosition, Player player, GameTime gameTime) : base(game, modelPosition, player, gameTime) { }

        public override void Initialize()
        {
            modelName = "Models/Bombs/MudBomb";
            modelScale = 0.2f;
            base.Initialize();
        }

        protected override void RegisterEvent(GameTime gameTime)
        {
            var mudExplosion = new MudExplosion(game, player, modelPosition, gameTime);
            models.AddExplosion(mudExplosion);
            models.RegisterEvent(new MudBombExplosionEvent(this, player), gameTime);
        }

        public override void OnEvent(CommonEvent ieEvent, GameTime gameTime)
        {
            RegisterEvent(gameTime);
        }
    }
}
