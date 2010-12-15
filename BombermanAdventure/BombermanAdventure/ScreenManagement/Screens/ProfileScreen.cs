using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BombermanAdventure.GameStorage;
using BombermanAdventure.GameObjects;

namespace BombermanAdventure.ScreenManagement.Screens
{
    class ProfileScreen : MenuScreen
    {

        #region Initialization

        private bool _createProfile;
        private Color _profileTextColor;
        private string _newProfileName;
        private string _input;
        private bool _saveable;
        private bool _shift;
        private readonly PlayerList _pl;

        /// <summary>
        /// Profile scree
        /// </summary>
        public ProfileScreen()
            : base("load profile", true)
        {
            _pl = PlayerListStorage.PlayerList;
            FillMenuItems();
            _createProfile = false;
            _saveable = false;
            _newProfileName = "";
            _input = "";
        }

        #endregion

        #region Handle Input

        void OptionsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            var m = (MenuEntry)sender;
            foreach (var p in _pl.Profiles.Where(p => p.Name == m.Text))
            {
                BombermanAdventureGame.ActivePlayer = p;
                ScreenManager.AddScreen(new MainMenuScreen(m.Text), e.PlayerIndex);
                ExitScreen();
            }
        }

        /// <summary>
        /// Event handler for when the Play Game menu entry is selected.
        /// </summary>
        void NewProfileEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            _createProfile = true;
        }

        void FillMenuItems()
        {

            /*foreach (var profileEntry in _pl.profiles.Select(player => new MenuEntry(player.Name)))
            {
                MenuEntries.Add(profileEntry);
                profileEntry.Selected += OptionsMenuEntrySelected;
            }*/

            foreach (var player in _pl.Profiles)
            {
                var profileEntry = new MenuEntry(player.Name);
                MenuEntries.Add(profileEntry);
                profileEntry.Selected += OptionsMenuEntrySelected;
            }

            // Create static menu entry for new profile creating
            var newProfileEntry = new MenuEntry("create new profile", new Vector2(LeftM + 20, BombermanAdventureGame.ScreenHeight - BottomM - 30));
            newProfileEntry.Selected += NewProfileEntrySelected;

            MenuEntries.Add(newProfileEntry);
        }

        public override void HandleInput(InputState input)
        {

            PlayerIndex playerIndex;
            if (_createProfile)
            {
                if (input.IsNewKeyPress(Keys.Escape, ControllingPlayer, out playerIndex))
                {
                    _createProfile = false;
                    return;
                }
                if (input.IsNewKeyPress(Keys.Enter, ControllingPlayer, out playerIndex))
                {
                    if (_saveable)
                    {
                        if (_pl.Profiles.Any(player => player.Name == _newProfileName))
                        {
                            return;
                        }
                        if (_pl.Profiles != null)
                        {
                            _pl.Profiles.Add(new Profile(_newProfileName));
                            MenuEntries.Clear();
                            FillMenuItems();
                            PlayerListStorage.Save();
                            _createProfile = false;
                            _newProfileName = "";
                            _input = "";
                        }
                    }
                    return;
                }

                var i = (int)playerIndex;
                var selected = false;
                var pressed = "";
                foreach (var key in input.CurrentKeyboardStates[i].GetPressedKeys())
                {
                    if (input.LastKeyboardStates[i].IsKeyUp(key) && !selected)
                    {
                        var keyNum = (int)key;
                        if (key == Keys.Back && _input.Length > 0)
                        {
                            _input = _input.Substring(0, _input.Length - 1);
                            return;
                        }
                        if (_input.Length > 9)
                        {
                            return;
                        }
                        if (keyNum >= (int)Keys.A && keyNum <= (int)Keys.Z)
                        {
                            selected = true;
                            pressed = key.ToString();
                        }
                        else if (keyNum >= (int)Keys.D0 && keyNum <= (int)Keys.D9)
                        {
                            selected = true;
                            pressed = key.ToString().Substring(1, 1);
                        }
                    }
                    if ((key == Keys.RightShift || key == Keys.LeftShift))
                    {
                        _shift = true;
                    }
                    else
                    {
                        _shift = false;
                    }
                }

                if (!string.IsNullOrEmpty(pressed))
                {
                    if (!_shift)
                    {
                        _input += pressed.ToLower();
                    }
                    else
                    {
                        _input += pressed.ToUpper();
                    }
                }

                if (_input.Length > 4)
                {
                    _saveable = true;
                }

            }
            else if (input.IsMenuCancel(ControllingPlayer, out playerIndex))
            {
                return;
            }
            else
            {
                base.HandleInput(input);
            }

        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                      bool coveredByOtherScreen)
        {

            if (_createProfile)
            {
                if (string.IsNullOrEmpty(_input))
                {
                    _newProfileName = "new profile";
                    _profileTextColor = new Color(200, 200, 200);
                }
                else
                {
                    _newProfileName = _input;
                    _profileTextColor = new Color(0, 0, 0);
                }
            }

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            var spriteBatch = ScreenManager.SpriteBatch;
            var viewport = ScreenManager.GraphicsDevice.Viewport;

            spriteBatch.Begin();

            if (_createProfile)
            {

                //vypsat kolonku
                spriteBatch.Draw(Blank, new Rectangle(LeftM + 20, viewport.Height - BottomM - 80, 200, 30), Color.White);

                //vypsat text
                var font = ScreenManager.Font;
                var origin = new Vector2(0, font.LineSpacing / 2.0f);

                spriteBatch.DrawString(font, "profile name:", new Vector2(LeftM + 25, viewport.Height - BottomM - 95), Color.White, 0,
                                       origin, 1f, SpriteEffects.None, 0);

                spriteBatch.DrawString(font, _newProfileName, new Vector2(LeftM + 40, viewport.Height - BottomM - 65), _profileTextColor, 0,
                                       origin, 1f, SpriteEffects.None, 0);
            }

            spriteBatch.End();

        }


        #endregion

    }
}
