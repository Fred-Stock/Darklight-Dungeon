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
        }


        //constructor
        public SpeedArmor(ArmorType armor, string name, Rectangle position, Texture2D texture) : base(armor, name, position, texture)
        {
            speedBoost = 2;
            defense = 2;
        }

        //methods
    }
}
