using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Xml.Serialization;
using BombermanAdventure.GameStorage;
using BombermanAdventure.GameObjects;

namespace BombermanAdventure.ScreenManagement.Screens
{
    class ProfileDetailScreen : MenuScreen
    {

        #region Initialization
        
        /// <summary>
        /// Constructor fills in the menu contents.
        /// </summary>
        public ProfileDetailScreen()
            : base(BombermanAdventureGame.ActivePlayer.Name + " info", false)
        {

            MenuEntry backEntry = new MenuEntry("back", new Vector2(leftM + 20, BombermanAdventureGame.ScreenHeight - bottomM - 30));
            backEntry.Selected += OnCancel;

            // Add entries to the menu.
            MenuEntries.Add(backEntry);
        }



        #endregion

        #region Handle Input

        
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            GraphicsDevice graphics = ScreenManager.GraphicsDevice;
            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;

            spriteBatch.Begin();

            SpriteFont font = ScreenManager.Font;
            Vector2 origin = new Vector2(0, font.LineSpacing / 2);
            spriteBatch.DrawString(font, "score:", new Vector2(leftM + 25, topM + 80), Color.White, 0,
                                       origin, 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, BombermanAdventureGame.ActivePlayer.Score.ToString(), new Vector2(leftM + 100, topM + 80), Color.White, 0,
                                       origin, 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "healt:", new Vector2(leftM + 25, topM + 100), Color.White, 0,
                                       origin, 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, BombermanAdventureGame.ActivePlayer.Healt.ToString() + " %" , new Vector2(leftM + 100, topM + 100), Color.White, 0,
                                       origin, 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "level:", new Vector2(leftM + 25, topM + 120), Color.White, 0,
                                       origin, 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, BombermanAdventureGame.ActivePlayer.Level.ToString(), new Vector2(leftM + 100, topM + 120), Color.White, 0,
                                       origin, 1f, SpriteEffects.None, 0);
                        
            spriteBatch.End();            

        }



        #endregion

    }
}
