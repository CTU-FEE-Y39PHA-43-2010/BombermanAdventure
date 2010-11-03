using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;
using System.IO;
using System.Xml.Serialization;
using BombermanAdventure.ScreenManagement;
using BombermanAdventure.ScreenManagement.Screens;
using BombermanAdventure.GameObjects;
using BombermanAdventure.GameStorage;

namespace BombermanAdventure
{
    
    
    
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class BombermanAdventureGame : Microsoft.Xna.Framework.Game
    {
        public const int ScreenWidth = 1216;
        public const int ScreenHeight = 624;
        
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ScreenManager screenManager; 
        Song intro;

        // By preloading any assets used by UI rendering, we avoid framerate glitches
        // when they suddenly need to be loaded in the middle of a menu transition.
        static readonly string[] preloadAssets =
        {
            "gradient",
        };

        public BombermanAdventureGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = ScreenWidth;
            graphics.PreferredBackBufferHeight = ScreenHeight;

            Content.RootDirectory = "Content"; //folder: BombermanAdventureContent - Content in release

            // Create the screen manager component.
            screenManager = new ScreenManager(this);

            Components.Add(screenManager);

            // Activate the first screens.
            screenManager.AddScreen(new BackgroundScreen(), null);

            PlayerList playerList;
            try
            {
                XmlStorage.ReadObject<PlayerList>(out playerList, "profiles.xml");
            }
            catch (FileNotFoundException)
            {
                //create empty file
                playerList = new PlayerList();
            }

            if (playerList.profiles.Count > 0)
            {
                //open profile screen
            }
            else
            {
                screenManager.AddScreen(new MainMenuScreen(), null);
            }

            intro = Content.Load<Song>("intro");
            MediaPlayer.Play(intro);

            //playerList.profiles.Add(new Profile() { playerName = "h3m3r22277", playerProfileFile = XmlStorage.GenerateHashedPlayerProfileFileName("h3m3r222") });

                       
            
            //pl = new PlayerList() { name = "kozel" };
            //XmlStorage.WriteObject<PlayerList>(ref pl, "profiles.xml"); //PlayerList should be a part of assembly

            //XmlStorage.ReadObject<PlayerList>(out pl, "pl55.xml");

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            foreach (string asset in preloadAssets)
            {
                Content.Load<object>(asset);
            }
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
