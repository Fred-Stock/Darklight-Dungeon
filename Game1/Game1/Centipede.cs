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
    class Centipede : Enemies
    {

        //fields
        private bool moveUp;
        private bool moveDown;
        private bool moveLeft;
        private bool moveRight;
        private int timer;

        //properties

        //constructor
        public Centipede(Player player, Random rng, int health, int damage, Rectangle position, Texture2D texture) : base(rng, health, damage, position, texture)
        {
            timer = 0;
            SelectDirection(player);
        }

        //methods
        public override void Move(Characters player)
        {
            Rectangle temp = Position;
            prevPos = Position;
            timer++;
            if (moveUp)
            {
                temp.Y -= 2;
            }
            if (moveDown)
            {
                temp.Y += 2;
            }
            if (moveLeft)
            {
                temp.X -= 2;
            }
            if (moveRight)
            {
                temp.X += 2;
            }
            if(timer >= 50)
            {
                SelectDirection(player);
                timer = 0;
            }
            
            Position = temp;
        }

        /// <summary>
        /// method that selects what direction the enemy should move in based on the distance from the player
        /// if the x distance is greater than the y distance than it will move it will set the corresponding boolean to true
        /// if the y distance is gerater than the x distance then it will set the corresponding boolean to true
        /// </summary>
        /// <param name="player"></param>
        public void SelectDirection(Characters player)
        {

            //set all movement related booleans to true
            moveDown = false;
            moveUp = false;
            moveLeft = false;
            moveRight = false;

            //get absolute value of distance in x and y direction
            int xDist = Math.Abs(Position.X - player.Position.X);
            int yDist = Math.Abs(Position.Y - player.Position.Y);

            //set the correct boolean to true
            if(xDist > yDist)
            {
                if((Position.X - player.Position.X) < 0)
                {
                    moveRight = true;
                }
                else
                {
                    moveLeft = true;
                }
            }
            else
            {
                if((Position.Y - player.Position.Y) < 0)
                {
                    moveDown = true;
                }
                else
                {
                    moveUp = true;
                }
            }
        }
    }
}
