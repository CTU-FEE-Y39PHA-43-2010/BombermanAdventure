
#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace BombermanAdventure.ScreenManagement.Screens
{

    abstract class MenuScreen : GameScreen
    {
        #region Fields

        readonly List<MenuEntry> _menuEntries = new List<MenuEntry>();
        private int _selectedEntry;
        readonly string _menuTitle;
        Texture2D _fadeTecture;

        protected int LineWeight = 2;
        protected int LeftM = 100;
        protected int RightM = 400;
        protected int TopM = 100;
        protected int BottomM = 100;
        protected Texture2D Blank;

        #endregion

        #region Properties


        protected IList<MenuEntry> MenuEntries
        {
            get { return _menuEntries; }
        }

        public Texture2D Icon { get; private set; }

        public bool FadeOut { get; set; }

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        protected MenuScreen(string menuTitle, bool isFadeOut = false)
        {
            _menuTitle = menuTitle;
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
            FadeOut = isFadeOut;
            _selectedEntry = 0;
        }


        #endregion

        #region Handle Input


        public override void HandleInput(InputState input)
        {
            // Move to the previous menu entry?
            if (input.IsMenuUp(ControllingPlayer))
            {
                if (_menuEntries.Count > 1)
                {
                    //play sound
                    ScreenManager.MenuUp.Play(0.5f, 0f, 0f);
                    _selectedEntry--;

                    if (_selectedEntry < 0)
                        _selectedEntry = _menuEntries.Count - 1;
                }
            }

            // Move to the next menu entry?
            if (input.IsMenuDown(ControllingPlayer))
            {
                if (_menuEntries.Count > 1)
                {
                    //play sound
                    ScreenManager.MenuDown.Play(0.5f, 0f, 0f);
                    _selectedEntry++;


                    if (_selectedEntry >= _menuEntries.Count)
                        _selectedEntry = 0;
                }
            }

            // Accept or cancel the menu? We pass in our ControllingPlayer, which may
            // either be null (to accept input from any player) or a specific index.
            // If we pass a null controlling player, the InputState helper returns to
            // us which player actually provided the input. We pass that through to
            // OnSelectEntry and OnCancel, so they can tell which player triggered them.
            PlayerIndex playerIndex;

            if (input.IsMenuSelect(ControllingPlayer, out playerIndex))
            {
                OnSelectEntry(_selectedEntry, playerIndex);
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
            _menuEntries[entryIndex].OnSelectEntry(playerIndex);
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
            var transitionOffset = (float)Math.Pow(TransitionPosition, 2);

            // start at Y = 175; each X value is generated per entry
            var position = new Vector2(0f, 175f);

            // update each menu entry's location in turn
            foreach (var menuEntry in _menuEntries.Where(menuEntry => !menuEntry.Fixed))
            {
                // each entry is to be centered horizontally
                // position.X = ScreenManager.GraphicsDevice.Viewport.Width / 2 - menuEntry.GetWidth(this) / 2;
                position.X = 120;
                position.X += (ScreenState == ScreenState.TransitionOn ? -(transitionOffset * 256) : transitionOffset * 512);

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
            for (var i = 0; i < _menuEntries.Count; i++)
            {
                var isSelected = IsActive && (i == _selectedEntry);
                _menuEntries[i].Update(this, isSelected, gameTime);
            }
        }

        public override void LoadContent()
        {
            var content = ScreenManager.Game.Content;
            _fadeTecture = content.Load<Texture2D>(@"images\gradient");
            Icon = content.Load<Texture2D>(@"images\bomba");
            Blank = new Texture2D(ScreenManager.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            Blank.SetData(new[] { Color.White });
        }

        /// <summary>
        /// Draws the menu.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            // make sure our entries are in the right place before we draw them
            UpdateMenuEntryLocations();

            var spriteBatch = ScreenManager.SpriteBatch;
            var font = ScreenManager.Font;
            var color = Color.Black * Math.Min(TransitionAlpha, .8f);

            spriteBatch.Begin();

            var viewport = ScreenManager.GraphicsDevice.Viewport;
            Color titleColor;

            if (FadeOut)
            {
                spriteBatch.Draw(_fadeTecture, new Rectangle(0, 0, viewport.Width, viewport.Height), color);
                titleColor = new Color(255, 255, 255) * TransitionAlpha;
            }
            else
            {
                spriteBatch.Draw(_fadeTecture, new Rectangle(TopM, LeftM, RightM - LeftM, viewport.Height - TopM - BottomM), color);
                titleColor = new Color(0, 0, 0) * TransitionAlpha;
            }

            //top
            DrawLine(spriteBatch, Blank, LineWeight, Color.Gray, new Vector2(LeftM, TopM), new Vector2(RightM, TopM));
            //bottom
            DrawLine(spriteBatch, Blank, LineWeight, Color.Gray, new Vector2(LeftM - LineWeight, viewport.Height - 100), new Vector2(400, viewport.Height - 100));
            //left
            DrawLine(spriteBatch, Blank, LineWeight, Color.Gray, new Vector2(LeftM, TopM), new Vector2(100, viewport.Height - BottomM));
            //right
            DrawLine(spriteBatch, Blank, LineWeight, Color.Gray, new Vector2(RightM, TopM), new Vector2(400, viewport.Height - BottomM));


            // Draw each menu entry in turn.
            for (var i = 0; i < _menuEntries.Count; i++)
            {
                var menuEntry = _menuEntries[i];
                var isSelected = IsActive && (i == _selectedEntry);
                menuEntry.Draw(this, isSelected, gameTime, Icon);
            }

            // Make the menu slide into place during transitions, using a
            // power curve to make things look more interesting (this makes
            // the movement slow down as it nears the end).
            var transitionOffset = (float)Math.Pow(TransitionPosition, 2);

            var titlePosition = new Vector2(150, 60);
            var titleOrigin = font.MeasureString(_menuTitle) / 2;

            const float titleScale = 1.25f;
            titlePosition.Y -= transitionOffset * 100;

            spriteBatch.DrawString(font, "<< " + _menuTitle + " >>", titlePosition, titleColor, 0,
                                   titleOrigin, titleScale, SpriteEffects.None, 0);

            spriteBatch.End();
        }


        #endregion
    }
}