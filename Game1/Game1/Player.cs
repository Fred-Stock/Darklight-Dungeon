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
        private int atkTimer;
        private int walkTimer;
        private bool attacking;
        private bool attacking2;
        private bool walkRight;
        private bool walkLeft;
        private bool hit;
        private Texture2D sidewaysWalk;
        private Texture2D walkUp;
        private Texture2D walkDown;
        KeyboardState previous;
        bool leftAttack; //field for keeping track of what direction the player is attacking
        bool rightAttack; //field for keeping track of what direction the player is attacking


        //properties
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
        public bool Attacking2
        {
            get { return attacking2; }
            set { attacking2 = value; }
        }
        public int WalkTimer
        {
            get { return walkTimer; }
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
        
        //constructor
        public Player(int score, Weapon weapon, Armor armor, Texture2D sidewaysWalk, Texture2D walkUp, Texture2D walkDown, int health, int damage, Rectangle position, Texture2D texture ) : base(health, damage, position, texture)
        {
            moveSpeed = 3;
            this.score = score;
            this.weapon = weapon;
            this.armor = armor;

            if(weapon == null)
            {
                this.damage = damage;
            }
            else
            {
                this.damage = weapon.Damage;
                inventory.Add(weapon.Name, weapon);
                invList.Add(weapon.Name);
            }

            if(armor != null)
            {
                
                inventory.Add(armor.Name, armor);
                invList.Add(armor.Name);
            }
            inventory = new Dictionary<string, Item>();
            invList = new List<string>();

            atkTimer = 0;
            attacking = false;
            prevPos = position;
            currentSprite = texture;

            this.sidewaysWalk = sidewaysWalk;
            this.walkDown = walkDown;
            this.walkUp = walkUp;
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
            //temp solution but if there is a collision then add a 1 to the end of the name and keep doing that until no collision
            else
            {
                item.Name = item.Name + "1";
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
            //temp solution but if there is a collision then add a 1 to the end of the name and keep doing that until no collision
            else
            {
                item.Name = item.Name + "1";
                PickUpItem(item);
            }
        }
        

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

        public void Attack(Player player, List<Enemies> enemys, List<Texture2D> animation)
        {
            Enemies tempEnemy;
            if (attacking)
            {
                atkTimer++;
                if(enemys != null)
                {
                    for(int i = 0; i < enemys.Count; i++)
                    {
                        tempEnemy = enemys[i];
                        tempEnemy.Health -= player.damage;
                        enemys[i] = tempEnemy;
                        enemys.RemoveAt(i);
                        i--;
                    }
                }
                if(atkTimer < 13)
                {
                    currentAtkSprite = animation[atkTimer];
                }
                if (atkTimer > 13)
                {
                    leftAttack = false;
                    rightAttack = false;
                    attacking = false;
                    atkTimer = 0;
                }
            }
        }

        public override void Move()
        {
            prevPos = Position;
            Rectangle temp = Position;
            kbstate = Keyboard.GetState();
            int moveIncrease = 0;

            if (armor is SpeedArmor tempArmor)
            {
                tempArmor = (SpeedArmor)armor;
                moveIncrease = tempArmor.SpeedBoost;
            }
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
            if (kbstate.IsKeyDown(Keys.A))
            {
                if (!kbstate.IsKeyDown(Keys.D))
                {
                    walkRight = false;
                    walkTimer++;
                    currentSprite = sidewaysWalk;
                }
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

                if(walkTimer >= 30)
                {
                    walkTimer = 0;   
                }

            }
            if (kbstate.IsKeyDown(Keys.D))
            {

                if (!kbstate.IsKeyDown(Keys.A))
                {
                    walkLeft = false;
                    walkTimer++;
                    currentSprite = sidewaysWalk;
                }
                else if(kbstate.IsKeyDown(Keys.S))
                {
                    currentSprite = walkDown;
                }
                else
                {
                    currentSprite = walkUp;
                }
                temp.X += moveSpeed + moveIncrease;

                walkRight = true;

                if (walkTimer >= 30)
                {
                    walkTimer = 0;
                }
            }
            else if(!kbstate.IsKeyDown(Keys.D) && !kbstate.IsKeyDown(Keys.S) && !kbstate.IsKeyDown(Keys.A) && !kbstate.IsKeyDown(Keys.W))
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

        public void UseHealthPotion(HealthPotion hpPot)
        {
            health += hpPot.RestoreAmnt;
            inventory.Remove(hpPot.Name);
            invList.Remove(hpPot.Name);
        }
        
    }
}
