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

        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        public int Damage
        {
            get { return damage; }
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


    }
}
