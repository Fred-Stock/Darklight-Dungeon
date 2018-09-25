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
    class ThornArmor : Armor
    {
        //fields

        //properties

        //constructor
        public ThornArmor(string name, Rectangle position, Texture2D texture) : base(name, position, texture)
        {
            defense = 1;
            cost = 15;
        }

        //methods
        /// <summary>
        /// when the enemy runs into the player it takes damage
        /// </summary>
        /// <param name="enemy"></param>
        public override void ArmorAction(Enemies enemy)
        {
            enemy.Health -= 3;
        }
    }
}
