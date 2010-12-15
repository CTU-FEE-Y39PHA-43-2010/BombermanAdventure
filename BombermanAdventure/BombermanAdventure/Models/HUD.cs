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

        public HUD(Game game)
            : base(game)
        {
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
            texture = Game.Content.Load<Texture2D>(@"images\head");
            spriteFont = Game.Content.Load<SpriteFont>("BAFont");
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(texture, new Rectangle(0, 0, 100, 100), Color.White);
            spriteBatch.DrawString(spriteFont, models.Player.PlayerProfile.Life + " %", new Vector2(150, 30), Color.White);
            
            spriteBatch.DrawString(spriteFont, String.Format("speed: {0} bombs: {1} range: {2} score: {3}", models.Player.PlayerProfile.Speed, models.Player.PlayerProfile.PossibleBombsCount, models.Player.PlayerProfile.BombRange, models.Player.PlayerProfile.Score), new Vector2(320, 30), Color.White);
            spriteBatch.End();
        }
    }
}
