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
    class Obstacle : GameObject
    {
        //constructor
        public Obstacle(Rectangle position, Texture2D texture) : base(position, texture)
        {

        }
    }
}
