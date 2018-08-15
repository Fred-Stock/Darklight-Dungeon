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
        private double dotInterval; //interval for each tick of fire damage
        private int numTicks; //int for number of ticks of damage

        //properties

        //constructor
        public FireWeapon(WeaponType weapon, string name, Rectangle position, Texture2D texture) : base(weapon, name, position, texture)
        {
            affectDuration = 10;
            dotInterval = 1;
            damage = 5;
            cost = 25;
            numTicks = 0;
        }

        //methods
        public override void WeaponAction(Enemies attacked, GameTime gameTime)
        {
            if (attacked.Affected)
            {
                attacked.HitDuration += gameTime.ElapsedGameTime.TotalSeconds;
                if (attacked.HitDuration > dotInterval)
                {
                    attacked.Health -= 3;
                    attacked.HitDuration -= dotInterval;
                    numTicks++;
                }

                if (numTicks >= 5)
                {
                    attacked.HitDuration = 0;
                    attacked.Affected = false;
                    numTicks = 0;

                }
            }
        }
    }
}
