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
                
        }

        //methods
        public virtual void TakeDamage(Characters damaged, Characters damager)
        {
            damaged.Health -= damager.Damage;
        }
    }
}
