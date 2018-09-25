using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    class Player : Characters
    {
        //fields
        private int score;
        private Weapon weapon;
        private Armor armor;
        private Dictionary<string, Item> inventory;
        private List<string> invList;
        private int currency;
        private Texture2D currentSprite; //texture2D for holding current player sprite
        private Texture2D currentAtkSprite; //texture2D for holding current frame of attack animation
        private int atkFrame;
        private int walkFrame;
        private double atkTimePerFrame;
        private double walkTimePerFrame;
        private double atkTimer;
        private double fps;
        private double walkTimer;
        private bool attacking;
        private bool walkRight;
        private bool walkLeft;
        private bool healed;
        private double healIndiDuration;
        private double healIndiTimer;
        private Texture2D sidewaysWalk;
        private Texture2D walkUp;
        private Texture2D walkDown;
        private KeyboardState previous;
        private bool leftAttack; //field for keeping track of what direction the player is attacking
        private bool rightAttack; //field for keeping track of what direction the player is attacking
        private double hitDuration;
        private double hitTimer;
        private int alternate;

        //properties
        public double HealIndiDuration
        {
            get { return healIndiDuration; }
            set { healIndiDuration = value; }
        }
        public double HealIndiTimer
        {
            get { return healIndiTimer; }
            set { healIndiTimer = value; }
        }
        public bool Healed
        {
            get { return healed; }
            set { healed = value; }
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }
        public Weapon Weapon
        {
            get { return weapon; }
            set { weapon = value; }
        }
        public Armor Armor
        {
            get { return armor; }
            set { armor = value; }
        }
        public List<string> InvList
        {
            get { return invList; }
        }
        public Dictionary<string, Item> Inventory
        {
            get { return inventory; }
        }
        public int Currency
        {
            get { return currency; }
            set { currency = value; }
        }
        public Texture2D CurrentSprite
        {
            get { return currentSprite; }
            set { currentSprite = value; }
        }
        public Texture2D CurrentAtkSprite
        {
            get { return currentAtkSprite; }
        }
        public bool Attacking
        {
            get { return attacking; }
            set { attacking = value; }
        }
        public bool WalkRight
        {
            get { return walkRight; }
        }
        public bool WalkLeft
        {
            get { return walkLeft; }
        }
        public bool LeftAttack
        {
            get { return leftAttack; }
            set { leftAttack = value; }
        }
        public bool RightAttack
        {
            get { return rightAttack; }
            set { rightAttack = value; }
        }
        public int WalkFrame
        {
            get { return walkFrame; }
        }
        
        //constructor
        public Player(int score, Weapon weapon, Armor armor, Texture2D sidewaysWalk, Texture2D walkUp, Texture2D walkDown, int health, int damage, Rectangle position, Texture2D texture ) : base(health, damage, position, texture)
        {
            moveSpeed = 3;
            this.score = score;


            //timer fields
            fps = 40.0;
            atkTimePerFrame = 1.0/ fps;
            atkTimer = 0;
            walkTimePerFrame = 1.0/ 10.0;
            healIndiTimer = 2;
            hitDuration = 0.4;
            atkTimer = 0;
            alternate = 0;


            //if the player is spawned with a weapon adjust damage accordingly
            if(weapon == null)
            {
                this.damage = damage;
            }
            else
            {
                this.damage = weapon.Damage;
            }


            attacking = false;
            prevPos = position;

            //player sprites
            currentSprite = texture;
            this.sidewaysWalk = sidewaysWalk;
            this.walkDown = walkDown;
            this.walkUp = walkUp;
            
            //player items
            this.weapon = weapon;
            this.armor = armor;

            hit = false;
            inventory = new Dictionary<string, Item>();
            invList = new List<string>();
        }

        //these next two methods are almost identical but are needed otherwise errors occur if an item with the same name
        //is in the shop and in the level
        /// <summary>
        /// method for handling items picked up from the ground
        /// </summary>
        /// <param name="item"></param>
        public void PickUpItem(Item item)
        {
            //collision maanagement
            if (!inventory.ContainsKey(item.Name))
            {
                inventory.Add(item.Name, item);
                invList.Add(item.Name);
                item.Visible = false;
            }
            //if the item added has the same name as an item already in the inventory then add a blank character to the end of its name to prevent collision 
            else
            {
                item.Name = item.Name + " ";
                PickUpItem(item);
            }
        }
        /// <summary>
        /// method for handling items  bought from the store
        /// </summary>
        /// <param name="item"></param>
        public void BuyItem(Item item)
        {
            //collision maanagement
            if (!inventory.ContainsKey(item.Name))
            {
                //if the inventory does not contain an item with the same name then add it to the inventory
                //in addition equip the item
                inventory.Add(item.Name, item);
                invList.Add(item.Name);
                if(item is Weapon)
                {
                    if(item is FireWeapon)
                    {
                        weapon = (FireWeapon)item;
                    }
                    if(item is ShockWeapon)
                    {
                        weapon = (ShockWeapon)item;
                    }
                    if(item is FrostWeapon)
                    {
                        weapon = (FrostWeapon)item;
                    }
                    else
                    {
                        weapon = (Weapon)item;
                    }
                    damage = weapon.Damage;
                }
                if(item is Armor)
                {
                    if(item is SpeedArmor)
                    {
                        armor = (SpeedArmor)item;
                    }
                    if (item is ShieldArmor)
                    {
                        armor = (ShieldArmor)item;
                    }
                    if (item is ThornArmor)
                    {
                        armor = (ThornArmor)item;
                    }
                    else
                    {
                        armor = (Armor)item;
                    }
                }
            }
            //if there is already an item with that name within the player inventory add a blank character to the end of its name to prevent collision
            else
            {
                item.Name = item.Name + " ";
                PickUpItem(item);
            }
        }
        
        //increase the player's currency depending on the coin picked up
        public void PickUpCurrency(Item item)
        {
            if(item.Name == "largeCoin")
            {
                currency += 5;
            }
            else if (item.Name == "smallCoin")
            {
                currency += 1;
            }
            item.Visible = false;
        }

        /// <summary>
        /// attack method for the player
        /// </summary>
        /// <param name="player"></param>
        /// <param name="enemies">list of enemies being attacked</param>
        /// <param name="animation"></param>
        /// <param name="gameTime"></param>
        public void Attack(Player player, List<Enemies> enemies, List<Texture2D> animation, GameTime gameTime)
        {
            Enemies tempEnemy;
            if (attacking)
            {
                atkTimer += gameTime.ElapsedGameTime.TotalSeconds;
                if(enemies != null)
                {
                    //for each enemy deal damage and then remove it from the list of enemies to be damaged
                    for(int i = 0; i < enemies.Count; i++)
                    {
                        if(enemies[i] is Boss temp)
                        {
                            temp.Stunned = true;
                        }
                        tempEnemy = enemies[i];
                        tempEnemy.Health -= player.damage;
                        enemies[i] = tempEnemy;
                        enemies.RemoveAt(i);
                        i--;
                    }
                }
                //go through the attack animation
                if(atkFrame == 0)
                {
                    currentAtkSprite = animation[0];
                }
                if(atkTimer > atkTimePerFrame)
                {
                    atkFrame++;
                    currentAtkSprite = animation[atkFrame];
                    atkTimer -= atkTimePerFrame;
                }
                if (atkFrame == 12)
                {
                    leftAttack = false;
                    rightAttack = false;
                    attacking = false;
                    atkFrame = 0;
                    
                }
            }
        }

        /// <summary>
        /// move method for the player
        /// movement is based on WASD
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Move(GameTime gameTime)
        {
            prevPos = Position;
            Rectangle temp = Position;
            kbstate = Keyboard.GetState();
            int moveIncrease = 0;
            
            
            //if the player has SpeedArmor then their movement speed is increased
            if (armor is SpeedArmor tempArmor)
            {
                tempArmor = (SpeedArmor)armor;
                moveIncrease = tempArmor.SpeedBoost;
            }
            //move up and down based on wether or not w or d is pressed
            if (kbstate.IsKeyDown(Keys.W))
            {
                currentSprite = walkUp;
                walkLeft = false;
                walkRight = false;
                temp.Y -= moveSpeed + moveIncrease;
            }
            if (kbstate.IsKeyDown(Keys.S))
            {
                currentSprite = walkDown;
                walkRight = false;
                walkLeft = false;
                temp.Y += moveSpeed + moveIncrease;
            }

            //in addition if moving left or right
            if (kbstate.IsKeyDown(Keys.A))
            {
                if (!previous.IsKeyDown(Keys.A))//if this is the first frame of moving left start at the begning of the walk animation
                {
                    walkTimer = 0;
                }
                walkTimer += gameTime.ElapsedGameTime.TotalSeconds;
                if (!kbstate.IsKeyDown(Keys.D)) //loop through walk animation if not pressing D aswell
                {
                    walkRight = false;
                    currentSprite = sidewaysWalk;
                    if (walkFrame >= 9)
                    {
                        walkFrame = 0;
                    }
                    if (walkTimer > walkTimePerFrame)
                    {
                        walkFrame++;
                        walkTimer -= walkTimePerFrame;
                    }
                }
                //set correct walking sprite if moving vertically in additon
                else if (kbstate.IsKeyDown(Keys.S))
                {
                    currentSprite = walkDown;
                }
                else
                {
                    currentSprite = walkUp;
                }

                temp.X -= moveSpeed + moveIncrease;
                walkLeft = true;


            }
            if (kbstate.IsKeyDown(Keys.D))
            {
                if (!previous.IsKeyDown(Keys.D)) //if this is the first frame of them moving right then start the animation at the first frame
                {
                    walkTimer = 0;
                }
                walkTimer += gameTime.ElapsedGameTime.TotalSeconds;
                if (!kbstate.IsKeyDown(Keys.A)) //if not pressing A then loop through the walk animation
                {
                    walkLeft = false;
                    currentSprite = sidewaysWalk;
                    if (walkFrame >= 9)
                    {
                        walkFrame = 0;
                    }
                    if(walkTimer > walkTimePerFrame)
                    {
                        walkTimer -= walkTimePerFrame;
                        walkFrame++;
                    }
                }
                //set correct walking sprite if moving vertically in additon
                else if (kbstate.IsKeyDown(Keys.S))
                {
                    currentSprite = walkDown;
                }
                else
                {
                    currentSprite = walkUp;
                }
                temp.X += moveSpeed + moveIncrease;

                walkRight = true;


            }
            else if(!kbstate.IsKeyDown(Keys.D) && !kbstate.IsKeyDown(Keys.S) && !kbstate.IsKeyDown(Keys.A) && !kbstate.IsKeyDown(Keys.W))//if no move input then use walkdown sprite
            {
                walkLeft = false;
                walkRight = false;
                currentSprite = walkDown;
            }
            Position = temp;
            previous = kbstate;
        }

        /// <summary>
        /// method for swapping the currently equipped piece of armor 
        /// </summary>
        /// <param name="newArmor"></param>
        public void ArmorSwap(Armor newArmor)
        {
            if(newArmor is SpeedArmor)
            {
                armor = (SpeedArmor)newArmor;
            }
            if(newArmor is ShieldArmor)
            {
                armor = (ShieldArmor)newArmor;
            }
            if(newArmor is ThornArmor)
            {
                armor = (ThornArmor)newArmor;
            }
            else
            {
                armor = newArmor;
            }
        }
        
        /// <summary>
        /// method for swapping the currently equipped weapon
        /// </summary>
        /// <param name="newWeapon"></param>
        public void WeaponSwap(Weapon newWeapon)
        {
            if(newWeapon is FireWeapon)
            {
                weapon = (FireWeapon)newWeapon;
            }
            if(newWeapon is ShockWeapon)
            {
                weapon = (ShockWeapon)newWeapon;
            }
            if (newWeapon is FrostWeapon)
            {
                weapon = (FrostWeapon)newWeapon;
            }
            else
            {
                weapon = newWeapon;
            }
        }

        /// <summary>
        /// give the player health if they use a health potion
        /// </summary>
        /// <param name="hpPot"></param>
        public void UseHealthPotion(HealthPotion hpPot)
        {
            
            if (health < 15)//if under 15 health then give them health and remove the potion from their inventory
            {
                health += hpPot.RestoreAmnt;
                inventory.Remove(hpPot.Name);
                invList.Remove(hpPot.Name);
                healed = true;
            }
            if(health > 15) //if they heal over 15 health then reset them to 15
            {
                health = 15;
            }
        }

        /// <summary>
        /// when hit flash the player sprite and give the player invulnerability for a short time
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        public bool HitEffect(GameTime gameTime)
        {
            if (hit)
            {
                hitTimer += gameTime.ElapsedGameTime.TotalSeconds;
                if(hitTimer >= hitDuration && alternate < 10)
                {
                    alternate++;
                    hitTimer -= hitDuration;
                }
                
                if(alternate >= 10)
                {
                    alternate = 0;
                    hit = false;
                }
                if(alternate % 2 == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
