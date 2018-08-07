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
            affectDuration = 120;
            damage = 10;
            cost = 15;
        }

        int prevMoveSpeed = 0;
        //method
        public override void WeaponAction(Enemies attacked)
        {
            if (attacked.HitDuration < affectDuration && attacked.Affected)
            {
                if(attacked.HitDuration == 1)
                {
                    prevMoveSpeed = attacked.MoveSpeed;
                    attacked.MoveSpeed = (int)Math.Ceiling((double)attacked.MoveSpeed / 2);

                }

            }

            if (attacked.Affected)
            {
                attacked.HitDuration++;
            }
            if (attacked.HitDuration > affectDuration)
            {
                attacked.HitDuration = 0;
                attacked.Affected = false;
                attacked.MoveSpeed = prevMoveSpeed;
            }
        }
    }
}
