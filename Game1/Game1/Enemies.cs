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
    class Enemies : Characters
    {
        //fields
        private Random rng;
        private int initialX;
        private int initialY;
        private int direction;
        private bool hit;
        private Rectangle prevPos;
        
        //properties
        public bool Hit
        {
            get { return hit; }
            set { hit = value; }
        }
        public Rectangle PrevPos
        {
            get { return prevPos; }
            set { prevPos = value; }
        }

        //constructor
        public Enemies(Random rng, int health, int damage, Rectangle position, Texture2D texture) : base(health, damage, position, texture)
        {
            hit = false;
            this.rng = rng;
            initialX = Position.X;
            initialY = Position.Y;

            direction = rng.Next(0, 4);
        }

        public override void Move(Characters player)
        {
            prevPos = Position;
            int xDist = player.Position.X - Position.X;
            int yDist = player.Position.Y - Position.Y;
            Double tDist = DistanceTo(player.Position.X, player.Position.Y, Position.X, Position.Y);

            double ratio = 3 / tDist;

            int xMov = (int)(ratio * xDist);
            int yMov = (int)(ratio * yDist);

            
            Rectangle temp = Position;
            temp.X += xMov;
            temp.Y += yMov;
            Position = temp;

        }

        public override void TakeDamage(Characters damaged, Characters damager)
        {
            if (damager.Position.Intersects(damaged.Position))
            {
                damaged.Health -= damage;
            }
        }

        /// <summary>
        /// method that calculates the distance from two points using pythag
        /// </summary>
        /// <param name="coord1"></param>
        /// <param name="coord2"></param>
        /// <returns></returns>
        protected Double DistanceTo(int coord1X, int coord1Y, int coord2X, int coord2Y)
        {
            int xDist = coord1X - coord2X;
            int yDist = coord1Y - coord2Y;
            return Math.Pow(Math.Pow(xDist, 2) + Math.Pow(yDist, 2), .5);
        }
    }
}

