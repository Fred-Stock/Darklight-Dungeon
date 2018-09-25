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
    class SpeedArmor : Armor
    {
        //fields
        private int speedBoost;

        //properties
        public int SpeedBoost
        {
            get { return speedBoost; }
            set { speedBoost = value; }
        }


        //constructor
        public SpeedArmor( string name, Rectangle position, Texture2D texture) : base( name, position, texture)
        {
            speedBoost = 2;
            defense = 2;
            cost = 25;
        }

        //methods
    }
}
