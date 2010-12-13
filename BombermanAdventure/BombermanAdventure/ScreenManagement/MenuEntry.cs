using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BombermanAdventure.ScreenManagement.Screens;

namespace BombermanAdventure.ScreenManagement
{
    /// <summary>
    /// Menu entry
    /// </summary>
    class MenuEntry
    {
        #region Fields

        /// <summary>
        /// The text rendered for this entry.
        /// </summary>
        string _text;

        /// <remarks>
        /// The entries transition out of the selection effect when they are deselected.
        /// </remarks>
        float _selectionFade;

        /// <summary>
        /// The position at which the entry is drawn. This is set by the MenuScreen
        /// each frame in Update.
        /// </summary>
        Vector2 _position;

        bool _isFixed;

        #endregion

        #region Properties


        /// <summary>
        /// Gets or sets the text of this menu entry.
        /// </summary>
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }


        /// <summary>
        /// Gets or sets the position at which to draw this menu entry.
        /// </summary>
        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }


        public bool Fixed {
            get { return _isFixed; }
            protected set { _isFixed = value; }
        }

        #endregion

        #region Events


        /// <summary>
        /// Event raised when the menu entry is selected.
        /// </summary>
        public event EventHandler<PlayerIndexEventArgs> Selected;


        /// <summary>
        /// Method for raising the Selected event.
        /// </summary>
        protected internal virtual void OnSelectEntry(PlayerIndex playerIndex)
        {
            if (Selected != null)
            {
                Selected(this, new PlayerIndexEventArgs(playerIndex));
            }
        }


        #endregion

        #region Initialization


        /// <summary>
        /// Constructs a new menu entry with the specified text.
        /// </summary>
        public MenuEntry(string text)
        {
           _text = text;
           Fixed = false;
        }

        public MenuEntry(string text, Vector2 position)
        {
            _text = text;
            Position = position;
            Fixed = true;
        }


        #endregion

        #region Update and Draw


        /// <summary>
        /// Updates the menu entry.
        /// </summary>
        public virtual void Update(MenuScreen screen, bool isSelected, GameTime gameTime)
        {
            var fadeSpeed = (float)gameTime.ElapsedGameTime.TotalSeconds * 4;

            if (isSelected)
            {
                _selectionFade = Math.Min(_selectionFade + fadeSpeed, 1);
            }
            else
            {
                _selectionFade = Math.Max(_selectionFade - fadeSpeed, 0);
            }
        }


        /// <summary>
        /// Draws the menu entry. This can be overridden to customize the appearance.
        /// </summary>
        public virtual void Draw(MenuScreen screen, bool isSelected, GameTime gameTime, Texture2D icon)
        {
            // selected entry draw red
            var color = isSelected ? new Color(204, 42, 42) : Color.White;
           
            // Modify the alpha to fade text out during transitions.
            color *= screen.TransitionAlpha;

            // Draw text, centered on the middle of each line.
            var screenManager = screen.ScreenManager;
            var spriteBatch = screenManager.SpriteBatch;
            var font = screenManager.Font;

            if (isSelected)
            {
                spriteBatch.Draw(icon, new Rectangle(320, (int)_position.Y-15, 30, 30), Color.White);
            }
            var origin = new Vector2(0, font.LineSpacing / 2.0f);

            spriteBatch.DrawString(font, _text, _position, color, 0,
                                   origin, 1f, SpriteEffects.None, 0);
        }


        /// <summary>
        /// Queries how much space this menu entry requires.
        /// </summary>
        public virtual int GetHeight(MenuScreen screen)
        {
            return screen.ScreenManager.Font.LineSpacing;
        }


        /// <summary>
        /// Queries how wide the entry is, used for centering on the screen.
        /// </summary>
        public virtual int GetWidth(MenuScreen screen)
        {
            return (int)screen.ScreenManager.Font.MeasureString(Text).X;
        }


        #endregion
    }
}
