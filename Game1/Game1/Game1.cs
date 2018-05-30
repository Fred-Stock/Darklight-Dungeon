using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    // gamestate enums
    enum GameState
    {
        MainMenu,
        Instructions,
        Options,
        Game,
        Pause,
        Inventory,
        Shop,
        EndGame
    }

    

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // specified fields
        GameState gameState = GameState.MainMenu;

        //create objects
        Player player;
        Weapon playerWeapon;
        Armor playerArmor;


        Door testDoor;

        // sprite fields
        #region Sprites

        // screens
        Texture2D mainMenu;
        Texture2D levelScreen;
        Texture2D pause;
        Texture2D gameOver;

        //objects
        Texture2D door_locked;
        Texture2D door_open;
        Texture2D door_open_animation;
        Texture2D wall;

        //sprites
        Texture2D player_forward;
        Texture2D player_backward;
        Texture2D rock_large;
        Texture2D rock_small;

        //buttons
        Texture2D play_hover;
        Texture2D quit_hover;

        #endregion


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";


            //temp door object
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
            graphics.PreferredBackBufferWidth = 1920;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 1080;   // set this value to the desired height of your window
            this.IsMouseVisible = true;
            graphics.ApplyChanges();

            //Initialize fields
            playerWeapon = new Weapon(WeaponType.test, new Rectangle(50, 50, 10, 10), door_locked); //all values in here are just for test
            playerArmor = new Armor(ArmorType.test, new Rectangle(50, 50, 10, 10), door_locked); //all values in here are just for test too

            //initialize the player
            player = new Player(0, playerWeapon, playerArmor, 100, 20, new Rectangle(100, 100, 50, 50), player_forward); //all values in hereare just for test as well

            Manager manager = new Manager(player);

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

            // TODO: use this.Content to load your game content here
            #region Loading
            //screen loading
            mainMenu = Content.Load<Texture2D>("Screens//Main_menu");
            levelScreen = Content.Load<Texture2D>("Screens//Level");
            wall = Content.Load<Texture2D>("Screens//Wall");
            pause = Content.Load<Texture2D>("Screens//Pause");
            gameOver = Content.Load<Texture2D>("Screens//Game_over");

            //object loading
            door_locked = Content.Load<Texture2D>("Sprites//door_locked");
            door_open = Content.Load<Texture2D>("Sprites//door_unlocked");
            door_open_animation = Content.Load<Texture2D>("Sprites//door_unlocking_animated");
            rock_large = Content.Load<Texture2D>("Sprites//rock_big");
            rock_small = Content.Load<Texture2D>("Sprites//rock_small");

            //sprite loading
            player_forward = Content.Load<Texture2D>("Sprites//player_forward");
            player_backward = Content.Load<Texture2D>("Sprites//player_backward");

            //button loading
            play_hover = Content.Load<Texture2D>("Sprites//Play_hover");
            quit_hover = Content.Load<Texture2D>("Sprites//quit_hover");
            #endregion
            testDoor = new Door(new Rectangle(500, 50, door_open.Width, door_open.Height), door_locked, door_open_animation, door_open);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            // gamestates
            #region Main Menu
            if (gameState == GameState.MainMenu)
            {
                //this whole if statement is just a place holder for testing
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    gameState = GameState.Game;
                }
            }
            #endregion
            #region Instructions
            if (gameState == GameState.Instructions)
            {

            }
            #endregion
            #region Options
            if (gameState == GameState.Options)
            {

            }
            #endregion
            #region Game
            if (gameState == GameState.Game)
            {
                player.Move(player);


                if (player.Position.Intersects(testDoor.Position))
                {
                    testDoor.Activated = true;
                }
                testDoor.DoorActivation();
            }
            #endregion
            #region Pause
            if (gameState == GameState.Pause)
            {

            }
            #endregion
            #region Inventory
            if (gameState == GameState.Inventory)
            {

            }
            #endregion
            #region Shop
            if (gameState == GameState.Shop)
            {

            }
            #endregion
            #region End Game
            if (gameState == GameState.EndGame)
            {

            }
            #endregion

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
            spriteBatch.Begin();

            

            // gamestates
            #region Main Menu
            if (gameState == GameState.MainMenu)
            {
                spriteBatch.Draw(mainMenu, new Vector2(0, 0), Color.White);
            }
            #endregion
            #region Instructions
            if (gameState == GameState.Instructions)
            {

            }
            #endregion
            #region Options
            if (gameState == GameState.Options)
            {

            }
            #endregion
            #region Game
            if (gameState == GameState.Game)
            {
                spriteBatch.Draw(levelScreen, new Vector2(0, 0), Color.White);
                spriteBatch.Draw(player_forward, player.Position, Color.White);
                spriteBatch.Draw(testDoor.CurrentTexture, testDoor.Position, Color.White);
            }
            #endregion
            #region Pause
            if (gameState == GameState.Pause)
            {

            }
            #endregion
            #region Inventory
            if (gameState == GameState.Inventory)
            {

            }
            #endregion
            #region Shop
            if (gameState == GameState.Shop)
            {

            }
            #endregion
            #region End Game
            if (gameState == GameState.EndGame)
            {

            }
            #endregion

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
