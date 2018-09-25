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
    class Armor : Item
    {
        //fields
        protected int defense;


        //properties
        public int Defense
        {
            get { return defense; }
            set { defense = value; }
        }

        //constructor
        public Armor(string name, Rectangle position, Texture2D texture) : base(name, position, texture)
        {
            defense = 1;
            cost = 8;
        }

        //methods
        //method overriden by child classes
        public virtual void ArmorAction()
        {

        }

        //overload for AmorAction() also overriden by child classes
        public virtual void ArmorAction(Enemies enemy)
        {

        }
    }
}
