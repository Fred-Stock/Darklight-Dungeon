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

        //constructor
        public Player(int score, Weapon weapon, Armor armor, int health, int damage, Rectangle position, Texture2D texture ) : base(health, damage, position, texture)
        {
            this.score = score;
            this.weapon = weapon;
            this.armor = armor;
        }

    }
}
