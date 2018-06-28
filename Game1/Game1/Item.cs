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
    class Item : GameObject 
    {
        //fields
        private string name;
        private bool visible;

        //properties
        public string Name
        {
            get { return name; }
        }

        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }


        //constructor 
        public Item(string name, Rectangle position, Texture2D texture) : base(position, texture)
        {
            this.name = name;
            visible = true;
        }
    }
}
