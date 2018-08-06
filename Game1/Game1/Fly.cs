using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1 
{
    class Fly : Enemies
    {
        //fields
        private int timer;


        //properties

        //constructor
        public Fly(Random rng, int health, int damage, Rectangle position, Texture2D texture) :
            base(rng, health, damage, position, texture)
        {   
            affected = false;
            moveSpeed = 3;
        }

        //methods
        /// <summary>
        /// the fly enemy will fly towards the player
        /// </summary>
        /// <param name="player"></param>
        public override void Move(Characters player)
        {
            PrevPos = Position;

            //every frame the character should move a certian distance towards the player
            //to keep that distance consistent a ratio needs to be used since the character is moving at a diagnol
            int xDist = player.Position.X - Position.X;
            int yDist = player.Position.Y - Position.Y;
            Double tDist = DistanceTo(player.Position.X, player.Position.Y, Position.X, Position.Y);
            
            double ratio = moveSpeed / tDist;

            int xMov = (int)(ratio * xDist);
            int yMov = (int)(ratio * yDist);
            if (xMov != 0)
            {
                yMov += (int)(3 * Math.Sin((timer * Math.PI) / 32));
            }

            Rectangle temp = Position;
            temp.X += xMov;
            temp.Y += yMov;
            Position = temp;
            if(prevPos2.X == Position.X && xMov != 0)
            {
                
                temp.Y += moveSpeed;
                
                Position = temp;
            }
            if(prevPos2.Y == Position.Y && (yMov != 0))
            {
                
                temp.X -= moveSpeed;

                Position = temp;
            }
            prevPos2 = Position;
            timer++;
        }
    }
}
