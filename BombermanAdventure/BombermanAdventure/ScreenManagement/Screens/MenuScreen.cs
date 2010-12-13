
#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
#endregion

namespace BombermanAdventure.ScreenManagement.Screens
{
    
    abstract class MenuScreen : GameScreen
    {
        #region Fields

        List<MenuEntry> menuEntries = new List<MenuEntry>();
        int selectedEntry = 0;
        string menuTitle;
        bool isFadeOut;
        Texture2D fadeTecture;
        Texture2D icon;

        protected int lineWeight = 2;
        protected int leftM = 100;
        protected int rightM = 400;
        protected int topM = 100;
        protected int bottomM = 100;
        protected Texture2D blank;

        #endregion

        #region Properties


        protected IList<MenuEntry> MenuEntries
        {
            get { return menuEntries; }
        }

        public Texture2D Icon
        {
            get { return icon; }
        }

        public bool FadeOut 
        {
            get { return isFadeOut; }
            set 
            { 
                isFadeOut = value;
            }
        }

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public MenuScreen(string menuTitle, bool isFadeOut = false)
        {
            
            this.menuTitle = menuTitle;
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
            FadeOut = isFadeOut;

        }


        #endregion

        #region Handle Input


        public override void HandleInput(InputState input)
        {
            // Move to the previous menu entry?
            if (input.IsMenuUp(ControllingPlayer))
            {
                //play sound
                ScreenManager.MenuUp.Play(0.5f, 0f, 0f);
                selectedEntry--;

                if (selectedEntry < 0)
                    selectedEntry = menuEntries.Count - 1;
            }

            // Move to the next menu entry?
            if (input.IsMenuDown(ControllingPlayer))
            {
                //play sound
                ScreenManager.MenuDown.Play(0.5f, 0f, 0f);
                selectedEntry++;
                

                if (selectedEntry >= menuEntries.Count)
                    selectedEntry = 0;
            }

            // Accept or cancel the menu? We pass in our ControllingPlayer, which may
            // either be null (to accept input from any player) or a specific index.
            // If we pass a null controlling player, the InputState helper returns to
            // us which player actually provided the input. We pass that through to
            // OnSelectEntry and OnCancel, so they can tell which player triggered them.
            PlayerIndex playerIndex;

            if (input.IsMenuSelect(ControllingPlayer, out playerIndex))
            {
                OnSelectEntry(selectedEntry, playerIndex);
            }
            /*else if (input.IsMenuCancel(ControllingPlayer, out playerIndex))
            {
                OnCancel(playerIndex);
            }*/
        }


        /// <summary>
        /// Handler for when the user has chosen a menu entry.
        /// </summary>
        protected virtual void OnSelectEntry(int entryIndex, PlayerIndex playerIndex)
        {
            menuEntries[entryIndex].OnSelectEntry(playerIndex);
        }


        /// <summary>
        /// Handler for when the user has cancelled the menu.
        /// </summary>
        protected virtual void OnCancel(PlayerIndex playerIndex)
        {
            ExitScreen();
        }


        /// <summary>
        /// Helper overload makes it easy to use OnCancel as a MenuEntry event handler.
        /// </summary>
        protected void OnCancel(object sender, PlayerIndexEventArgs e)
        {
            OnCancel(e.PlayerIndex);
        }


        #endregion

        #region Update and Draw

