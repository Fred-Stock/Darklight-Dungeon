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


        //properties


        //constructor
        public ShockWeapon(WeaponType weapon, string name, Rectangle position, Texture2D texture) : base(weapon, name, position, texture)
        {
            affectDuration = 1;
            damage = 7;
            cost = 20;
        }

        int prevMoveSpeed = 0;

        //methods
        public override void WeaponAction(Enemies attacked, GameTime gameTime)
        {
            if (attacked.Affected)
            {
                attacked.HitDuration += gameTime.ElapsedGameTime.TotalSeconds;
                if (attacked.HitDuration < affectDuration)
                {
                    if (attacked.MoveSpeed != 0)
                    {
                        prevMoveSpeed = attacked.MoveSpeed;
                        attacked.MoveSpeed = 0; 
                    }
                    else
                    {
                        attacked.MoveSpeed = 0;

                    }
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
}
