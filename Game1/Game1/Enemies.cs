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

        public override void Move(Characters character)
        {


            Rectangle temp = Position;

            if (direction == 0)
            {
                temp.Y -= 2;
                if(temp.Y < initialY - 100)
                {
                    direction = rng.Next(0, 4);
                }

            }
            if (direction == 1)
            {
               
                temp.X += 2;
                if (temp.X > initialX + 100)
                {
                    direction = rng.Next(0, 4);
                }

            }
            if (direction == 2)
            {
                temp.Y += 2;
                if (temp.Y > initialY + 100)
                {
                    direction = rng.Next(0, 4);
                }
            }
            if (direction == 3)
            {
                
                temp.X -= 2;
                if (temp.X < initialX - 100)
                {
                    direction = rng.Next(0, 4);
                }
            }

            
            Position = temp;
        }
        public override void TakeDamage(Characters damaged, Characters damager)
        {
            if (damager.Position.Intersects(damaged.Position))
            {
                damaged.Health -= damage;
            }
        }
    }
}
