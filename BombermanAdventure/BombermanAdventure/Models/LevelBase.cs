using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BombermanAdventure.Generators;

namespace BombermanAdventure.Models
{
    class LevelBase : DrawableGameComponent
    {
        ModelList models;
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;
        private Texture2D _image;
        private Texture2D _win;
        private Texture2D _gradientTexture;

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

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Game.Content.Load<SpriteFont>("BAFont");
            _image = Game.Content.Load<Texture2D>(@"images\over");
            _win = Game.Content.Load<Texture2D>(@"images\win");
            _gradientTexture = Game.Content.Load<Texture2D>(@"images\gradient");
        }

        public override void Update(GameTime gameTime)
        {
            models.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1.0f, 0);
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            models.DrawLabyrinth(gameTime);
            models.DrawWalls(gameTime);
            models.DrawBonuses(gameTime);
            models.DrawBombs(gameTime);
            models.DrawEnemies(gameTime);
            models.DrawExplosions(gameTime);
            if (models.Player != null && !models.Player.Dead && !models.Player.Winner)
            {
                models.Player.Draw(gameTime);
            }

            GraphicsDevice.DepthStencilState = DepthStencilState.None;
            models.Hud.Draw(gameTime);

            if (models.Player == null || models.Player.Dead)
            {
                //draw girl
                var viewport = Game.GraphicsDevice.Viewport;
                const string message = "GAME OVER";
                var viewportSize = new Vector2(viewport.Width, viewport.Height + 500);
                var textSize = _font.MeasureString(message);
                var textPosition = (viewportSize - textSize)/2;
                var origin = new Vector2(0, _font.LineSpacing/2.0f);

                var color = Color.White;
                var colorB = Color.Black*.6f;

                // Draw the background rectangle.
                var girlRectangle = new Rectangle(viewport.Width/2 - 150, viewport.Height/2 - 200, 300, 392);
                _spriteBatch.Begin();
                _spriteBatch.Draw(_gradientTexture, new Rectangle(0, 0, viewport.Width, viewport.Height), colorB);
                _spriteBatch.Draw(_image, girlRectangle,
                                  Color.White);
                _spriteBatch.DrawString(_font, message, textPosition, color, 0, origin, 1, SpriteEffects.None, 0);
                _spriteBatch.End();
            }

            if (models.Player.Winner)
            {
                //draw girl
                var viewport = Game.GraphicsDevice.Viewport;
                const string message = "CONGRATULATION, YOU WIN";
                var viewportSize = new Vector2(viewport.Width, viewport.Height + 500);
                var textSize = _font.MeasureString(message);
                var textPosition = (viewportSize - textSize) / 2;
                var origin = new Vector2(0, _font.LineSpacing / 2.0f);

                var color = Color.White;
                var colorB = Color.Black * .6f;

                // Draw the background rectangle.
                var girlRectangle = new Rectangle(viewport.Width / 2 - 154, viewport.Height / 2 - 200, 300, 392);
                _spriteBatch.Begin();
                _spriteBatch.Draw(_gradientTexture, new Rectangle(0, 0, viewport.Width, viewport.Height), colorB);
                _spriteBatch.Draw(_win, girlRectangle,
                                  Color.White);
                _spriteBatch.DrawString(_font, message, textPosition, color, 0, origin, 1, SpriteEffects.None, 0);
                _spriteBatch.End();
            }

        }
    }
}
