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
    class LightSource : GameObject
    {
        //fields
        private Texture2D lightEffect;
        private double opacity;
        private double opacityTimer;
        private double opacityCycleTime;
        private Rectangle lightEffectPos;

        //properties
        public Texture2D LightEffect
        {
            get { return lightEffect; }
        }

        public Rectangle LightEffectPos
        {
            get { return lightEffectPos; }
        }
        //constructor
        public LightSource(Texture2D lightEffect, Rectangle position, Texture2D texture) : base(position, texture)
        {
            this.lightEffect = lightEffect;
            opacityTimer = 0;
            opacityCycleTime = 10;

            lightEffectPos = position;
            lightEffectPos.Width = lightEffect.Width;
            lightEffectPos.Height = lightEffect.Height;
            lightEffectPos.X -= lightEffectPos.Width / 3;
            lightEffectPos.Y -= lightEffectPos.Height / 3;
        }


        //methods
        
        //this method outputs a double that will cycle through a range of values to create a flickering effect
        public float Flicker(GameTime gameTime)
        {

            opacityTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if(opacityTimer >= opacityCycleTime)
            {
                opacityTimer -= opacityCycleTime;
            }

            return (float)Math.Abs(Math.Sin(opacityTimer * (Math.PI/5)));
        }
    }
}
