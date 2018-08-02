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
        //fields
        
        //constructor
        public Obstacle(Rectangle position, Texture2D texture) : base(position, texture)
        {

        }

        //methods
        public virtual void Collision(Characters character, Rectangle prevPos, Game1 game)
        {
            if (character.Position.Intersects(Position))
            {
                character.Position = prevPos;
            }
        }
    }
}
