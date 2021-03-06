﻿using Microsoft.Xna.Framework;
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
        EndGame,
        Won
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
        Manager manager;
        ShopManager shop;
        Level currentLevel;
        Rectangle temp;
        

        //create stream reader and writer
        StreamReader reader;
        //lists sprites for animations
        List<Texture2D> attack1;
        List<Texture2D> attack2;

        string shopMessage; //not sure if this will get replaced but would probably be cleaner if there is a way to skip this


        //helper fields
        KeyboardState previous; //keyboardstate to help with tracking single keyboard inputs
        bool levelLoaded;
        string levelData;
        MouseState mouseState;
        MouseState prevMouseState;
        


        // sprite fields
        #region Sprites

        // screens
        Texture2D mainMenu;
        Texture2D levelScreen;
        Texture2D pause;
        Texture2D gameOver;
        Texture2D inventoryScreen;
        Texture2D shopScreen;
        Texture2D winScreen;

        //layers
        Texture2D darkLayer;

        //objects
        Texture2D door_locked;
        Texture2D door_open;
        Texture2D door_open_animation;
        Texture2D wall;
        Texture2D base_weapon;
        Texture2D base_armor;
        Texture2D fire_weapon;
        Texture2D frost_weapon;
        Texture2D shock_weapon;
        Texture2D shield_armor;
        Texture2D speed_armor;
        Texture2D thorn_armor;
        Texture2D silver_coin;
        Texture2D gold_coin;
        Texture2D store;
        Texture2D health_potion;
        Texture2D light_Source;
        Texture2D surronding_light;

        //indicators
        Texture2D fire_indicator;
        Texture2D frost_indicator;
        Texture2D shock_indicator;
        Texture2D healthbar;
        Texture2D healthbar_chunk;
        Texture2D health_indicator;

        //sprites
        Texture2D player_forward;
        Texture2D player_backward;
        Texture2D player_walk_side;
        Texture2D rock_large;
        Texture2D rock_small;
        Texture2D enemy_1;
        Texture2D enemy_2;
        Texture2D enemy_2_90;

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
        Texture2D shop_hover;

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
            manager = new Manager(player);

            Arial12 = Content.Load<SpriteFont>("arial12");

            //attack animation sprite list
            attack1 = new List<Texture2D>();
            attack2 = new List<Texture2D>();

            //level isnt currently loaded
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
            mainMenu = Content.Load<Texture2D>("Screens//Main_menu2");
            levelScreen = Content.Load<Texture2D>("Screens//Level");
            wall = Content.Load<Texture2D>("Screens//Wall");
            pause = Content.Load<Texture2D>("Screens//Pause");
            gameOver = Content.Load<Texture2D>("Screens//Game_over");
            inventoryScreen = Content.Load<Texture2D>("Screens//Inventory");
            shopScreen = Content.Load<Texture2D>("Screens//Shop_Screen");
            winScreen = Content.Load<Texture2D>("Screens//Win_Screen");

            //layer loading
            darkLayer = Content.Load<Texture2D>("Sprites//blank_rectangle");

            //object loading
            door_locked = Content.Load<Texture2D>("Sprites//door_locked");
            door_open = Content.Load<Texture2D>("Sprites//door_unlocked");
            door_open_animation = Content.Load<Texture2D>("Sprites//door_unlocking_animated");
            rock_large = Content.Load<Texture2D>("Sprites//rock_big");
            rock_small = Content.Load<Texture2D>("Sprites//rock_small");
            base_weapon = Content.Load<Texture2D>("Sprites//base_weapon");
            base_armor = Content.Load<Texture2D>("Sprites//base_armor");
            fire_weapon = Content.Load<Texture2D>("Sprites//fire_weapon");
            shield_armor = Content.Load<Texture2D>("Sprites//shield_armor");
            frost_weapon = Content.Load<Texture2D>("Sprites//frost_weapon");
            speed_armor = Content.Load<Texture2D>("Sprites//speed_armor");
            shock_weapon = Content.Load<Texture2D>("Sprites//shock_weapon");
            thorn_armor = Content.Load<Texture2D>("Sprites//thorn_armor");
            silver_coin = Content.Load<Texture2D>("Sprites//silver_coin");
            gold_coin = Content.Load<Texture2D>("Sprites//gold_coin");
            store = Content.Load<Texture2D>("Sprites//store_stand");
            health_potion = Content.Load<Texture2D>("Sprites//health_potion");
            light_Source = Content.Load<Texture2D>("Sprites//light_source");
            surronding_light = Content.Load<Texture2D>("Sprites//light_surrounding");

            //indicator loading
            fire_indicator = Content.Load<Texture2D>("Sprites//fire_indicator");
            frost_indicator = Content.Load<Texture2D>("Sprites//frost_indicator");
            shock_indicator = Content.Load<Texture2D>("Sprites//shock_indicator");
            healthbar = Content.Load<Texture2D>("Sprites//healthbar");
            healthbar_chunk = Content.Load<Texture2D>("Sprites//healthbar_chunk");
            health_indicator = Content.Load<Texture2D>("Sprites//health_indicator");

            //sprite loading
            player_forward = Content.Load<Texture2D>("Sprites//player_forward");
            player_backward = Content.Load<Texture2D>("Sprites//player_backward");
            player_walk_side = Content.Load<Texture2D>("Sprites//player_walk_side");
            enemy_1 = Content.Load<Texture2D>("Sprites//enemy_1");
            enemy_2 = Content.Load<Texture2D>("Sprites//enemy_2_animated");
            enemy_2_90 = Content.Load<Texture2D>("Sprites//enemy_2_animated_90");

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
            shop_hover = Content.Load<Texture2D>("Sprites//shop_hover");

            //initialize the player
            playerWeapon = new Weapon(WeaponType.basic, "weapon", new Rectangle(50, 250, 40, 40), base_weapon); //all values in here are just for test
            playerArmor = null; // new Armor(ArmorType.test, "armor", new Rectangle(50, 50, 10, 10), base_armor); //all values in here are just for test too
            player = new Player(0, playerWeapon, playerArmor, player_walk_side, player_backward, player_forward, 10, 3, new Rectangle(100, 100, 45, 75), player_forward); //all values in here are just for test as well

            //initialize the shop and current level objects 
            shop = new ShopManager();
            currentLevel = new Level(LevelIO("level1_1"), manager);
            MouseState mouseState = new MouseState();
            MouseState prevMouseState = new MouseState();
            #endregion

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
                        if (levelData[i].Equals('E') || levelData[i].Equals('F') || levelData[i].Equals('C') || levelData[i].Equals('B'))
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
                                manager.EnemyList.Add(new Fly(currentLevel, rng, 20, 4, new Rectangle(temp.X, temp.Y, 50, 50), enemy_1));
                            }
                            else if(levelData[i] == 'C')
                            {
                                manager.EnemyList.Add(new Centipede(currentLevel, player, rng, 30, 8, new Rectangle(temp.X, temp.Y, 200, 60), enemy_2, enemy_2_90));
                            }
                            else if(levelData[i] == 'B')
                            {
                                manager.EnemyList.Add(new Boss(currentLevel, rock_small, rng, 20, 7, new Rectangle(temp.X, temp.Y, 50, 50), enemy_1));
                            }
                            else
                            {
                                manager.EnemyList.Add(new Enemies(currentLevel, rng, 40, 6, new Rectangle(temp.X, temp.Y, 100, 100), enemy_1));
                            }

                        }

                        //spawn light sources
                        if (levelData[i].Equals('L'))
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
                            manager.LightList.Add(new LightSource(surronding_light, new Rectangle(temp.X, temp.Y, 100, 100), light_Source));
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
                            FillShop(player, shop);
                            manager.ItemList.Add(new Item("store", new Rectangle(temp.X, temp.Y, store.Width, store.Height), store));
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
                
                player.Move(gameTime);

                //check door activations
                for(int i = 0; i < manager.DoorList.Count; i++)
                {
                    if (player.Position.Intersects(manager.DoorList[i].Position) && currentLevel.WinCheck())
                    {
                        manager.DoorList[i].DoorActivation(gameTime);
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
                        if (manager.EnemyList[i].Position.Intersects(player.Position) && !(manager.EnemyList[i] is Boss))
                        {
                            manager.EnemyList[i].TakeDamage(player, manager.EnemyList[i]);
                            player.Knockback(manager.EnemyList[i]);                            
                        }                      
                        if(manager.EnemyList[i] is Centipede || manager.EnemyList[i] is Boss)
                        {
                            manager.EnemyList[i].Move(player, gameTime);
                        }
                        else
                        {
                            manager.EnemyList[i].Move(player, gameTime);
                        }
                        if(manager.EnemyList[i] is Boss temp)
                        {
                            temp.Attack(player, gameTime);
                            manager.EnemyList[i] = temp;
                        }
                    }
                }
                if (player.Health <= 0)
                {
                    gameState = GameState.EndGame;
                }

                //check if enemies are hit by player
                List<Enemies> hitEnemies = new List<Enemies>();
                if(SingleMouseClick() && !player.Attacking)
                {
                    player.Attacking = true;
                    if(mouseState.X > player.Position.X)
                    {
                        player.RightAttack = true;
                    }
                    else
                    {
                        player.LeftAttack = true;
                    }
                   for(int i = 0; i < manager.EnemyList.Count; i++)
                    {
                        if(manager.EnemyList[i] != null)
                        {
                            //if the player's attack connects with an enemy add them to a list of hit enemies
                            if(player.RightAttack && new Rectangle(player.Position.X + player.Position.Width, player.Position.Y, 100, 100).Intersects(manager.EnemyList[i].Position))
                            {
                                hitEnemies.Add(manager.EnemyList[i]);
                                //if(player.Weapon != null)
                                //{
                                //    manager.EnemyList[i].Affected = true;
                                //}
                            }
                            else if (player.LeftAttack && new Rectangle(player.Position.X - 100, player.Position.Y, 100, 100).Intersects(manager.EnemyList[i].Position))
                            {
                                hitEnemies.Add(manager.EnemyList[i]);
                                //if (player.Weapon != null)
                                //{
                                //    manager.EnemyList[i].Affected = true;
                                //}
                            }
                        }
                        //flag hit enemies to be affected by the player's weapon if it has an effect
                        for(int k = 0; k < hitEnemies.Count; k++)
                        {
                            hitEnemies[k].Affected = true;
                        }
                    }
                }
                //apply weapon affects to enemies
                manager.WeaponAffects(player, gameTime);

                //deal damage to enemies, do knockback, and determine attack animation
                //if moving left use left attack
                if (player.LeftAttack)
                {
                    for(int i = 0; i < hitEnemies.Count; i++)
                    {
                        hitEnemies[i].Knockback(player);
                    }

                    player.Attack(player, hitEnemies, attack2, gameTime);               
                }
                //else use right attack
                else if(player.RightAttack)
                {
                    for(int i = 0; i < hitEnemies.Count; i++)
                    {
                        hitEnemies[i].Knockback(player);
                    }

                    player.Attack(player, hitEnemies, attack1, gameTime);
                }
                //check if any enemies where killed
                for(int i = 0; i < manager.EnemyList.Count; i++)
                {
                    if (manager.EnemyList[i] != null)
                    {
                        if (manager.EnemyList[i].Health <= 0)
                        {
                            //random currency drop
                            if (rng.Next(4) == 1)
                            {
                                manager.ItemList.Add(new Item("largeCoin", new Rectangle(manager.EnemyList[i].Position.X, manager.EnemyList[i].Position.Y,
                                    50, 50), gold_coin));
                            }
                            else
                            {
                                manager.ItemList.Add(new Item("smallCoin", new Rectangle(manager.EnemyList[i].Position.X, manager.EnemyList[i].Position.Y,
                                    50, 50), silver_coin));
                            }
                            //remove from manager enemy list and insert a null placeholder
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

                //Hotkey for health potions
                if (SingleButtonPress(Keys.H))
                {
                    for(int i = 0; i < player.InvList.Count; i++)
                    {
                        if(player.Inventory[player.InvList[i]] is HealthPotion)
                        {
                            player.UseHealthPotion((HealthPotion)player.Inventory[player.InvList[i]]);
                            break;
                        }
                    }
                }
                //if player used a health potion display an effect above their character
                if (player.Healed)
                {
                    player.HealIndiDuration += gameTime.ElapsedGameTime.TotalSeconds;

                    if (player.HealIndiDuration > player.HealIndiTimer)
                    {
                        player.Healed = false;
                        player.HealIndiDuration = 0;
                    }
                }

                //transition to inventory screen
                if (SingleButtonPress(Keys.I))
                {
                    gameState = GameState.Inventory;
                }
                //transition to pause screen
                if (SingleButtonPress(Keys.P))
                {
                    gameState = GameState.Pause;
                }
            }
            #endregion
            #region Pause
            //if the player pauses the game either continue, go to the main menu, or quit based on what they click
            if (gameState == GameState.Pause)
            {
                if (ButtonClicked(735, 595, 1185, 685))//resume
                {
                    gameState = GameState.Game;
                }
                if (ButtonClicked(800, 720, 1120, 805))//main menu
                {
                    gameState = GameState.MainMenu;
                }
                if (ButtonClicked(830, 840, 1090, 925))//quit
                {
                    Exit();
                }
            }
            #endregion
            #region Inventory
            if (gameState == GameState.Inventory)
            {

                for(int i = 0; i < player.InvList.Count; i++)
                {
                    //if the player has defeated all enemies on the level they can swap which piece of armor and what weapon they have equiped
                    if (currentLevel.WinCheck())
                    {
                        //swap whatever the current piece of armor or weapon the player has equipped if they click on another
                        if(ButtonClicked(555 + (i / 3 * 170), 330 + i % 3 * 205,
                            690 + (i / 3 * 170), 465 + i % 3 * 205))
                        {
                            if(player.Inventory[player.InvList[i]] is Weapon)
                            {
                                player.WeaponSwap((Weapon)player.Inventory[player.InvList[i]]);
                            }
                            else if(player.Inventory[player.InvList[i]] is Armor)
                            {
                                player.ArmorSwap((Armor)player.Inventory[player.InvList[i]]);
                            }
                        }
                    }
                    //if the player clicks on a health potion use it
                    if (ButtonClicked(555 + (i / 3 * 170), 330 + i % 3 * 205,
                            690 + (i / 3 * 170), 465 + i % 3 * 205))
                    {
                        if (player.Inventory[player.InvList[i]] is HealthPotion)
                        {
                            player.UseHealthPotion((HealthPotion)player.Inventory[player.InvList[i]]);
                        }
                    }
                }

                if (SingleButtonPress(Keys.Enter))
                {
                    gameState = GameState.Game;
                }
            }
            #endregion
            #region Shop
            if (gameState == GameState.Shop)
            {
                //buy what ever the player is hovering over if the player has enough currency
                for (int i = 0; i < shop.ShopInv.Count; i++)
                {

                    if (ButtonClicked(555 + (i % 5) * 170, 320 + (320 * (i / 5)), 700 + (i % 5) * 170, 465 + (320 * (i / 5))))
                    {

                        shopMessage = shop.BuyItem(player, shop.ShopInv[i]);
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
                player.Weapon = null;
                player.Armor = null;
                player.Health = 10;
                player.Currency = 0;
                player.Hit = false;
                
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
            #region Won
            if(gameState == GameState.Won)
            {

            }
            #endregion

            previous = kbState;
            prevMouseState = mouseState;
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
                if (new Rectangle(210, 668, 380, 160).Contains(mouseState.Position))
                {
                    spriteBatch.Draw(quit_hover, new Rectangle(245, 728, quit_hover.Width, quit_hover.Height), Color.White);
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


                //draw all doors on level
                for(int i = 0; i < manager.DoorList.Count; i++)
                {
                    if (manager.DoorList[i].Activated)
                    {
                        spriteBatch.Draw(manager.DoorList[i].CurrentTexture, manager.DoorList[i].Position, new Rectangle(manager.DoorList[i].Frame * 128, 0, 128, 120), Color.White);
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
                        if(manager.EnemyList[i] is Centipede temp)
                        {
                            if (temp.MoveUp)
                            {
                                spriteBatch.Draw(temp.RotTexture, manager.EnemyList[i].Position, new Rectangle(temp.Frame * 54, 0, 54, 200), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0f);
                            }
                            if (temp.MoveDown)
                            {
                                spriteBatch.Draw(temp.RotTexture, manager.EnemyList[i].Position, new Rectangle(temp.Frame* 54, 0, 54, 200), Color.White, 0, Vector2.Zero, SpriteEffects.FlipVertically, 0f);
                            }
                            if (temp.MoveRight)
                            {
                                spriteBatch.Draw(manager.EnemyList[i].Texture, manager.EnemyList[i].Position, new Rectangle(temp.Frame * 200, 0, 200, 54), Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);
                            }
                            if (temp.MoveLeft)
                            {
                                spriteBatch.Draw(manager.EnemyList[i].Texture, manager.EnemyList[i].Position, new Rectangle(temp.Frame * 200, 0, 200, 54), Color.White);
                            }
                        }
                        else
                        {
                         spriteBatch.Draw(manager.EnemyList[i].Texture, manager.EnemyList[i].Position, Color.White);

                        }
                        if(player.Weapon is FireWeapon && manager.EnemyList[i].Affected)
                        {
                            spriteBatch.Draw(fire_indicator, new Rectangle(manager.EnemyList[i].Position.X + (manager.EnemyList[i].Position.Width / 2 - 17),
                                manager.EnemyList[i].Position.Y - 40, 35, 35), Color.White);
                        }
                        if(player.Weapon is ShockWeapon && manager.EnemyList[i].Affected)
                        {
                            spriteBatch.Draw(shock_indicator, new Rectangle(manager.EnemyList[i].Position.X + (manager.EnemyList[i].Position.Width / 2 - 17),
                                manager.EnemyList[i].Position.Y - 40, 35, 35), Color.White);
                        }
                        if(player.Weapon is FrostWeapon && manager.EnemyList[i].Affected)
                        {
                            spriteBatch.Draw(frost_indicator, new Rectangle(manager.EnemyList[i].Position.X + (manager.EnemyList[i].Position.Width / 2 - 17),
                                manager.EnemyList[i].Position.Y - 40, 35, 35), Color.White);
                        }

                        if(manager.EnemyList[i] is Boss tempEn)
                        {
                            for(int k = 0; k < tempEn.ProjList.Count; k++)
                            {
                                spriteBatch.Draw(tempEn.ProjList[k].Texture, tempEn.ProjList[k].Position, Color.White);
                            }
                        }
                    }
                }
                


                //draw attacking sprites
                Color atkColor = Color.White;
                if (player.Attacking && player.LeftAttack)
                { 
                    spriteBatch.Draw(player.CurrentAtkSprite,
                        new Rectangle(player.Position.X - 100, player.Position.Y - 10, 100, 100), atkColor);
                }
                else if(player.Attacking)
                {
                    spriteBatch.Draw(player.CurrentAtkSprite,
                        new Rectangle(player.Position.X + player.Position.Width, player.Position.Y - 10, 100, 100), atkColor);
                }

                //draw player

                //if the player is walking left or right draw the corresponding animation
                //if the player is attacking do not swap the animation
                if (!player.HitEffect(gameTime))
                {
                    if (player.WalkRight && !player.WalkLeft)
                    {
                        if (player.LeftAttack)
                        {
                            spriteBatch.Draw(player.CurrentSprite, player.Position, new Rectangle(player.WalkFrame * 60, 0, 60, 127), Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);
                        }
                        else
                        {
                            spriteBatch.Draw(player.CurrentSprite, player.Position, new Rectangle(player.WalkFrame * 60, 0, 60, 127), Color.White);
                        }
                    }
                    if (player.WalkLeft && !player.WalkRight)
                    {
                        if (player.RightAttack)
                        {
                            spriteBatch.Draw(player.CurrentSprite, player.Position, new Rectangle(player.WalkFrame * 60, 0, 60, 127), Color.White);
                        }
                        else
                        {
                            spriteBatch.Draw(player.CurrentSprite, player.Position, new Rectangle(player.WalkFrame * 60, 0, 60, 127), Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);
                        }
                    }
                    else if((!player.WalkLeft && !player.WalkRight) || (player.WalkLeft && player.WalkRight))
                    {
                        spriteBatch.Draw(player.CurrentSprite, player.Position, Color.White);
                    }

                    if (player.Healed)
                    {
                        spriteBatch.Draw(health_indicator, new Rectangle(player.Position.X + (player.Position.Width / 2) - (health_indicator.Width/2),
                            player.Position.Y - 20, health_indicator.Width, health_indicator.Height), Color.White);
                    }
                }
                spriteBatch.DrawString(Arial12, "HP", new Vector2(26, 55), Color.White);
                for (int i = 0; i < player.Health; i++)
                {
                    if ((i % 2) == 0)
                    {
                        spriteBatch.Draw(healthbar_chunk, new Rectangle(55 + (i * 13), 55, 23, 50), null, Color.Blue, 0, Vector2.Zero, SpriteEffects.FlipVertically, 0f);
                    }
                    else
                    {
                        spriteBatch.Draw(healthbar_chunk, new Rectangle(55 + (i * 13), 55, 23, 50), Color.Black);
                    }
                }

                //draw dark layer over
                spriteBatch.Draw(darkLayer, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.Black * .3f);
                //draw all lightsources
                for (int i = 0; i < manager.LightList.Count; i++)
                {
                    spriteBatch.Draw(manager.LightList[i].LightEffect, manager.LightList[i].LightEffectPos, manager.LightList[i].Flicker(gameTime));
                    spriteBatch.Draw(manager.LightList[i].Texture, manager.LightList[i].Position, Color.White);
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
                //draw each sprite and its corresponding name in the correct box in the inventory screen
                foreach (string item in player.InvList)
                {
                    spriteBatch.Draw(player.Inventory[item].Texture, new Rectangle(555 + (player.InvList.IndexOf(item)/3 * 170), 
                        330 + player.InvList.IndexOf(item)%3 * 205,
                        135, 135), Color.White);
                    spriteBatch.DrawString(Arial12, item, new Vector2(560 + (player.InvList.IndexOf(item)/3 * 170),
                         490 + player.InvList.IndexOf(item)%3 * 205), Color.Black);
                }
                spriteBatch.DrawString(Arial12, player.Currency.ToString(), new Vector2(345, 275), Color.White);
            }
            #endregion
            #region Shop
            if (gameState == GameState.Shop)
            {
                spriteBatch.Draw(shopScreen, new Vector2(0, 0), Color.White);
                for(int i = 0; i < shop.ShopInv.Count; i++)
                {
                    //highlight currently selected item in the shop 
                    if (new Rectangle(555 + (i % 5) * 170, 320 + (320 * (i / 5)), 145, 145).Contains(mouseState.Position))
                    {
                        spriteBatch.Draw(shop_hover, new Vector2(504 + (i % 5) * 167, 289 + (320 * (i / 5))), Color.White);
                    }
                    //draw sprites and their corresponding text in the correct box in the shop
                    spriteBatch.Draw(shop.ShopInv[i].Texture, new Rectangle(570 + (i % 5) * 170, 345 + 320 * (i / 5), 100, 100), Color.White);
                    spriteBatch.DrawString(Arial12, shop.ShopInv[i].Name, new Vector2(560 + (i % 5) * 170, 490 + 315 * (i / 5)), Color.Black);
                    spriteBatch.DrawString(Arial12, shop.ShopInv[i].Cost.ToString(), new Vector2(570 + (i % 5) * 170, 535 + 315 * (i / 5)), Color.Black);
                    spriteBatch.DrawString(Arial12, player.Currency.ToString(), new Vector2(340, 280), Color.White);

                    //Commented out until shop menu has been changed to accomidate the button and and button logic in implemented
                    // adding text to exit shop
                    //spriteBatch.DrawString(Arial12, "Exit Shop", new Vector2(GraphicsDevice.Viewport.Width / 2, 280), Color.Black);
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
            #region Won
            if(gameState == GameState.Won)
            {
                spriteBatch.Draw(winScreen, new Vector2(0, 0), Color.White);
            }

            #endregion


            spriteBatch.End();
            prevMouseState = mouseState;
            base.Draw(gameTime);
        }

        //helper methods
        /// <summary>
        /// returns true only on the inital key is pressed
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
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
        
        /// <summary>
        /// returns true only on the initial frame the left mousebutton is pressed
        /// </summary>
        /// <returns></returns>
        public bool SingleMouseClick()
        {
            if(mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// method for checking if the mouse clicks in a certain region
        /// used for menu buttons
        /// </summary>
        /// <param name="tlx">top left corner y coordinate</param>
        /// <param name="tly">top left corner x coordinate</param>
        /// <param name="brx">bottom right corner y coordinate</param>
        /// <param name="bry">bottom right corner x coordinate</param>
        /// <returns></returns>
        public bool ButtonClicked(int tlx, int tly, int brx, int bry)
        {
            if ((mouseState.X >= tlx && mouseState.Y >= tly) && (mouseState.X < brx && mouseState.Y < bry) && SingleMouseClick())
            {
                prevMouseState = mouseState; //this line ensures that if the button click changes a gamestate that the preMouseState is updated 
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// randomly fills the shop
        /// always spawns one piece of armor and one weapon
        /// if the player doesnt have the base armor or weapon it will spawn one of those
        /// else it adds a random type of armor or weapon
        /// spawns one additional piece of armor or weapon
        /// </summary>
        /// <param name="player"></param>
        /// <param name="store"></param>
        private void FillShop(Player player, ShopManager store)
        {
            if (!player.Inventory.ContainsKey("weapon"))
            {
                store.AddToShop(new Weapon(WeaponType.basic, "weapon", new Rectangle(50, 50, 10, 10), base_weapon));
            }
            else
            {
                AddRandWeap();

            }

            if (!player.Inventory.ContainsKey("armor"))
            {
                store.AddToShop(new Armor("armor", new Rectangle(50, 50, 10, 10), base_armor));
            }
            else
            {
                AddRandArmor();
            }

            if (!player.Inventory.ContainsKey("weapon") || !player.Inventory.ContainsKey("armor"))
            {
                if (player.Inventory.ContainsKey("weapon"))
                {
                    AddRandWeap();
                }
                else if (player.Inventory.ContainsKey("armor"))
                {
                    AddRandArmor();
                }
                else
                {
                    if (rng.Next(2) == 1)
                    {
                        AddRandWeap();
                    }
                    else
                    {
                        AddRandArmor();
                    }
                }
            }

            int healthPckAmount = rng.Next(1, 5);

            for (int i = 0; i < healthPckAmount; i++)
            {
                shop.AddToShop(new HealthPotion(3, "health potion", new Rectangle(50, 50, 10, 10), health_potion));
            }

        }

        /// <summary>
        /// determines a random type of weapon and adds it to the store
        /// </summary>
        public void AddRandWeap()
        {
            //determining random weapon upgrade
            int weaponVersion = rng.Next(3);

            //determining random weapon upgrade
            if (weaponVersion == 0)
            {
                shop.AddToShop(new ShockWeapon(WeaponType.shock, "Weapon of shock", new Rectangle(50, 50, 10, 10), shock_weapon));
            }
            else if (weaponVersion == 1)
            {
                shop.AddToShop(new FireWeapon(WeaponType.fire, "weapon of fire", new Rectangle(50, 50, 10, 10), fire_weapon));
            }
            else if (weaponVersion == 2)
            {
                shop.AddToShop(new FrostWeapon(WeaponType.frost, "weapon of frost", new Rectangle(50, 50, 10, 10), frost_weapon));
            }
        }

        /// <summary>
        /// Determies a random type of armor and adds it to the shop
        /// </summary>
        public void AddRandArmor()
        {
            //detertermine which type of armor to add to the store
            int armorVersion = rng.Next(3);

            if (armorVersion == 0)
            {
                shop.AddToShop(new ThornArmor("Armor of thorns", new Rectangle(50, 50, 10, 10), thorn_armor));
            }
            else if (armorVersion == 1)
            {
                shop.AddToShop(new ShieldArmor("Armor of shielding", new Rectangle(50, 50, 10, 10), shield_armor));
            }
            else if (armorVersion == 2)
            {
                shop.AddToShop(new SpeedArmor("Armor of speed", new Rectangle(50, 50, 10, 10), speed_armor));
            }
        }


        //level input methods

        /// <summary>
        /// takes a 16x10 level file and converts it into a 16x10 2d char array
        /// </summary>
        /// <param name="levelName">name of the level being loaded</param>
        /// <returns></returns>
        public char[,] LevelIO(string levelName)
        {
            //check if the player has won the game
            if(levelName == "won")
            {
                gameState = GameState.Won;
                return null;

            }

            else
            {
                //clear manager lists before level is swapped
                manager.DoorList.Clear();
                manager.ItemList.Clear();
                manager.EnemyList.Clear();
                manager.LightList.Clear();
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
                currentLevel = new Level(levelArray, manager);
                return levelArray;

            }
        }
        
        /// <summary>
        /// takes a 2d character array that correlates to the level
        /// transforms the array into a string of all characters with their x and y position
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
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


    }
}
