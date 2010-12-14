using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BombermanAdventure.ScreenManagement.Screens
{
    class HelpScreen : MenuScreen
    {

        #region Initialization

        /// <summary>
        /// Constructor fills in the menu contents.
        /// </summary>
        public HelpScreen()
            : base("command help")
        {

            var backEntry = new MenuEntry("back", new Vector2(LeftM + 20, BombermanAdventureGame.ScreenHeight - BottomM - 30));
            backEntry.Selected += OnCancel;

            MenuEntries.Add(backEntry);
        }



        #endregion

        #region Handle Input


        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            var spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            var font = ScreenManager.Font;
            var origin = new Vector2(0, font.LineSpacing / 2.0f);
            spriteBatch.DrawString(font, "move:", new Vector2(LeftM + 25, TopM + 80), Color.White, 0,
                                       origin, 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "arrows", new Vector2(LeftM + 200, TopM + 80), Color.White, 0,
                                       origin, 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "shooting:", new Vector2(LeftM + 25, TopM + 100), Color.White, 0,
                                       origin, 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "space", new Vector2(LeftM + 200, TopM + 100), Color.White, 0,
                                       origin, 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "common bomb:", new Vector2(LeftM + 25, TopM + 120), Color.White, 0,
                                       origin, 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "z/y", new Vector2(LeftM + 200, TopM + 120), Color.White, 0,
                                       origin, 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "electrical bomb:", new Vector2(LeftM + 25, TopM + 140), Color.White, 0,
                                       origin, 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "x", new Vector2(LeftM + 200, TopM + 140), Color.White, 0,
                                       origin, 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "mud bomb:", new Vector2(LeftM + 25, TopM + 160), Color.White, 0,
                                       origin, 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "c", new Vector2(LeftM + 200, TopM + 160), Color.White, 0,
                                       origin, 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "water bomb:", new Vector2(LeftM + 25, TopM + 180), Color.White, 0,
                                       origin, 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "v", new Vector2(LeftM + 200, TopM + 180), Color.White, 0,
                                       origin, 1f, SpriteEffects.None, 0);
            
            spriteBatch.End();

        }

        #endregion

    }
}