        /// <summary>
        /// Allows the screen the chance to position the menu entries. By default
        /// all menu entries are lined up in a vertical list, centered on the screen.
        /// </summary>
        protected virtual void UpdateMenuEntryLocations()
        {
            // Make the menu slide into place during transitions, using a
            // power curve to make things look more interesting (this makes
            // the movement slow down as it nears the end).
            float transitionOffset = (float)Math.Pow(TransitionPosition, 2);

            // start at Y = 175; each X value is generated per entry
            Vector2 position = new Vector2(0f, 175f);

            // update each menu entry's location in turn
            for (int i = 0; i < menuEntries.Count; i++)
            {
                MenuEntry menuEntry = menuEntries[i];
                if (menuEntry.Fixed)
                {
                    continue;
                }

                // each entry is to be centered horizontally
               // position.X = ScreenManager.GraphicsDevice.Viewport.Width / 2 - menuEntry.GetWidth(this) / 2;
                position.X = 120;

                if (ScreenState == ScreenState.TransitionOn)
                    position.X -= transitionOffset * 256;
                else
                    position.X += transitionOffset * 512;

                // set the entry's position
                menuEntry.Position = position;

                // move down for the next entry the size of this entry
                position.Y += menuEntry.GetHeight(this);
            }
        }


        /// <summary>
        /// Updates the menu.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            // Update each nested MenuEntry object.
            for (int i = 0; i < menuEntries.Count; i++)
            {
                bool isSelected = IsActive && (i == selectedEntry);

                menuEntries[i].Update(this, isSelected, gameTime);
            }
        }

        public override void LoadContent()
        {
            ContentManager content = ScreenManager.Game.Content;
            fadeTecture = content.Load<Texture2D>(@"images\gradient");
            icon = content.Load<Texture2D>(@"images\bomba");
            blank = new Texture2D(ScreenManager.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            blank.SetData(new[] { Color.White });
        }
                
        /// <summary>
        /// Draws the menu.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            // make sure our entries are in the right place before we draw them
            UpdateMenuEntryLocations();

            GraphicsDevice graphics = ScreenManager.GraphicsDevice;
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            SpriteFont font = ScreenManager.Font;
            Color color = Color.Black * Math.Min(TransitionAlpha, .8f);

            spriteBatch.Begin();

            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            Color titleColor;

            if (FadeOut)
            {
                spriteBatch.Draw(fadeTecture, new Rectangle(0, 0, viewport.Width, viewport.Height), color);
                titleColor = new Color(255, 255, 255) * TransitionAlpha;
                // fade.End();
            }
            else {
                spriteBatch.Draw(fadeTecture, new Rectangle(topM, leftM, rightM-leftM, viewport.Height-topM-bottomM), color);
                titleColor = new Color(0, 0, 0) * TransitionAlpha;
            }

            //top
            DrawLine(spriteBatch, blank, lineWeight, Color.Gray, new Vector2(leftM, topM), new Vector2(rightM, topM));
            //bottom
            DrawLine(spriteBatch, blank, lineWeight, Color.Gray, new Vector2(leftM - lineWeight, viewport.Height - 100), new Vector2(400, viewport.Height - 100));
            //left
            DrawLine(spriteBatch, blank, lineWeight, Color.Gray, new Vector2(leftM, topM), new Vector2(100, viewport.Height - bottomM));
            //right
            DrawLine(spriteBatch, blank, lineWeight, Color.Gray, new Vector2(rightM, topM), new Vector2(400, viewport.Height - bottomM));


            // Draw each menu entry in turn.
            for (int i = 0; i < menuEntries.Count; i++)
            {
                MenuEntry menuEntry = menuEntries[i];

                bool isSelected = IsActive && (i == selectedEntry);

                menuEntry.Draw(this, isSelected, gameTime, Icon);
            }

            // Make the menu slide into place during transitions, using a
            // power curve to make things look more interesting (this makes
            // the movement slow down as it nears the end).
            float transitionOffset = (float)Math.Pow(TransitionPosition, 2);

            // Draw the menu title centered on the screen
            Vector2 titlePosition = new Vector2(150, 60);
            Vector2 titleOrigin = font.MeasureString(menuTitle) / 2;
            
            float titleScale = 1.25f;

            titlePosition.Y -= transitionOffset * 100;

            spriteBatch.DrawString(font, "<< " + menuTitle + " >>", titlePosition, titleColor, 0,
                                   titleOrigin, titleScale, SpriteEffects.None, 0);

            spriteBatch.End();
        }


        #endregion
    }
}