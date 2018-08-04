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
            affectDuration = 120;
            damage = 5;
        }

        //methods
        public override void WeaponAction(Enemies attacked)
        {
             if (attacked.Affected)
             {
                 timer++;
                 if(timer % 12 == 0)
                 {
                    attacked.Health -= 1;
                 }
             }
            
             else if(timer > affectDuration && attacked.Affected)
             {
                 timer = 0;
                 attacked.Affected = false;
             
             }
        }
    }
}
