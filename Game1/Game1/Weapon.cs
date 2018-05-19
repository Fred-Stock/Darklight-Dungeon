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
        test,
    }

    class Weapon : Item
    {
        //fields
        WeaponType weapon;

        public Weapon(WeaponType weapon, Rectangle position, Texture2D texture) : base(position, texture)
        {
            this.weapon = weapon;
        }

        //methods
        public void WeaponAction()
        {

        }
    }
}
