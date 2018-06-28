using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

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

        //spritefont
        SpriteFont Arial12;

        //list of all enemies probably hould be reworked later but for now good enough
        List<Enemies> enemyList;

        //keyboardstate fields
        KeyboardState kbState;

        //random object
        Random rng;

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

            enemyList = new List<Enemies>();
            rng = new Random();
            
            //Initialize fields

            playerArmor = new Armor(ArmorType.test, "testA", new Rectangle(50, 50, 10, 10), door_locked); //all values in here are just for test too

            //initialize the player
            player = new Player(0, playerWeapon, playerArmor, 100, 20, new Rectangle(100, 100, 50, 50), player_forward); //all values in hereare just for test as well

            Manager manager = new Manager(player);


            Arial12 = Content.Load<SpriteFont>("arial12");

            gameState = GameState.MainMenu;

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
            playerWeapon = new Weapon(WeaponType.test, "testW", new Rectangle(50, 50, 10, 10), rock_small); //all values in here are just for test
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

         MouseState mouseState = new MouseState();


        /// <summary>
        /// method for checking button presses
        /// </summary>
        /// <param name="tlx">top left corner y coordinate</param>
        /// <param name="tly">top left corner x coordinate</param>
        /// <param name="brx">bottom right corner y coordinate</param>
        /// <param name="bry">bottom right corner x coordinate</param>
        /// <returns></returns>
        public bool ButtonClicked(int tlx, int tly, int brx, int bry)
        {
            if((mouseState.X >= tlx && mouseState.Y >= tly) && (mouseState.X < brx && mouseState.Y < bry))
            {
                return true;
            }
            else
            {
                return false;
            }
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
            mouseState = Mouse.GetState();
            kbState = Keyboard.GetState();

            if(enemyList.Count < 1)
            {
                Enemies testEnemy = new Enemies(rng, 10, 2, new Rectangle(500, 500, 100, 100), player_forward);
                enemyList.Add(testEnemy);

            }
            // gamestates
            #region Main Menu
            if (gameState == GameState.MainMenu)
            {
                //checks which button is pressed when the mouse is clicked
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if(ButtonClicked(210, 530, 615, 670))
                    {
                        gameState = GameState.Game;

                    }
                    if(ButtonClicked(210, 670, 590, 900))
                    {
                        gameState = GameState.Options;
                    }
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

                if (player.Position.Intersects(playerWeapon.Position))
                {
                    if (playerWeapon.Visible)
                    {
                        player.PickUpItem(playerWeapon);
                    }
                }

                for(int i = 0; i < enemyList.Count; i++)
                {
                    if(enemyList[i] != null)
                    {
                        if (enemyList[i].Position.Intersects(player.Position))
                        {
                            if (kbState.IsKeyDown(Keys.J))
                            {
                                enemyList[i].TakeDamage(enemyList[i], player);
                                if (enemyList[i].Health <= 0)
                                {
                                    enemyList.RemoveAt(i);
                                    enemyList.Insert(i, null);
                                }
                            }
                            else
                            {
                                enemyList[i].TakeDamage(player, enemyList[i]);
                                if(player.Health <= 0)
                                {
                                    gameState = GameState.EndGame;
                                }
                            }

                        }

                        enemyList[i].Move(enemyList[i]);
                    }
                }
                //transition to inventory screen
                if (kbState.IsKeyDown(Keys.I))
                {
                    gameState = GameState.Inventory;
                }
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
            mouseState = Mouse.GetState();

            //show mouse placement for button placements

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
                if (playerWeapon.Visible)
                {

                    spriteBatch.Draw(playerWeapon.Texture, playerWeapon.Position, Color.White);
                }

                for(int i = 0; i < enemyList.Count; i++)
                {
                    if(enemyList[i] != null)
                    {
                        spriteBatch.Draw(enemyList[i].Texture, enemyList[i].Position, Color.White);
                    }
                }
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
                foreach (string item in player.InvList)
                {
                    spriteBatch.Draw(player.Inventory[item].Texture, new Rectangle(50 + (player.InvList.IndexOf(item) * 50), 
                        50 + player.InvList.IndexOf(item)%10 * 50,
                        100, 100), Color.White);
                    spriteBatch.DrawString(Arial12, item, new Vector2(60 + (player.InvList.IndexOf(item) * 50), 160 + player.InvList.IndexOf(item) % 10 * 50), Color.White);
                }
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

            spriteBatch.DrawString(Arial12, "X: " + mouseState.X + "\nY: " + mouseState.Y, new Vector2(100, 100), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
