using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BombermanAdventure.Models
{
    class HUD : DrawableGameComponent
    {
        ModelList models;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        Texture2D texture;

        public HUD(Game game) :base(game) {
            Initialize();
        }

        public override void Initialize()
        {
            base.Initialize();
            LoadContent();
        }

        protected override void LoadContent()
        {
            models = ModelList.GetInstance();
            spriteBatch = new SpriteBatch(GraphicsDevice);
            texture = Game.Content.Load<Texture2D>("bomberHUD");
            spriteFont = Game.Content.Load<SpriteFont>("BAFont");
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            // vykreslime texturu
            spriteBatch.Draw(texture, new Rectangle(0,0,100,100), Color.Green);
            // vzkreslime text
            spriteBatch.DrawString(spriteFont, String.Format("{0}%[{1},{2}]~[{3},{4}]", models.Player.Life, models.Player.ModelPosition.X, models.Player.ModelPosition.Z, models.Player.ModelPosition.X % 20, models.Player.ModelPosition.Z % 20), new Vector2(20, 50), Color.White);
            spriteBatch.End();
        }
    }
}
