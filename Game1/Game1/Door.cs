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
    class Door : GameObject
    {
        //fields
        private Texture2D middleSprite;
        private Texture2D finalSprite;
        private Texture2D currentTexture;
        private double timePerFrame;
        private double activeTimer;
        private double fps;
        private int frame;
        private bool activated;
        private string nextLevel;

        //properites
        public Texture2D MiddleSprite
        {
            get { return middleSprite; }
            set { middleSprite = value; }
        }

        public Texture2D FinalSprite
        {
            get { return finalSprite; }
            set { finalSprite = value; }
        }

        public Texture2D CurrentTexture
        {
            get { return currentTexture; }
        }

        public bool Activated
        {
            get { return activated; }
            set { activated = value; }
        }
        public int Frame
        {
            get { return frame; }
        }
        public string NextLevel
        {
            get { return nextLevel; }
        }
        

        //constructor
        public Door(Rectangle position, Texture2D initialTexture, Texture2D middleSprite, Texture2D finalSprite, string nextLevel) : base(position, initialTexture)
        {
            this.middleSprite = middleSprite;
            this.finalSprite = finalSprite;
            currentTexture = initialTexture;
            activated = false;
            activeTimer = 0;
            frame = 0;
            fps = 30;
            timePerFrame = 1.0 / fps;
            prevSpeed = 0;
            prevArmorSpeed = 0;
            this.nextLevel = nextLevel;
        }

        //methods
        /// <summary>
        /// this method checks if the door has been activiated and if so animates the sprite accordingly
        /// </summary>
        /// <returns></returns>
        public Texture2D DoorActivation(GameTime gameTime)
        {

            if (activated)
            {
                activeTimer += gameTime.ElapsedGameTime.TotalSeconds;
                if(activeTimer >= timePerFrame)
                {
                    activeTimer -= timePerFrame;
                    frame++;
                }
                if(frame < 8)
                {
                    currentTexture = middleSprite;
                }
                else
                {
                    currentTexture = finalSprite;
                    frame = 0;
                    activated = false;
                }
            }
            return currentTexture;
        }

        /// <summary>
        /// method for transfering between levels
        /// </summary>
        /// <param name="game"></param>
        /// <param name="player"></param>
        int prevSpeed = 0;
        int prevArmorSpeed = 0;
        public bool DoorTransistion(Player player)
        {
            if (frame == 0)
            {
                if(prevSpeed == 0)
                {
                    prevSpeed = player.MoveSpeed;

                }
                player.MoveSpeed = 0;
                if(player.Armor is SpeedArmor tempAr && prevArmorSpeed != 0)
                {
                    tempAr = (SpeedArmor)player.Armor;
                    prevArmorSpeed = tempAr.SpeedBoost;
                    tempAr.SpeedBoost = 0;
                    player.Armor = tempAr;
                }
            }

            Activated = true;
            if(frame == 7)
            {
                player.MoveSpeed = prevSpeed;
                if(player.Armor is SpeedArmor tempAr)
                {
                    tempAr = (SpeedArmor)player.Armor;
                    tempAr.SpeedBoost = prevArmorSpeed;                   
                    player.Armor = tempAr;
                }
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
