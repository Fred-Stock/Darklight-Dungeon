
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
        private int timer;
        private bool activated;
        private Level nextLevel;

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
        public int Timer
        {
            get { return timer; }
        }

        //constructor
        public Door(Rectangle position, Texture2D initialTexture, Texture2D middleSprite, Texture2D finalSprite) : base(position, initialTexture)
        {
            this.middleSprite = middleSprite;
            this.finalSprite = finalSprite;
            currentTexture = initialTexture;
            activated = false;
            timer = 0;

        }

        //methods
        /// <summary>
        /// this method checks if the door has been activiated and if so animates the sprite accordingly
        /// </summary>
        /// <returns></returns>
        public Texture2D DoorActivation()
        {

            if (activated)
            {
                timer++;
                if(timer < 160)
                {
                    currentTexture = middleSprite;
                }
                if (timer == 160)
                {
                    currentTexture = finalSprite;
                    timer = 0;
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
        public void DoorTransistion(Game game, Player player)
        {
            int prevSpeed = player.MoveSpeed;
            player.MoveSpeed = 0;
            Activated = true;
            if(timer >= 160)
            {
                player.MoveSpeed = prevSpeed;
              
            }
        }


    }
}
