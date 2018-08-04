using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using System.IO;


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
        Weapon playerWeapon2;
        Armor playerArmor;
        Armor playerArmor2;
        Manager manager;
        Item testCurrency;
        ShopManager shop;
        Level currentLevel;
        Rectangle temp;

        //create stream reader and writer
        StreamReader reader;
        StreamWriter writer;

        //lists sprites for animations
        List<Texture2D> attack1;
        List<Texture2D> attack2;

        //temp fields (will be replaced when other parts of the game are developed
        bool usedShop; //level loading should handle this in someway
        string shopMessage; //not sure if this will get replaced but would probably be cleaner if there is a way to skip this


        //helper fields
        KeyboardState previous; //keyboardstate to help with tracking single keyboard inputs
        bool levelLoaded;
        string levelData;
        


        // sprite fields
        #region Sprites

        // screens
        Texture2D mainMenu;
        Texture2D levelScreen;
        Texture2D pause;
        Texture2D gameOver;
        Texture2D inventoryScreen;

        //objects
        Texture2D door_locked;
        Texture2D door_open;
        Texture2D door_open_animation;
        Texture2D wall;
        Texture2D base_weapon;
        Texture2D base_armor;

        //sprites
        Texture2D player_forward;
        Texture2D player_backward;
        Texture2D player_walk_side;
        Texture2D rock_large;
        Texture2D rock_small;
        Texture2D enemy_1;

        //attack sprites
        //attack 1
        Texture2D attack_1_1;
        Texture2D attack_1_2;
        Texture2D attack_1_3;
        Texture2D attack_1_4;
        Texture2D attack_1_5;
        Texture2D attack_1_6;
        Texture2D attack_1_7;
        Texture2D attack_1_8;
        Texture2D attack_1_9;
        Texture2D attack_1_10;
        Texture2D attack_1_11;
        Texture2D attack_1_12;
        Texture2D attack_1_13;

        //attack 2
        Texture2D attack_2_1;
        Texture2D attack_2_2;
        Texture2D attack_2_3;
        Texture2D attack_2_4;
        Texture2D attack_2_5;
        Texture2D attack_2_6;
        Texture2D attack_2_7;
        Texture2D attack_2_8;
        Texture2D attack_2_9;
        Texture2D attack_2_10;
        Texture2D attack_2_11;
        Texture2D attack_2_12;
        Texture2D attack_2_13;

        //buttons
        Texture2D play_hover;
        Texture2D quit_hover;

        //spritefont
        SpriteFont Arial12;

        //keyboardstate fields
        KeyboardState kbState;

        //random object
        Random rng;

        #endregion


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
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
            
            rng = new Random();

            //Initialize fields
            usedShop = false;



            manager = new Manager(player);

            Arial12 = Content.Load<SpriteFont>("arial12");

            attack1 = new List<Texture2D>();
            attack2 = new List<Texture2D>();
            levelLoaded = false;
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
            inventoryScreen = Content.Load<Texture2D>("Screens//Inventory");

            //object loading
            door_locked = Content.Load<Texture2D>("Sprites//door_locked");
            door_open = Content.Load<Texture2D>("Sprites//door_unlocked");
            door_open_animation = Content.Load<Texture2D>("Sprites//door_unlocking_animated");
            rock_large = Content.Load<Texture2D>("Sprites//rock_big");
            rock_small = Content.Load<Texture2D>("Sprites//rock_small");
            base_weapon = Content.Load<Texture2D>("Sprites//base_weapon");
            base_armor = Content.Load<Texture2D>("Sprites//base_armor");

            //sprite loading
            player_forward = Content.Load<Texture2D>("Sprites//player_forward");
            player_backward = Content.Load<Texture2D>("Sprites//player_backward");
            player_walk_side = Content.Load<Texture2D>("Sprites//player_walk_side");
            enemy_1 = Content.Load<Texture2D>("Sprites//enemy_1");
            //attack sprite loading
            //attack 1
            attack_1_1 = Content.Load<Texture2D>("Sprites//AttackSprites//attack_1a");
            attack1.Add(attack_1_1);
            attack_1_2 = Content.Load<Texture2D>("Sprites//AttackSprites//attack_1b");
            attack1.Add(attack_1_2);
            attack_1_3 = Content.Load<Texture2D>("Sprites//AttackSprites//attack_1c");
            attack1.Add(attack_1_3);
            attack_1_4 = Content.Load<Texture2D>("Sprites//AttackSprites//attack_1d");
            attack1.Add(attack_1_4);
            attack_1_5 = Content.Load<Texture2D>("Sprites//AttackSprites//attack_1e");
            attack1.Add(attack_1_5);
            attack_1_6 = Content.Load<Texture2D>("Sprites//AttackSprites//attack_1f");
            attack1.Add(attack_1_6);
            attack_1_7 = Content.Load<Texture2D>("Sprites//AttackSprites//attack_1g");
            attack1.Add(attack_1_7);
            attack_1_8 = Content.Load<Texture2D>("Sprites//AttackSprites//attack_1h");
            attack1.Add(attack_1_8);
            attack_1_9 = Content.Load<Texture2D>("Sprites//AttackSprites//attack_1i");
            attack1.Add(attack_1_9);
            attack_1_10 = Content.Load<Texture2D>("Sprites//AttackSprites//attack_1j");
            attack1.Add(attack_1_10);
            attack_1_11 = Content.Load<Texture2D>("Sprites//AttackSprites//attack_1k");
            attack1.Add(attack_1_11);
            attack_1_12 = Content.Load<Texture2D>("Sprites//AttackSprites//attack_1l");
            attack1.Add(attack_1_12);
            attack_1_13 = Content.Load<Texture2D>("Sprites//AttackSprites//attack_1m");
            attack1.Add(attack_1_13);

            //attack 2
            attack_2_1 = Content.Load<Texture2D>("Sprites//AttackSprites//attack_2a");
            attack2.Add(attack_2_1);
            attack_2_2 = Content.Load<Texture2D>("Sprites//AttackSprites//attack_2b");
            attack2.Add(attack_2_2);
            attack_2_3 = Content.Load<Texture2D>("Sprites//AttackSprites//attack_2c");
            attack2.Add(attack_2_3);
            attack_2_4 = Content.Load<Texture2D>("Sprites//AttackSprites//attack_2d");
            attack2.Add(attack_2_4);
            attack_2_5 = Content.Load<Texture2D>("Sprites//AttackSprites//attack_2e");
            attack2.Add(attack_2_5);
            attack_2_6 = Content.Load<Texture2D>("Sprites//AttackSprites//attack_2f");
            attack2.Add(attack_2_6);
            attack_2_7 = Content.Load<Texture2D>("Sprites//AttackSprites//attack_2g");
            attack2.Add(attack_2_7);
            attack_2_8 = Content.Load<Texture2D>("Sprites//AttackSprites//attack_2h");
            attack2.Add(attack_2_8);
            attack_2_9 = Content.Load<Texture2D>("Sprites//AttackSprites//attack_2i");
            attack2.Add(attack_2_9);
            attack_2_10 = Content.Load<Texture2D>("Sprites//AttackSprites//attack_2j");
            attack2.Add(attack_2_10);
            attack_2_11 = Content.Load<Texture2D>("Sprites//AttackSprites//attack_2k");
            attack2.Add(attack_2_11);
            attack_2_12 = Content.Load<Texture2D>("Sprites//AttackSprites//attack_2l");
            attack2.Add(attack_2_12);
            attack_2_13 = Content.Load<Texture2D>("Sprites//AttackSprites//attack_2m");
            attack2.Add(attack_2_13);

            //button loading
            play_hover = Content.Load<Texture2D>("Sprites//Play_hover");
            quit_hover = Content.Load<Texture2D>("Sprites//quit_hover");
            
            //initialize the player
            playerWeapon = new Weapon(WeaponType.basic, "weapon", new Rectangle(50, 250, 40, 40), base_weapon); //all values in here are just for test
            playerArmor = new Armor(ArmorType.test, "armor", new Rectangle(50, 50, 10, 10), base_armor); //all values in here are just for test too
            player = new Player(0, playerWeapon, playerArmor, player_walk_side, player_backward, player_forward, 10, 5, new Rectangle(100, 100, 45, 75), player_forward); //all values in here are just for test as well
            #endregion
            testCurrency = new Item("smallCoin", new Rectangle(200, 500, 20, 20), rock_large);
            playerArmor2 = new Armor(ArmorType.test, "testA2", new Rectangle(50, 50, 10, 10), door_locked);//test value
            List<Item> testShopInv = new List<Item>();
            shop = new ShopManager();
            shop.AddToShop(playerWeapon, 0);
            shop.AddToShop(playerArmor, 0);
            currentLevel = new Level(LevelIO("level1_1"), manager);
            
            
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
                        Exit();
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
                //load level
                if (!levelLoaded)
                {
                    levelData = LevelInfo(currentLevel.LevelArray);
                    string nextLevel = null;
                    int k = 0;
                    while (levelData[k] != ',')
                    {
                        nextLevel += levelData[k];
                        k++;
                    }
                    for (int i = 0; i < levelData.Length; i++)
                    {
                        //spawn player
                        if(levelData[i].Equals('P'))
                        {
                            temp = player.Position;
                            k = i + 1;
                            
                            string coord = null;
                            while (levelData[k] != ',')
                            {
                                coord += levelData[k];
                                k++;
                            }
                            temp.X = int.Parse(coord) * 120;
                            k++;
                            coord = null;
                            while (levelData[k] != ',')
                            {
                                coord += levelData[k];
                                k++;
                            }
                            temp.Y = int.Parse(coord) * 120;
                            player.Position = temp;
                        }

                        //spawn enemies
                        if (levelData[i].Equals('E') || levelData[i].Equals('F') || levelData[i].Equals('C'))
                        {
                            
                            k = i + 1;

                            string coord = null;
                            while (levelData[k] != ',')
                            {
                                coord += levelData[k];
                                k++;
                            }
                            temp.X = int.Parse(coord) * 120;
                            k++;
                            coord = null;
                            while (levelData[k] != ',')
                            {
                                coord += levelData[k];
                                k++;
                            }
                            temp.Y = int.Parse(coord) * 120;
                            if(levelData[i] == 'F')
                            {
                                manager.EnemyList.Add(new Fly(rng, 20, 4, new Rectangle(temp.X, temp.Y, 50, 50), enemy_1));
                            }
                            else if(levelData[i] == 'C')
                            {
                                manager.EnemyList.Add(new Centipede(player, rng, 30, 8, new Rectangle(temp.X, temp.Y, 75, 75), enemy_1));
                            }
                            else
                            {
                                manager.EnemyList.Add(new Enemies(rng, 40, 6, new Rectangle(temp.X, temp.Y, 100, 100), enemy_1));
                            }

                        }

                        //spawn doors
                        if (levelData[i].Equals('D'))
                        {
                            
                            k = i + 1;

                            string coord = null;
                            while (levelData[k] != ',')
                            {
                                coord += levelData[k];
                                k++;
                            }
                            temp.X = int.Parse(coord) * 120;
                            k++;
                            coord = null;
                            while (levelData[k] != ',')
                            {
                                coord += levelData[k];
                                k++;
                            }
                            temp.Y = int.Parse(coord) * 120;
                            manager.DoorList.Add(new Door(new Rectangle(temp.X, temp.Y, 100, 100), door_locked, door_open_animation, door_open, nextLevel));
                        }

                        //spawn stores
                        if (levelData[i].Equals('S'))
                        {
                            k = i + 1;

                            string coord = null;
                            while (levelData[k] != ',')
                            {
                                coord += levelData[k];
                                k++;
                            }
                            temp.X = int.Parse(coord) * 120;
                            k++;
                            coord = null;
                            while (levelData[k] != ',')
                            {
                                coord += levelData[k];
                                k++;
                            }
                            temp.Y = int.Parse(coord) * 120;
                            shop.ShopInv.Clear();
                            shop.ItemCosts.Clear();
                            usedShop = false;
                            FillShop(player, shop);
                            manager.ItemList.Add(new Item("store", new Rectangle(temp.X, temp.Y, 60, 60), rock_large));
                        }
                    }
                    //spawn obstacles
                    for (int i = 1; i < currentLevel.LevelArray.GetLength(1); i++) //start at the second row because first row is level info
                    {
                        for (int j = 0; j < currentLevel.LevelArray.GetLength(0); j++)
                        {
                            if (currentLevel.LevelArray[j, i] == '-')
                            {
                                currentLevel.WallList.Add(new Wall(new Rectangle(j * 120, (i - 1) * 120, 128, 120), wall));
                            }
                            if(currentLevel.LevelArray[j, i] == 'R')
                            {
                                currentLevel.WallList.Add(new Rock(new Rectangle(j * 120, (i - 1) * 120, 120, 120), rock_small));
                            }
                        }
                    }
                }
                
                player.Move();

                //check door activations
                for(int i = 0; i < manager.DoorList.Count; i++)
                {
                    if (player.Position.Intersects(manager.DoorList[i].Position) && currentLevel.WinCheck())
                    {
                        manager.DoorList[i].DoorActivation();
                        if (manager.DoorList[i].DoorTransistion(player))
                        {
                            currentLevel = new Level(LevelIO(manager.DoorList[i].NextLevel), manager);
                            levelLoaded = false;
                        }
                    }
                }

                //check items
                for(int i = 0; i < manager.ItemList.Count; i++)
                {
                    if (player.Position.Intersects(manager.ItemList[i].Position) && manager.ItemList[i].Visible)
                    {
                        if(manager.ItemList[i].Name == "smallCoin" || manager.ItemList[i].Name == "largeCoin")
                        {
                            player.PickUpCurrency(manager.ItemList[i]);
                        }
                        else if (manager.ItemList[i].Name == "store")
                        {
                            gameState = GameState.Shop;
                        }
                        else
                        {
                            player.PickUpItem(manager.ItemList[i]);
                        }
                    }
                }

                //check enemies
                for(int i = 0; i < manager.EnemyList.Count; i++)
                {
                    if(manager.EnemyList[i] != null)
                    {
                        if (manager.EnemyList[i].Position.Intersects(player.Position))
                        {

                            manager.EnemyList[i].TakeDamage(player, manager.EnemyList[i]);
                            player.Knockback(manager.EnemyList[i]);
                            
                            if(player.Health <= 0)
                            {
                                gameState = GameState.EndGame;
                            }
                        }                      
                            manager.EnemyList[i].Move(player);
                    }
                }

                //check if enemies are hit by player
                List<Enemies> hitEnemies = new List<Enemies>();
                if (SingleButtonPress(Keys.J))
                {
                    player.Attacking = true;
                    for(int i = 0; i < manager.EnemyList.Count; i++)
                    {
                        if(manager.EnemyList[i] != null)
                        {
                            if(kbState.IsKeyDown(Keys.D) && new Rectangle(player.Position.X + player.Position.Width, player.Position.Y, 100, 100).Intersects(manager.EnemyList[i].Position))
                            {
                                hitEnemies.Add(manager.EnemyList[i]);
                                if(player.Weapon.Type != WeaponType.basic)
                                {
                                    manager.EnemyList[i].Affected = true;
                                }
                            }
                            else if (kbState.IsKeyDown(Keys.A) && new Rectangle(player.Position.X - 100, player.Position.Y, 100, 100).Intersects(manager.EnemyList[i].Position))
                            {
                                hitEnemies.Add(manager.EnemyList[i]);
                                if (player.Weapon.Type != WeaponType.basic)
                                {
                                    manager.EnemyList[i].Affected = true;
                                }
                            }
                        }
                    }
                }
                manager.WeaponAffects(player);

                //deal damage to enemies, do knockback, and determine attack animation
                //if moving left use left attack
                if (kbState.IsKeyDown(Keys.A))
                {
                    for(int i = 0; i < hitEnemies.Count; i++)
                    {
                        hitEnemies[i].Knockback(player);
                    }

                    player.Attack(player, hitEnemies, attack2);               
                }
                //else use right attack
                else
                {
                    for(int i = 0; i < hitEnemies.Count; i++)
                    {
                        hitEnemies[i].Knockback(player);
                    }

                    player.Attack(player, hitEnemies, attack1);
                }
                for(int i = 0; i < manager.EnemyList.Count; i++)
                {
                    if (manager.EnemyList[i] != null)
                    {
                        if (manager.EnemyList[i].Health <= 0)
                        {
                            manager.EnemyList.RemoveAt(i);
                            manager.EnemyList.Insert(i, null);
                        }
                    }
                }

                //check wall collision
                for(int i = 0; i < currentLevel.WallList.Count; i++)
                {
                    currentLevel.WallList[i].Collision(player, player.PrevPos, this);

                    for(int k = 0; k < manager.EnemyList.Count; k++)
                    {
                        if(manager.EnemyList[k] != null)
                        {
                            currentLevel.WallList[i].Collision(manager.EnemyList[k], manager.EnemyList[k].PrevPos, this);
                        }
                    }
                }

                //transition to inventory screen
                if (SingleButtonPress(Keys.I))
                {
                    gameState = GameState.Inventory;
                }
                if (SingleButtonPress(Keys.P))
                {
                    gameState = GameState.Pause;
                }
            }
            #endregion
            #region Pause
            if (gameState == GameState.Pause)
            {
                if (ButtonClicked(735, 595, 1185, 685))
                {
                    gameState = GameState.Game;
                }
                if (ButtonClicked(800, 720, 1120, 805))
                {
                    gameState = GameState.MainMenu;
                }
                if (ButtonClicked(830, 840, 1090, 925))
                {
                    Exit();
                }
            }
            #endregion
            #region Inventory
            if (gameState == GameState.Inventory)
            {
                if (SingleButtonPress(Keys.Enter))
                {
                    gameState = GameState.Game;
                }
            }
            #endregion
            #region Shop
            if (gameState == GameState.Shop)
            {
                //if player trys to buy something that doesn't exist it will crash.
                //currently thinking that the player will only be able to buy one item at a shop and therefore no need to fix this
                if (!usedShop)
                {
                    if (SingleButtonPress(Keys.D1))
                    {
                        shopMessage = shop.BuyItem(player, shop.ShopInv[0]);
                        usedShop = true;
                    }
                    if (SingleButtonPress(Keys.D2))
                    {
                        shopMessage = shop.BuyItem(player, shop.ShopInv[1]);
                        usedShop = true;
                    }
                    if (SingleButtonPress(Keys.D3))
                    {
                        shopMessage = shop.BuyItem(player, shop.ShopInv[2]);
                        usedShop = true;
                    }
                }
                if (kbState.IsKeyDown(Keys.Enter))
                {
                    //move the player back one pixel so that the game does not instantly send them back in
                    //this is a temporary solution will need to be adjusted based on position of player according to the door
                    Rectangle temp = player.Position;
                    temp.X -= 5;
                    player.Position = temp;
                    gameState = GameState.Game;
                }
            }
            #endregion
            #region End Game
            if (gameState == GameState.EndGame)
            {
                levelLoaded = false;
                manager.EnemyList.Clear();
                manager.ItemList.Clear();
                manager.DoorList.Clear();
                player.Inventory.Clear();
                player.InvList.Clear();
                player.Health = 100;
                
                //button logic
                if(ButtonClicked(700, 560, 1205, 660))//resart
                {
                    currentLevel = new Level(LevelIO("level1_1"), manager);
                    gameState = GameState.Game;
                }
                if(ButtonClicked(640, 740, 1275, 835))//main menu
                {
                    player.Health = 100;
                    gameState = GameState.MainMenu;
                }
            }
            #endregion

            previous = kbState;

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
                if (new Rectangle(210, 530, 405, 140).Contains(mouseState.Position))
                {
                    spriteBatch.Draw(play_hover, new Rectangle(240, 550, play_hover.Width, play_hover.Height), Color.White);

                }
                if (new Rectangle(210, 670, 380, 230).Contains(mouseState.Position))
                {
                    spriteBatch.Draw(quit_hover, new Rectangle(245, 730, quit_hover.Width, quit_hover.Height), Color.White);
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

                spriteBatch.Draw(levelScreen, new Vector2(0, 0), Color.White);

                //draw walls
                for(int i = 0; i < currentLevel.WallList.Count; i++)
                {                   
                   spriteBatch.Draw(currentLevel.WallList[i].Texture, currentLevel.WallList[i].Position, Color.White);
                }

                //draw player
                if (player.WalkRight)
                {
                    spriteBatch.Draw(player.CurrentSprite, player.Position, new Rectangle((player.WalkTimer / 3) * 60, 0, 60, 127), Color.White);
                }
                if (player.WalkLeft)
                {
                    spriteBatch.Draw(player.CurrentSprite, player.Position, new Rectangle((player.WalkTimer / 3) * 60, 0, 60, 127), Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);
                }
                else if(!player.WalkLeft && !player.WalkRight)
                {
                    spriteBatch.Draw(player.CurrentSprite, player.Position, Color.White);
                }

                //draw all doors on level
                for(int i = 0; i < manager.DoorList.Count; i++)
                {
                    if (manager.DoorList[i].Activated)
                    {
                        spriteBatch.Draw(manager.DoorList[i].CurrentTexture, manager.DoorList[i].Position, new Rectangle((manager.DoorList[i].Timer/20) * 128, 0, 128, 120), Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(manager.DoorList[i].CurrentTexture, manager.DoorList[i].Position, Color.White);
                    }
                }
                
                //draw all items on level
                for (int i = 0; i < manager.ItemList.Count; i++)
                {
                    if (manager.ItemList[i].Visible)
                    {
                        spriteBatch.Draw(manager.ItemList[i].Texture, manager.ItemList[i].Position, Color.White);
                    }
                }

                //draw all enemies on level
                for(int i = 0; i < manager.EnemyList.Count; i++)
                {
                    if(manager.EnemyList[i] != null)
                    {
                        spriteBatch.Draw(manager.EnemyList[i].Texture, manager.EnemyList[i].Position, Color.White);
                    }
                }

                //draw attacking sprites
                Color atkColor = Color.White;
                if (player.Inventory.ContainsKey("+1weapon"))
                {
                    atkColor = Color.Red;
                }
                if (player.Attacking && kbState.IsKeyDown(Keys.A))
                { 
                    spriteBatch.Draw(player.CurrentAtkSprite,
                        new Rectangle(player.Position.X - 100, player.Position.Y - 10, 100, 100), atkColor);
                }
                else if(player.Attacking)
                {
                    spriteBatch.Draw(player.CurrentAtkSprite,
                        new Rectangle(player.Position.X + player.Position.Width, player.Position.Y - 10, 100, 100), atkColor);
                }
            }
            #endregion
            #region Pause
            if (gameState == GameState.Pause)
            {
                spriteBatch.Draw(levelScreen, new Vector2(0, 0), Color.White);
                spriteBatch.Draw(pause, new Vector2(0, 0), Color.White);
            }
            #endregion
            #region Inventory
            if (gameState == GameState.Inventory)
            {
                spriteBatch.Draw(inventoryScreen, new Vector2(0, 0), Color.White);
                foreach (string item in player.InvList)
                {
                    spriteBatch.Draw(player.Inventory[item].Texture, new Rectangle(555 + (player.InvList.IndexOf(item)/3 * 170), 
                        330 + player.InvList.IndexOf(item)%3 * 205,
                        135, 135), Color.White);
                    spriteBatch.DrawString(Arial12, item, new Vector2(560 + (player.InvList.IndexOf(item)/3 * 170),
                         490 + player.InvList.IndexOf(item)%3 * 205), Color.Black);
                }
                spriteBatch.DrawString(Arial12, "Currency: " + player.Currency, new Vector2(25, 25), Color.White);
            }
            #endregion
            #region Shop
            if (gameState == GameState.Shop)
            {
                spriteBatch.Draw(levelScreen, new Vector2(0, 0), Color.White);
                for(int i = 0; i < shop.ShopInv.Count; i++)
                {
                    
                    spriteBatch.Draw(shop.ShopInv[i].Texture, new Rectangle(GraphicsDevice.Viewport.Width / 3 + (200 * i), GraphicsDevice.Viewport.Height / 2 - 50, 100, 100), Color.White);
                    spriteBatch.DrawString(Arial12, shop.ShopInv[i].Name, new Vector2(25 + GraphicsDevice.Viewport.Width / 3 + (200 * i), 110 + GraphicsDevice.Viewport.Height / 2 - 50), Color.White);
                    spriteBatch.DrawString(Arial12, "Currency: " + player.Currency, new Vector2(25, 25), Color.White);


                }
            }
            #endregion
            #region End Game
            if (gameState == GameState.EndGame)
            {
                spriteBatch.Draw(levelScreen, new Vector2(0, 0), Color.White);
                spriteBatch.Draw(gameOver, new Vector2(0, 0), Color.White);
            }
            #endregion

            spriteBatch.DrawString(Arial12, "X: " + mouseState.X + "\nY: " + mouseState.Y, new Vector2(100, 100), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        //helper methods
        public bool SingleButtonPress(Keys key)
        {
            if (kbState.IsKeyDown(key) && !previous.IsKeyDown(key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        //level input methods
        public char[,] LevelIO(string levelName)
        {
            //clear lists before next level is loaded otherwise things may carry over through levels
            manager.DoorList.Clear();
            manager.ItemList.Clear();
            manager.EnemyList.Clear();
            char[,] levelArray = new char[16, 10];
            try
            {
                reader = new StreamReader("../../../../" + levelName + ".txt");
                string line = reader.ReadLine();
                while (line != null)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        for (int j = 0; j < line.Length; j++)
                        {
                            levelArray[j, i] = line[j];
                        }
                        line = reader.ReadLine();
                    }
                } 

                reader.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return levelArray;
        }
        
        public string LevelInfo(char[,] level)
        {
            string levelInfo = null;
            for (int i = 0; i < level.GetLength(1); i++)
            {             
                for (int j = 0; j < level.GetLength(0); j++)
                {
                    if(i == 0)
                    {
                        if (level[j, i] == '\0')
                        {
                            levelInfo += ",";
                            i++;
                        }
                        else
                        {
                            levelInfo += level[j,i];
                        }
                    }
                    else
                    {
                        if(level[j,i] != '-' & level[j,i] != ' ')
                        {
                            levelInfo = levelInfo + level[j, i].ToString() + j + "," + (i - 1) + ",";
                        }
                    }
                }
            }
            levelLoaded = true;
            return levelInfo;
        }

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
            if ((mouseState.X >= tlx && mouseState.Y >= tly) && (mouseState.X < brx && mouseState.Y < bry) && mouseState.LeftButton == ButtonState.Pressed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void FillShop(Player player, ShopManager store)
        {
            if (!player.Inventory.ContainsKey("weapon"))
            {
                store.AddToShop(new Weapon(WeaponType.basic, "weapon", new Rectangle(50, 50, 10, 10), base_weapon), 0);
            }
            else
            {
                //determining random weapon upgrade
                int weaponVersion = rng.Next(4);

                //determining random weapon upgrade
                if(weaponVersion == 0)
                {
                    store.AddToShop(new Weapon(WeaponType.basic, "+1" + player.Weapon.Name, new Rectangle(50, 50, 10, 10), base_weapon), 0);
                }
                else if(weaponVersion == 1)
                {
                    store.AddToShop(new ShockWeapon(WeaponType.shock, "Weapon of shock", new Rectangle(50, 50, 10, 10), base_weapon), 0);
                }
                else if(weaponVersion == 2)
                {
                    store.AddToShop(new FireWeapon(WeaponType.fire, "weapon of fire", new Rectangle(50, 50, 10, 10), base_weapon), 0);
                }
                else if(weaponVersion == 3)
                {
                    store.AddToShop(new FrostWeapon(WeaponType.frost, "weapon of frost", new Rectangle(50, 50, 10, 10), base_weapon), 0);
                }

            }
            if (!player.Inventory.ContainsKey("armor"))
            {
                store.AddToShop(new Item("armor", new Rectangle(50, 50, 10, 10), base_armor), 0);
            }
            else
            {
                //determining random armor upgrade
                int armorVersion = rng.Next(4);


                if(armorVersion == 0)
                {
                    store.AddToShop(new Item("+1" + player.Armor.Name, new Rectangle(50, 50, 10, 10), base_armor), 0);
                }
                else if(armorVersion == 1)
                {
                    store.AddToShop(new ThornArmor(ArmorType.test, "Armor of thorns", new Rectangle(50, 50, 10, 10), base_armor), 0);
                }
                else if(armorVersion == 2)
                {
                    store.AddToShop(new ShieldArmor(ArmorType.test, "Armor of shielding", new Rectangle(50, 50, 10, 10), base_armor), 0);
                }
                else if(armorVersion == 3)
                {
                    store.AddToShop(new SpeedArmor(ArmorType.test, "Armor of speed", new Rectangle(50, 50, 10, 10), base_armor), 0);
                }
            }
        }

    }
}
