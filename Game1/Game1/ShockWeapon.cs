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
    class ShockWeapon : Weapon
    {
        //fields
        int timer;
        private bool stopped;

        //properties


        //constructor
        public ShockWeapon(WeaponType weapon, string name, Rectangle position, Texture2D texture) : base(weapon, name, position, texture)
        {
            timer = 0;
            affectDuration = 45;
            damage = 7;
            cost = 20;
        }

        int prevMoveSpeed = 0;

        //methods
        public override void WeaponAction(Enemies attacked)
        {
            if (attacked.HitDuration < affectDuration && attacked.Affected)
            {
                if(attacked.HitDuration == 0)
                {
                    prevMoveSpeed = attacked.MoveSpeed;
                }
                if(attacked.HitDuration > 1)
                {
                    attacked.MoveSpeed = 0;

                }
            }

            if (attacked.Affected)
            {
                attacked.HitDuration++;
            }
            if(attacked.HitDuration > affectDuration)
            {
                attacked.HitDuration = 0;
                attacked.Affected = false;
                attacked.MoveSpeed = prevMoveSpeed;
            }
        }
    }
}
