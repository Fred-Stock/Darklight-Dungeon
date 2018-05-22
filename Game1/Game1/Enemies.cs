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
        //constructor
        public Enemies(int health, int damage, Rectangle position, Texture2D texture) : base(health, damage, position, texture)
        {

        }

        public override void Move(Characters character)
        {
            
        }
    }
}
