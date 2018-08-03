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


        //constructor
        public Characters(int health, int damage, Rectangle position, Texture2D texture) : base(position, texture)
        {
            this.damage = damage;
            this.health = health;
            
        }

        //methods
        public virtual void TakeDamage(Characters damaged, Characters damager)
        {
            damaged.Health -= damager.Damage;
        }

        public virtual void Move(Characters character)
        {
            Rectangle temp = character.Position;
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
            character.Position = temp;
        }

        /// <summary>
        /// method for determining knockback
        /// </summary>
        /// <param name="attacker">character that damaged the character getting knocked backwards</param>
        public virtual void Knockback(Characters attacker)
        {
            
            Rectangle temp = Position;
            
            if(attacker.Position.X < Position.X) //attack from the left
            {
                temp.X += 100;
            }
            if(attacker.Position.X > Position.X) //attack from the right
            {
                temp.X -= 100;
            }
            if(attacker.Position.Y < Position.Y) //attack from above
            {
                temp.Y += 100;
            }
            if(attacker.Position.Y > Position.Y) //attack from below
            {
                temp.Y -= 100;
            }
            Position = temp;
            timer++;
        }
    }

}
