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
    class ShieldArmor :Armor
    {
        //fields
        private bool popped;
        private int hitNum;

        //properties
        public bool Popped
        {
            get { return popped; }
        }
        

        //constructor
        public ShieldArmor(ArmorType armor, string name, Rectangle position, Texture2D texture) : base(armor, name, position, texture)
        {
            popped = false;
            defense = 0;
            cost = 20;
        }

        //methods
        public override void ArmorAction()
        {
            if(hitNum < 3)
            {
                hitNum++;
            }
            else
            {
                popped = true;
            }
        }
    }
}
