using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BombermanAdventure.ScreenManagement.Screens
{
    class BackgroundScreen : GameScreen
    {
        #region Fields

        ContentManager content;
        Texture2D backgroundTexture;

        Vector2 mPosition;
        Texture2D mSpriteTexture;

        int padding = 30;

        #endregion

        #region Initialization


        enum RoundingSpriteBatch
        { 
            GoingLeft,
            GoingRight,
            GoingUp,
            GoingDown
        }

        RoundingSpriteBatch state;

        /// <summary>
        /// Constructor.
        /// </summary>
        public BackgroundScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
            state = RoundingSpriteBatch.GoingRight;
        }
        

        /// <summary>
        /// Loads graphics content for this screen. The background texture is quite
        /// big, so we use our own local ContentManager to load it. This allows us
        /// to unload before going from the menus into the game itself, wheras if we
        /// used the shared ContentManager provided by the Game class, the content
        /// would remain loaded forever.
        /// </summary>
        public override void LoadContent()
        {
            if (content == null)
            {
                content = new ContentManager(ScreenManager.Game.Services, "Content");
            } 
            backgroundTexture = content.Load<Texture2D>(@"images\background");
            mSpriteTexture = content.Load<Texture2D>(@"images\bomb");
            mPosition = new Vector2(padding, padding);
        }


        /// <summary>
        /// Unloads graphics content for this screen.
        /// </summary>
        public override void UnloadContent()
        {
            content.Unload();
        }


        #endregion

        #region Update and Draw


        /// <summary>
        /// Updates the background screen. Unlike most screens, this should not
        /// transition off even if it has been covered by another screen: it is
        /// supposed to be covered, after all! This overload forces the
        /// coveredByOtherScreen parameter to false in order to stop the base
        /// Update method wanting to transition off.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            UpdateRoundingSpritePosition();
            base.Update(gameTime, otherScreenHasFocus, false);
        }

        void UpdateRoundingSpritePosition()
        {
            switch (state)
            { 
                case RoundingSpriteBatch.GoingRight:
                    if (++mPosition.X == BombermanAdventureGame.ScreenWidth - mSpriteTexture.Width - padding)
                    {
                        state = RoundingSpriteBatch.GoingDown;
                    }
                    return;
                case RoundingSpriteBatch.GoingLeft:
                    if (--mPosition.X == padding)
                    {
                        state = RoundingSpriteBatch.GoingUp;
                    }
                    return;
                case RoundingSpriteBatch.GoingDown:
                    if (++mPosition.Y == BombermanAdventureGame.ScreenHeight - mSpriteTexture.Height - padding)
                    {
                        state = RoundingSpriteBatch.GoingLeft;
                    }
                    return;
                case RoundingSpriteBatch.GoingUp:
                    if (--mPosition.Y == padding)
                    {
                        state = RoundingSpriteBatch.GoingRight;
                    }
                    return;
            }
        }

        /// <summary>
        /// Draws the background screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            Rectangle fullscreen = new Rectangle(0, 0, viewport.Width, viewport.Height);

            spriteBatch.Begin();

            spriteBatch.Draw(backgroundTexture, fullscreen,
                             new Color(TransitionAlpha, TransitionAlpha, TransitionAlpha));
            spriteBatch.Draw(mSpriteTexture, mPosition, Color.White);

            spriteBatch.End();
        }


        #endregion
    }
}
