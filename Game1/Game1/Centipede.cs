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
        private int aniTimer;
        private bool prevUp;
        private bool prevDown;
        private bool prevLeft;
        private bool prevRight;
        private Texture2D rotTexture;

        //properties
        public bool MoveUp
        {
            get { return moveUp; }
        }
        public bool MoveDown
        {
            get { return moveDown; }
        }
        public bool MoveLeft
        {
            get { return moveLeft; }
        }
        public bool MoveRight
        {
            get { return moveRight; }
        }
        public int AniTimer
        {
            get { return aniTimer; }
        }
        public Texture2D RotTexture
        {
            get { return rotTexture; }
        }


        //constructor
        public Centipede(Player player, Random rng, int health, int damage, Rectangle position, Texture2D texture, Texture2D rotTexture) : base(rng, health, damage, position, texture)
        {
            affected = false;
            moveSpeed = 2;
            timer = 0;
            aniTimer = 0;
            SelectDirection(player);
            prevLeft = true;
            this.rotTexture = rotTexture;
        }

        //methods
        public override void Move(Characters player)
        {
            Rectangle temp = Position;
            prevPos = Position;
            timer++;
            aniTimer++;
            if (moveUp)
            {
                //if (timer == 0)
                //{
                //    temp.X += temp.Width / 2;
                //    temp.Y += temp.Height / 2;
                //}
                temp.Y -= moveSpeed;
            }
            if (moveDown)
            {
                //if (timer == 0)
                //{
                //    temp.X += temp.Width / 2;
                //    temp.Y += temp.Height / 2;
                //
                //}
                temp.Y += moveSpeed;
            }
            if (moveLeft)
            {
                temp.X -= moveSpeed;
            }
            if (moveRight)
            {
                temp.X += moveSpeed;
            }
            if(timer >= 100)
            {
                SelectDirection(player);
                temp = Position;
                timer = 0;
            }
            if(aniTimer >= 20)
            {
                aniTimer = 0;
            }

            Position = temp;
            if (prevPos2.X == Position.X && (moveLeft || moveRight))
            {

                temp.Y += moveSpeed;

                Position = temp;
            }
            if (prevPos2.Y == Position.Y && (moveUp || moveDown))
            {

                temp.X -= moveSpeed;

                Position = temp;
            }
            prevPos2 = Position;
        }

        /// <summary>
        /// method that selects what direction the enemy should move in based on the distance from the player
        /// if the x distance is greater than the y distance than it will move it will set the corresponding boolean to true
        /// if the y distance is gerater than the x distance then it will set the corresponding boolean to true
        /// </summary>
        /// <param name="player"></param>
        public void SelectDirection(Characters player)
        {
            //booleans to keep track of the previous direction chosen to help with the rotations of the hitbox
            prevDown = MoveDown;
            prevUp = moveUp;
            prevLeft = moveLeft;
            prevRight = moveRight;

            //set all movement related booleans to true
            moveDown = false;
            moveUp = false;
            moveLeft = false;
            moveRight = false;

            //get absolute value of distance in x and y direction
            int xDist = Math.Abs(Position.X - player.Position.X);
            int yDist = Math.Abs(Position.Y - player.Position.Y);

            //set the correct boolean to true
            //in addition the sprite is a rectangle so the hitbox needs to be changed correspondingly
            Rectangle temp = Position;
            if(xDist > yDist)
            {
                
                if (!prevRight && !prevLeft)//if the rectangle is vertical
                {

                    temp.Width = 200;
                    temp.Height = 60;
                }

                if((Position.X - player.Position.X) < 0)
                {
                    moveRight = true;
                    if (prevDown)
                    {
                        temp.X = Position.X - 200;
                        temp.Y = Position.Y + Position.Height;
                    }
                }
                else
                {
                    moveLeft = true;
                    if (prevDown)
                    {
                        temp.X = Position.X + Position.Width;
                        temp.Y = Position.Y + Position.Height;
                    }
                }
            }
            else
            {               
                if (!prevUp && !prevDown)
                {
                    //temp.X = Position.X + 70;
                    //temp.Y = Position.Y - 70;
                    temp.Width = 53;
                    temp.Height = 200;
                }

                if ((Position.Y - player.Position.Y) < 0)
                {
                    moveDown = true;
                }
                else
                {
                    moveUp = true;
                }
            }
            Position = temp;

            
        }
    }
}
