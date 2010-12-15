using BombermanAdventure.GameStorage;
using BombermanAdventure.Models;

namespace BombermanAdventure.ScreenManagement.Screens
{
    class MainMenuScreen : MenuScreen
    {
        #region Initialization


        /// <summary>
        /// Constructor fills in the menu contents.
        /// </summary>
        public MainMenuScreen(string x)
            : base(x)
        {
            // Create our menu entries.
            if (BombermanAdventureGame.ActivePlayer.InGame)
            {
                var continueGameMenuEntry = new MenuEntry("continue game");
                continueGameMenuEntry.Selected += ContinueGameMenuEntrySelected;
                MenuEntries.Add(continueGameMenuEntry);
            }
            var playGameMenuEntry = new MenuEntry("play new game");
            var infoMenuEntry = new MenuEntry("player info");
            var helpMenuEntry = new MenuEntry("command help");
            var deleteProfileMenuEntry = new MenuEntry("delete profile");
            var exitMenuEntry = new MenuEntry("exit");

            // Hook up menu event handlers.
            playGameMenuEntry.Selected += PlayGameMenuEntrySelected;
            infoMenuEntry.Selected += InfoMenuEntrySelected;
            helpMenuEntry.Selected += HelpMenuEntrySelected;
            exitMenuEntry.Selected += OnExit;
            deleteProfileMenuEntry.Selected += OnDelete;

            // Add entries to the menu.
            MenuEntries.Add(playGameMenuEntry);
            MenuEntries.Add(infoMenuEntry);
            MenuEntries.Add(helpMenuEntry);
            MenuEntries.Add(deleteProfileMenuEntry);
            MenuEntries.Add(exitMenuEntry);
        }


        #endregion

        #region Handle Input

        void PlayGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ModelList.Clean();
            if (BombermanAdventureGame.ActivePlayer.Life == 0)
            {
                BombermanAdventureGame.ActivePlayer.Life = 100;
            }
            BombermanAdventureGame.ActivePlayer.InGame = true;
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               new PlayingScreen());
        }

        void ContinueGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               new PlayingScreen());
        }


        /// <summary>
        /// Event handler for when the Options menu entry is selected.
        /// </summary>
        void InfoMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new ProfileDetailScreen(), e.PlayerIndex);
        }

        /// <summary>
        /// Event handler for when the Options menu entry is selected.
        /// </summary>
        void HelpMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new HelpScreen(), e.PlayerIndex);
        }


        /// <summary>
        /// When the user cancels the main menu, ask if they want to exit the sample.
        /// </summary>
        void OnExit(object sender, PlayerIndexEventArgs e)
        {
            const string message = "Do you really want to exit Bomberman Adventure?";
            var confirmExit = new DecisionScreen(message);
            confirmExit.Accepted += ConfirmExitAccepted;
            ScreenManager.AddScreen(confirmExit, e.PlayerIndex);
        }

        void OnDelete(object sender, PlayerIndexEventArgs e)
        {
            const string message = "Do you really want to delete this profile?";
            var confirmDelete = new DecisionScreen(message);
            confirmDelete.Accepted += ConfirmDeleteAccepted;
            ScreenManager.AddScreen(confirmDelete, e.PlayerIndex);
        }


        /// <summary>
        /// Event handler for when the user selects ok on the "are you sure
        /// you want to exit" message box.
        /// </summary>
        void ConfirmExitAccepted(object sender, PlayerIndexEventArgs e)
        {
            BombermanAdventureGame.ActivePlayer.InGame = false;
            PlayerListStorage.Save();
            ScreenManager.Game.Exit();
        }

        void ConfirmDeleteAccepted(object sender, PlayerIndexEventArgs e)
        {
            //delete and store xml   
            var i = 0;
            foreach (var p in PlayerListStorage.PlayerList.Profiles)
            {
                if (p.Name == BombermanAdventureGame.ActivePlayer.Name)
                {
                    PlayerListStorage.PlayerList.Profiles.RemoveAt(i);
                    break;
                }
                i++;
            }
            ScreenManager.AddScreen(new ProfileScreen(), null);
            ExitScreen();
        }


        #endregion
    }
}
