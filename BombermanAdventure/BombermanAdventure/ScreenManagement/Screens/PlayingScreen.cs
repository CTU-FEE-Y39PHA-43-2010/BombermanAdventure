using System;
using System.Diagnostics;
using System.Threading;
using BombermanAdventure.Cameras;
using BombermanAdventure.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BombermanAdventure.ScreenManagement.Screens
{
    /// <summary>
    /// Start bomberman adventure
    /// </summary>
    class PlayingScreen : GameScreen
    {
        #region Fields

        ContentManager _content;
        readonly Random _random = new Random();
        float _pauseAlpha;
        LevelBase gameSession;

        Camera camera;
        ModelList models;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public PlayingScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        public override void InitializeScreenComponents()
        {
            gameSession = new LevelBase(ScreenManager.Game);
            float aspectRatio = ScreenManager.GraphicsDevice.Viewport.Width / (float)ScreenManager.GraphicsDevice.Viewport.Height;

            // inicializace kamery a urceni pocatecni pozice
            camera = new Camera(aspectRatio);

            models = ModelList.GetInstance();
            models.Camera = camera;
        }


        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void LoadContent()
        {
            if (_content == null)
            {
                _content = new ContentManager(ScreenManager.Game.Services, "Content");
            }

            // A real game would probably have more content than this sample, so
            // it would take longer to load. We simulate that by delaying for a
            // while, giving you a chance to admire the beautiful loading screen.
            Thread.Sleep(1000);

            // once the load has finished, we use ResetElapsedTime to tell the game's
            // timing mechanism that we have just finished a very long frame, and that
            // it should not try to catch up.
            ScreenManager.Game.ResetElapsedTime();
        }


        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>
        public override void UnloadContent()
        {
            _content.Unload();
        }


        #endregion

        #region Update and Draw


        /// <summary>
        /// Updates the state of the game. This method checks the GameScreen.IsActive
        /// property, so the game will stop updating when the pause menu is active,
        /// or if you tab away to a different application.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);

            // Gradually fade in or out depending on whether we are covered by the pause screen.
            _pauseAlpha = coveredByOtherScreen ? Math.Min(_pauseAlpha + 1f / 32, 1) : Math.Max(_pauseAlpha - 1f / 32, 0);

            if (!IsActive)
            {
                return;
            }

            camera.Update();
            gameSession.Update(gameTime);

        }

        void ConfirmExitGameAccepted(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new BackgroundScreen(), null);
            ScreenManager.AddScreen(new MainMenuScreen(BombermanAdventureGame.ActivePlayer.Name), e.PlayerIndex);
            ExitScreen();
        }


        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            if (input == null)
            {
                return;
            }

            // Look up inputs for the active player profile.
            if (ControllingPlayer == null)
            {
                Debug.WriteLine("ControllingPlayer is null...");
                return;
            }
            var playerIndex = (int)ControllingPlayer.Value;

            var keyboardState = input.CurrentKeyboardStates[playerIndex];
            var gamePadState = input.CurrentGamePadStates[playerIndex];

            // The game pauses either if the user presses the pause button, or if
            // they unplug the active gamepad. This requires us to keep track of
            // whether a gamepad was ever plugged in, because we don't want to pause
            // on PC if they are playing with a keyboard and have no gamepad at all!
            var gamePadDisconnected = !gamePadState.IsConnected &&
                                      input.GamePadWasConnected[playerIndex];

            if (input.IsPauseGame(ControllingPlayer) || gamePadDisconnected)
            {
                const string message = "Pause. Do you really want to exit game?";
                var confirmExit = new DecisionScreen(message);
                confirmExit.Accepted += ConfirmExitGameAccepted;
                ScreenManager.AddScreen(confirmExit, ControllingPlayer);
            }
        }


        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {

            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,
                                               Color.CornflowerBlue, 0, 0);

            /*SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.Begin();


            spriteBatch.End();*/

            gameSession.Draw(gameTime);
        }


        #endregion
    }
}