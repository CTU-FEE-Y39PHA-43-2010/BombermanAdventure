using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BombermanAdventure.ScreenManagement.Screens
{
    class BackgroundScreen : GameScreen
    {
        #region Fields

        ContentManager _content;
        Texture2D _backgroundTexture;
        Vector2 _mPosition;
        Texture2D _mSpriteTexture;
        const int Padding = 30;

        #endregion

        #region Initialization


        enum RoundingSpriteBatch
        {
            GoingLeft,
            GoingRight,
            GoingUp,
            GoingDown
        }

        RoundingSpriteBatch _state;

        /// <summary>
        /// Constructor.
        /// </summary>
        public BackgroundScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
            _state = RoundingSpriteBatch.GoingRight;
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
            if (_content == null)
            {
                _content = new ContentManager(ScreenManager.Game.Services, "Content");
            }
            _backgroundTexture = _content.Load<Texture2D>(@"images\background");
            _mSpriteTexture = _content.Load<Texture2D>(@"images\bomb");
            _mPosition = new Vector2(Padding, Padding);
        }


        /// <summary>
        /// Unloads graphics content for this screen.
        /// </summary>
        public override void UnloadContent()
        {
            _content.Unload();
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
            switch (_state)
            {
                case RoundingSpriteBatch.GoingRight:
                    if (++_mPosition.X == BombermanAdventureGame.ScreenWidth - _mSpriteTexture.Width - Padding)
                    {
                        _state = RoundingSpriteBatch.GoingDown;
                    }
                    return;
                case RoundingSpriteBatch.GoingLeft:
                    if (--_mPosition.X == Padding)
                    {
                        _state = RoundingSpriteBatch.GoingUp;
                    }
                    return;
                case RoundingSpriteBatch.GoingDown:
                    if (++_mPosition.Y == BombermanAdventureGame.ScreenHeight - _mSpriteTexture.Height - Padding)
                    {
                        _state = RoundingSpriteBatch.GoingLeft;
                    }
                    return;
                case RoundingSpriteBatch.GoingUp:
                    if (--_mPosition.Y == Padding)
                    {
                        _state = RoundingSpriteBatch.GoingRight;
                    }
                    return;
            }
        }

        /// <summary>
        /// Draws the background screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            var spriteBatch = ScreenManager.SpriteBatch;
            var viewport = ScreenManager.GraphicsDevice.Viewport;
            var fullscreen = new Rectangle(0, 0, viewport.Width, viewport.Height);

            spriteBatch.Begin();

            spriteBatch.Draw(_backgroundTexture, fullscreen,
                             new Color(TransitionAlpha, TransitionAlpha, TransitionAlpha));
            spriteBatch.Draw(_mSpriteTexture, _mPosition, Color.White);

            spriteBatch.End();
        }


        #endregion
    }
}
