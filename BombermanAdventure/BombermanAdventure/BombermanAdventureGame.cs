using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BombermanAdventure.ScreenManagement;
using BombermanAdventure.ScreenManagement.Screens;
using BombermanAdventure.GameObjects;

namespace BombermanAdventure
{
    
    /// <summary>
    /// Bomberman Adventure Game
    /// </summary>
    public class BombermanAdventureGame : Microsoft.Xna.Framework.Game
    {
        
        public const int ScreenWidth = 1000;
        public const int ScreenHeight = 800;
        
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ScreenManager screenManager; 
        private static Profile _activePlayer;


        /// <summary>
        /// Actual gaming player
        /// </summary>
        public static Profile ActivePlayer 
        {
            get { return _activePlayer; }
            set { _activePlayer = value; }
        }
        
        /// <summary>
        /// Preload game components
        /// </summary>
        static readonly string[] PreloadAssets =
        {
            @"images\gradient",
            @"images\bomba",
            @"sound\intro",
        };

        /// <summary>
        /// 
        /// </summary>
        public BombermanAdventureGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = ScreenWidth;
            graphics.PreferredBackBufferHeight = ScreenHeight;

            //folder: BombermanAdventureContent - Content in release
            Content.RootDirectory = "Content"; 
            
            // start screen manager
            screenManager = new ScreenManager(this);

            Components.Add(screenManager);

            // Activate the first screens.
            screenManager.AddScreen(new BackgroundScreen(), null);
            screenManager.AddScreen(new ProfileScreen(), null);

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
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
            foreach (string asset in PreloadAssets)
            {
                Content.Load<object>(asset);
            }
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
            base.Update(gameTime);
        }

        /// <summary>
        /// Game Draw
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }
    }
}
