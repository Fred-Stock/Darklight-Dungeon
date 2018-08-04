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
        //fields
        ArmorType armor;
        protected int defense;

        //properties
        public int Defense
        {
            get { return defense; }
            set { defense = value; }
        }

        //constructor
        public Armor(ArmorType armor, string name, Rectangle position, Texture2D texture) : base(name, position, texture)
        {
            this.armor = armor;
            defense = 1;
        }

        //methods
        public virtual void ArmorAction()
        {

        }

        public virtual void ArmorAction(Enemies enemy)
        {

        }
    }
}
