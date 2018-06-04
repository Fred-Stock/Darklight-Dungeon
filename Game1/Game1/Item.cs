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
        private Texture2D texture;
        private Rectangle position;

        //properties
        public Texture2D Texture
        {
            get { return texture; }
        }

        public Rectangle Position
        {
            get { return position; }
            set { position = value; }
        }


        //constructor 
        public Item(Rectangle position, Texture2D texture) : base(position, texture)
        {

        }
    }
}
