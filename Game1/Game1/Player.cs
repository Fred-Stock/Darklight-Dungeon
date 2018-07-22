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
        private int timer;
        private bool attacking;
        private bool attacking2;
        private int moveSpeed;

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
        public int MoveSpeed
        {
            get { return moveSpeed; }
            set { moveSpeed = value; }
        }



        //constructor
        public Player(int score, Weapon weapon, Armor armor, int health, int damage, Rectangle position, Texture2D texture ) : base(health, damage, position, texture)
        {
            this.score = score;
            this.weapon = weapon;
            currentSprite = texture;
            this.armor = armor;
            inventory = new Dictionary<string, Item>();
            invList = new List<string>();
            timer = 0;
            attacking = false;
            moveSpeed = 3;
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
                currency += 10;
            }
            else if (item.Name == "smallCoin")
            {
                currency += 5;
            }
            item.Visible = false;
        }

        public void Attack(Player player, List<Enemies> enemys, List<Texture2D> animation)
        {
            if (attacking)
            {
                timer++;
                if(enemys != null)
                {
                    for(int i = 0; i < enemys.Count; i++)
                    {
                        enemys[i].Health -= player.damage;
                    }
                }
                if(timer < 13)
                {
                    currentAtkSprite = animation[timer];
                }
                if (timer > 13)
                {
                    attacking = false;
                    timer = 0;
                }
            }
        }

        public override void Move(Characters character)
        {
            Rectangle temp = character.Position;
            kbstate = Keyboard.GetState();
            if (kbstate.IsKeyDown(Keys.W))
            {
                temp.Y -= moveSpeed;
            }
            if (kbstate.IsKeyDown(Keys.S))
            {
                temp.Y += moveSpeed;
            }
            if (kbstate.IsKeyDown(Keys.A))
            {
                temp.X -= moveSpeed;
            }
            if (kbstate.IsKeyDown(Keys.D))
            {
                temp.X += moveSpeed;
            }
            character.Position = temp;
        }
    }
}
