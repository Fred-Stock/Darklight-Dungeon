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
    class Characters : GameObject
    {
        //fields
        protected int health;

        protected int damage;

        protected KeyboardState kbstate;

        protected bool invulnerable;

        private int timer;

        protected Rectangle prevPos;

        protected int moveSpeed;

        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        public int Damage
        {
            get { return damage; }
        }

        public bool Invulnerable
        {
            get { return invulnerable; }
            set { invulnerable = value; }
        }

        public Rectangle PrevPos
        {
            get { return prevPos; }
            set { prevPos = value; }
        }

        public int MoveSpeed
        {
            get { return moveSpeed; }
            set { moveSpeed = value; }
        }



        //constructor
        public Characters(int health, int damage, Rectangle position, Texture2D texture) : base(position, texture)
        {
            this.damage = damage;
            this.health = health;
            moveSpeed = 3;
        }

        //methods
        public virtual void TakeDamage(Player damaged, Characters damager)
        {
            damaged.Health -= damager.Damage;
        }

        public virtual void Move(Characters character)
        {
            Rectangle temp = Position;
            kbstate = Keyboard.GetState();
            if (kbstate.IsKeyDown(Keys.W))
            {
                temp.Y -= moveSpeed;
            }
            if (kbstate.IsKeyDown(Keys.S))
            {
                temp.Y += moveSpeed;
            }
            if (kbstate.IsKeyDown(Keys.A))
            {
                temp.X -= moveSpeed;
            }
            if (kbstate.IsKeyDown(Keys.D))
            {
                temp.X += moveSpeed;
            }
            Position = temp;
        }

        public virtual void Move()
        {
            Rectangle temp = Position;
            kbstate = Keyboard.GetState();
            if (kbstate.IsKeyDown(Keys.W))
            {
                temp.Y -= 3;
            }
            if (kbstate.IsKeyDown(Keys.S))
            {
                temp.Y += 3;
            }
            if (kbstate.IsKeyDown(Keys.A))
            {
                temp.X -= 3;
            }
            if (kbstate.IsKeyDown(Keys.D))
            {
                temp.X += 3;
            }
            Position = temp;
        }
        public virtual void Move(GameTime gameTime)
        {

        }

        /// <summary>
        /// method for determining knockback
        /// </summary>
        /// <param name="attacker">character that damaged the character getting knocked backwards</param>
        public virtual void Knockback(Characters attacker)
        {
            PrevPos = Position;
            Rectangle temp = Position;

            int xDist = attacker.Position.X - Position.X;
            int yDist = attacker.Position.Y - Position.Y;
            Double tDist = DistanceTo(attacker.Position.X, attacker.Position.Y, Position.X, Position.Y);

            double xRatio = xDist / tDist;
            double yRatio = yDist / tDist;

         

            if (attacker.Position.X < Position.X) //attack from the left
            {
                temp.X -= (int)(50 * xRatio);
            }
            if(attacker.Position.X > Position.X) //attack from the right
            {
                temp.X -= (int)(50 * xRatio);
            }
            if(attacker.Position.Y < Position.Y) //attack from above
            {
                temp.Y -= (int)(50 * yRatio);
            }
            if(attacker.Position.Y > Position.Y) //attack from below
            {
                temp.Y -= (int)(50 * yRatio);
            }
            Position = temp;
            timer++;
        }
    }

}
