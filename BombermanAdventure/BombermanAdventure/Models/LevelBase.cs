using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BombermanAdventure.Generators;

namespace BombermanAdventure.Models
{
    class LevelBase : DrawableGameComponent
    {
        ModelList models;

        public LevelBase(Game game) : base(game) 
        {
            Initialize(game);
        }

        public void Initialize(Game game)
        {
            models = (new LevelGenerator()).GenerateLevel(game);
            models.Hud = new HUD(game);
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            models.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1.0f, 0);
            //GraphicsDevice.BlendState = BlendState.AlphaBlend;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            models.DrawLabyrinth(gameTime);
            models.DrawWalls(gameTime);
            models.DrawBombs(gameTime);
            models.DrawExplosions(gameTime);
            models.Player.Draw(gameTime);

            GraphicsDevice.DepthStencilState = DepthStencilState.None;
            models.Hud.Draw(gameTime);
        }

    }
}
