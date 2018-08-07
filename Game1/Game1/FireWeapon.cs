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
    class FireWeapon : Weapon
    {
        //fields
        int timer;

        //properties


        //constructor
        public FireWeapon(WeaponType weapon, string name, Rectangle position, Texture2D texture) : base(weapon, name, position, texture)
        {
            timer = 0;
            affectDuration = 60;
            damage = 5;
            cost = 25;
        }

        //methods
        public override void WeaponAction(Enemies attacked)
        {
            if (attacked.HitDuration < affectDuration && attacked.Affected)
            {
                 if(attacked.HitDuration % 6 == 0)
                 {
                    attacked.Health -= 1;
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

            }
        }
    }
}
