using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BombermanAdventure.ScreenManagement.Screens
{
    class ProfileDetailScreen : MenuScreen
    {

        #region Initialization

        /// <summary>
        /// Constructor fills in the menu contents.
        /// </summary>
        public ProfileDetailScreen()
            : base(BombermanAdventureGame.ActivePlayer.Name + " info")
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
            spriteBatch.DrawString(font, "score:", new Vector2(LeftM + 25, TopM + 80), Color.White, 0,
                                       origin, 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, BombermanAdventureGame.ActivePlayer.Score.ToString(), new Vector2(LeftM + 200, TopM + 80), Color.White, 0,
                                       origin, 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "life:", new Vector2(LeftM + 25, TopM + 100), Color.White, 0,
                                       origin, 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, BombermanAdventureGame.ActivePlayer.Life + " %", new Vector2(LeftM + 200, TopM + 100), Color.White, 0,
                                       origin, 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "level:", new Vector2(LeftM + 25, TopM + 120), Color.White, 0,
                                       origin, 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, BombermanAdventureGame.ActivePlayer.Level.ToString(), new Vector2(LeftM + 200, TopM + 120), Color.White, 0,
                                       origin, 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "possible bombs:", new Vector2(LeftM + 25, TopM + 140), Color.White, 0,
                                       origin, 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, BombermanAdventureGame.ActivePlayer.PossibleBombsCount.ToString(), new Vector2(LeftM + 200, TopM + 140), Color.White, 0,
                                       origin, 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "common bomb:", new Vector2(LeftM + 25, TopM + 160), Color.White, 0,
                                       origin, 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, BombermanAdventureGame.ActivePlayer.HasCommonBomb ? "yes" : "no", new Vector2(LeftM + 200, TopM + 160), Color.White, 0,
                                       origin, 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "electric bomb:", new Vector2(LeftM + 25, TopM + 180), Color.White, 0,
                                       origin, 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, BombermanAdventureGame.ActivePlayer.HasElectricBomb ? "yes" : "no", new Vector2(LeftM + 200, TopM + 180), Color.White, 0,
                                       origin, 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "mud bomb:", new Vector2(LeftM + 25, TopM + 200), Color.White, 0,
                                       origin, 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, BombermanAdventureGame.ActivePlayer.HasElectricBomb ? "yes" : "no", new Vector2(LeftM + 200, TopM + 200), Color.White, 0,
                                       origin, 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "water bomb:", new Vector2(LeftM + 25, TopM + 220), Color.White, 0,
                                       origin, 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, BombermanAdventureGame.ActivePlayer.HasElectricBomb ? "yes" : "no", new Vector2(LeftM + 200, TopM + 220), Color.White, 0,
                                       origin, 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "speed:", new Vector2(LeftM + 25, TopM + 240), Color.White, 0,
                                       origin, 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, BombermanAdventureGame.ActivePlayer.Speed.ToString(), new Vector2(LeftM + 200, TopM + 240), Color.White, 0,
                                       origin, 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "bomb range:", new Vector2(LeftM + 25, TopM + 260), Color.White, 0,
                                       origin, 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, BombermanAdventureGame.ActivePlayer.BombRange.ToString(), new Vector2(LeftM + 200, TopM + 260), Color.White, 0,
                                       origin, 1f, SpriteEffects.None, 0);
            

            spriteBatch.End();

        }

        #endregion

    }
}
