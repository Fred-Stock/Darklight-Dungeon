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
    enum WeaponType
    {
        test = 4,
    }

    class Weapon : Item
    {
        //fields
        WeaponType type;
        
        public WeaponType Type
        {
            get { return type; }
        }


        public Weapon(WeaponType weapon, Rectangle position, Texture2D texture) : base(position, texture)
        {
            this.type = weapon;
        }

        //methods
        public void WeaponAction()
        {

        }
    }
}
