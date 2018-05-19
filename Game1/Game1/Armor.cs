using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

enum ArmorType
{
    test,
}

namespace Game1
{
    class Armor : Item
    {
        ArmorType armor;

        public Armor(ArmorType armor, Rectangle position, Texture2D texture) : base(position, texture)
        {
            this.armor = armor;
        }

        //methods
        public void ArmorAction()
        {

        }
    }
}
