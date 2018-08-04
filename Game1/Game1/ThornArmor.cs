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
        public ThornArmor(ArmorType armor, string name, Rectangle position, Texture2D texture) : base(armor, name, position, texture)
        {
            defense = 1;
        }

        //methods
        public override void ArmorAction(Enemies enemy)
        {
            enemy.Health -= 3;
        }
    }
}
