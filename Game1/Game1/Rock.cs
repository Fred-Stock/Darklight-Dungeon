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
    class Rock : Obstacle
    {
        //fields

        //properties

        //constructor
        public Rock(Rectangle position, Texture2D texture) : base(position, texture)
        {

        }

        //methods
        public override void Collision(Characters character, Rectangle prevPos, Game1 game)
        {
            //if the character is a fly type enemy then they can fly over rocks so do not call the base collision class
            if(!(character is Fly))
            {
                base.Collision(character, prevPos, game);
            }
        }
    }
}
