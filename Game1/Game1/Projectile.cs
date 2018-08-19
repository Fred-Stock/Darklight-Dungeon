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
    enum Direction
    {
        left,
        right,
        up,
        down,
    }

    class Projectile : Characters
    {
        //fields
        private Direction direction;
        private double duration;
        private double timer;

        //properties
        public Direction Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        //constructor
        public Projectile(int health, int damage, Rectangle position, Texture2D texture) : base(health, damage, position, texture)
        {
            duration = 2.0;
        }

        //methods
        public override void TakeDamage(Player damaged, Characters damager)
        {
            if (Position.Intersects(damaged.Position))
            {
                damaged.Health -= damage;
                Health = 0;

            }
        }

        public override void Move(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.TotalSeconds;

            if(timer >= duration)
            {
                timer -= duration;
                health = 0;
            }

            Rectangle temp = Position;
            if(direction == Direction.left)
            {
                temp.X -= 6;
            }
            if(direction == Direction.right)
            {
                temp.X += 6;
            }
            if(direction == Direction.up)
            {
                temp.Y -= 6;
            }
            if(direction == Direction.down)
            {
                temp.Y += 6;
            }
            Position = temp;
        }
    }
}
