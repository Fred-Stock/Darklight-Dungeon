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

        //constructor
        public Player(int score, Weapon weapon, Armor armor, int health, int damage, Rectangle position, Texture2D texture ) : base(health, damage, position, texture)
        {
            this.score = score;
            this.weapon = weapon;

            this.armor = armor;
            inventory = new Dictionary<string, Item>();
            invList = new List<string>();
        }

        public void PickUpItem(Item item)
        {
            inventory.Add(item.Name, item);
            invList.Add(item.Name);
            item.Visible = false;
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

    }
}
