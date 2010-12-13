using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace BombermanAdventure.ScreenManagement.Screens
{
    /// <summary>
    /// A popup message box screen, used to display "are you sure?"
    /// confirmation messages.
    /// </summary>
    class DecisionScreen : GameScreen
    {
        #region Fields

        string message;
        Texture2D gradientTexture;

        #endregion

        #region Events

        public event EventHandler<PlayerIndexEventArgs> Accepted;
        public event EventHandler<PlayerIndexEventArgs> Cancelled;

        #endregion

        #region Initialization

        /// <summary>
        /// Constructor lets the caller specify whether to include the standard
        /// "A=ok, B=cancel" usage text prompt.
        /// </summary>
        public DecisionScreen(string message)
        {
            
            this.message = message;

            IsPopup = true;

            TransitionOnTime = TimeSpan.FromSeconds(0.2);
            TransitionOffTime = TimeSpan.FromSeconds(0.2);
        }


        /// <summary>
        /// Loads graphics content for this screen. This uses the shared ContentManager
        /// provided by the Game class, so the content will remain loaded forever.
        /// Whenever a subsequent MessageBoxScreen tries to load this same content,
        /// it will just get back another reference to the already loaded data.
        /// </summary>
        public override void LoadContent()
        {
            ContentManager content = ScreenManager.Game.Content;
            gradientTexture = content.Load<Texture2D>(@"images\gradient");
        }


        #endregion

        #region Handle Input


        /// <summary>
        /// Responds to user input, accepting or cancelling the message box.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            PlayerIndex playerIndex;

            // We pass in our ControllingPlayer, which may either be null (to
            // accept input from any player) or a specific index. If we pass a null
            // controlling player, the InputState helper returns to us which player
            // actually provided the input. We pass that through to our Accepted and
            // Cancelled events, so they can tell which player triggered them.
            if (input.IsMenuSelect(ControllingPlayer, out playerIndex))
            {
                // Raise the accepted event, then exit the message box.
                if (Accepted != null)
                    Accepted(this, new PlayerIndexEventArgs(playerIndex));

                ExitScreen();
            }
            else if (input.IsMenuCancel(ControllingPlayer, out playerIndex))
            {
                // Raise the cancelled event, then exit the message box.
                if (Cancelled != null)
                    Cancelled(this, new PlayerIndexEventArgs(playerIndex));

                ExitScreen();
            }
        }


        #endregion

        #region Draw


        /// <summary>
        /// Draws the message box.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            SpriteFont font = ScreenManager.Font;

            // Darken down any other screens that were drawn beneath the popup.
            ScreenManager.FadeBackBufferToBlack(TransitionAlpha * 2 / 3);

            // Center the message text in the viewport.
            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            Vector2 viewportSize = new Vector2(viewport.Width, viewport.Height);
            Vector2 textSize = font.MeasureString(message);
            Vector2 textPosition = (viewportSize - textSize) / 2;

            // The background includes a border somewhat larger than the text itself.
            const int hPad = 40;
            const int vPad = 40;

            Rectangle fullscreen = new Rectangle(0, 0, viewport.Width, viewport.Height);

            Rectangle backgroundRectangle = new Rectangle((int)textPosition.X - hPad,
                                                          (int)textPosition.Y - vPad,
                                                          (int)textSize.X + hPad * 2,
                                                          (int)textSize.Y + vPad * 2);

            // Fade the popup alpha during transitions.
            Color color = Color.Black * Math.Min(TransitionAlpha, .6f);

            spriteBatch.Begin();

            // Draw the background rectangle.
            spriteBatch.Draw(gradientTexture, fullscreen, color);

            // Draw the message box text.
            spriteBatch.DrawString(font, message, textPosition, Color.White);

            

            Color borderc = new Color(204, 42, 42);
            Texture2D blank  = new Texture2D(ScreenManager.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            blank.SetData(new[] { Color.White });
           //top
            DrawLine(spriteBatch, blank, 2f, borderc, new Vector2(backgroundRectangle.X, backgroundRectangle.Y), new Vector2(backgroundRectangle.X + backgroundRectangle.Width, backgroundRectangle.Y));
            //bottom
            DrawLine(spriteBatch, blank, 2f, borderc, new Vector2(backgroundRectangle.X - 2f, backgroundRectangle.Y + backgroundRectangle.Height), new Vector2(backgroundRectangle.X + backgroundRectangle.Width, backgroundRectangle.Y + backgroundRectangle.Height));
            //left
            DrawLine(spriteBatch, blank, 2f, borderc, new Vector2(backgroundRectangle.X, backgroundRectangle.Y), new Vector2(backgroundRectangle.X, backgroundRectangle.Y + backgroundRectangle.Height));
            //right
            DrawLine(spriteBatch, blank, 2f, borderc, new Vector2(backgroundRectangle.X + backgroundRectangle.Width, backgroundRectangle.Y), new Vector2(backgroundRectangle.X + backgroundRectangle.Width, backgroundRectangle.Y + backgroundRectangle.Height));

            spriteBatch.End();

        }


        #endregion
    }
}
