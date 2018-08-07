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
    class HealthPotion : Item
    {
        //fields
        private int restoreAmnt;

        //properties
        public int RestoreAmnt
        {
            get { return restoreAmnt; }
        }


        //constructor
        public HealthPotion(int restoreAmnt, string name, Rectangle position, Texture2D texture) : base (name, position, texture)
        {
            cost = 3;
            this.restoreAmnt = restoreAmnt;
        }

        //method
        
    }
}
