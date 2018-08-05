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
        basic,
        plus1,
        shock,
        frost,
        fire,
    }

    class Weapon : Item
    {
        //fields
        private WeaponType type;
        protected int affectDuration;
        protected int damage;

        //properties
        public WeaponType Type
        {
            get { return type; }
        }
        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }

        //constructor
        public Weapon(WeaponType weapon, string name, Rectangle position, Texture2D texture) : base(name, position, texture)
        {
            type = weapon;
            damage = 50;
            cost = 8;
        }

        //methods
        public virtual void WeaponAction(Enemies attacked)
        {

        }
    }
}
