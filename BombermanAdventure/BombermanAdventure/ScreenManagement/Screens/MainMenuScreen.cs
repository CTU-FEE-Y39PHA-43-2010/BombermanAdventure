using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using BombermanAdventure.GameStorage;
using BombermanAdventure.GameObjects;

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
            MenuEntry playGameMenuEntry = new MenuEntry("play game");
            MenuEntry infoMenuEntry = new MenuEntry("player info");
            MenuEntry deleteProfileMenuEntry = new MenuEntry("delete profile");
            MenuEntry exitMenuEntry = new MenuEntry("exit");

            // Hook up menu event handlers.
            // playGameMenuEntry.Selected += PlayGameMenuEntrySelected;
            infoMenuEntry.Selected += InfoMenuEntrySelected;
            exitMenuEntry.Selected += OnExit;
            deleteProfileMenuEntry.Selected += OnDelete;

            // Add entries to the menu.
            MenuEntries.Add(playGameMenuEntry);
            MenuEntries.Add(infoMenuEntry);
            MenuEntries.Add(deleteProfileMenuEntry);
            MenuEntries.Add(exitMenuEntry);
        } 


        #endregion

        #region Handle Input


        /// <summary>
        /// Event handler for when the Play Game menu entry is selected.
        /// </summary>
        void PlayGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            //LoadingScreen.Load(ScreenManager, true, e.PlayerIndex, new GameplayScreen());
        }


        /// <summary>
        /// Event handler for when the Options menu entry is selected.
        /// </summary>
        void InfoMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new ProfileDetailScreen(), e.PlayerIndex);
        }


        /// <summary>
        /// When the user cancels the main menu, ask if they want to exit the sample.
        /// </summary>
        void OnExit(object sender, PlayerIndexEventArgs e)
        {
            const string message = "Do you really want to exit Bomberman Adventure?";
            DecisionScreen confirmExit = new DecisionScreen(message);
            confirmExit.Accepted += ConfirmExitAccepted;
            ScreenManager.AddScreen(confirmExit, e.PlayerIndex);
        }

        void OnDelete(object sender, PlayerIndexEventArgs e)
        {
            const string message = "Do you really want to delete this profile?";
            DecisionScreen confirmDelete = new DecisionScreen(message);
            confirmDelete.Accepted += ConfirmDeleteAccepted;
            ScreenManager.AddScreen(confirmDelete, e.PlayerIndex);
        }


        /// <summary>
        /// Event handler for when the user selects ok on the "are you sure
        /// you want to exit" message box.
        /// </summary>
        void ConfirmExitAccepted(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.Game.Exit();
        }

        void ConfirmDeleteAccepted(object sender, PlayerIndexEventArgs e)
        {
            //delete and store xml   
            int i = 0;
            foreach(Player p in PlayerListStorage.PlayerList.profiles) 
            {
                if (p.Name == BombermanAdventureGame.ActivePlayer.Name)
                {
                    PlayerListStorage.PlayerList.profiles.RemoveAt(i);
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
