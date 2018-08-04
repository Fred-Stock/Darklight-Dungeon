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

            Rectangle leftX = new Rectangle(character.Position.X - 3, character.Position.Y, 3, character.Position.Height);
            Rectangle rightX = new Rectangle(character.Position.X + character.Position.Width + 1, character.Position.Y, 3, character.Position.Height);
            Rectangle topY = new Rectangle(character.Position.X, character.Position.Y - 3, character.Position.Width, 3);
            Rectangle bottomY = new Rectangle(character.Position.X, character.Position.Y + character.Position.Height + 1, character.Position.Width, 3);
            

            // X Check
            if (leftX.Intersects(Position) || rightX.Intersects(Position))
            {
                Rectangle temp = character.Position;
                temp.X = character.PrevPos.X;
                character.Position = temp;
            }

            // Y check
            if (topY.Intersects(Position) || bottomY.Intersects(Position))
            {
                Rectangle temp = character.Position;
                temp.Y = character.PrevPos.Y;
                character.Position = temp;
            }

        }
    }
}
