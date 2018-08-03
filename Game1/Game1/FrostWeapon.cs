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
    class FrostWeapon : Weapon
    {
        //fields
        int timer;
        private bool stopped;

        //properties


        //constructor
        public FrostWeapon(WeaponType weapon, string name, Rectangle position, Texture2D texture) : base(weapon, name, position, texture)
        {
            timer = 0;
            affectDuration = 60;
        }

        int prevMoveSpeed = 0;
        //method
        public override void WeaponAction(Enemies attacked)
        {
            if (timer == 0)
            {
                prevMoveSpeed = attacked.MoveSpeed;
                attacked.MoveSpeed = (int)Math.Ceiling((double)attacked.MoveSpeed / 2);
            }

            if(timer < affectDuration && attacked.Affected)
            {
                
                timer++;
            }
            else
            {
                timer = 0;
                attacked.Affected = false;
                attacked.MoveSpeed = prevMoveSpeed;
            }
        }
    }
}
