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


        //properties


        //constructor
        public FrostWeapon(WeaponType weapon, string name, Rectangle position, Texture2D texture) : base(weapon, name, position, texture)
        {
            affectDuration = 3;
            damage = 7;
            cost = 15;
        }

        int prevMoveSpeed = 0;
        //method
        public override void WeaponAction(Enemies attacked, GameTime gameTime)
        {
            if (attacked.Affected)
            {
                attacked.HitDuration += gameTime.ElapsedGameTime.TotalSeconds;
                if (attacked.HitDuration < affectDuration)
                {
                    if(attacked.HitDuration == gameTime.ElapsedGameTime.TotalSeconds)
                    {
                        prevMoveSpeed = attacked.MoveSpeed;
                        attacked.MoveSpeed = (int)Math.Ceiling((double)attacked.MoveSpeed / 2);
                    }

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
}
