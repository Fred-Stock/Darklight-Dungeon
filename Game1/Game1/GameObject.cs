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
    class GameObject
    {
        //fields
        private Texture2D texture;
        private Rectangle position;

        //properties
        public Texture2D Texture
        {
            get { return texture; }
        }

        public Rectangle Position
        {
            get { return position; }
            set { position = value; }
        }


        //Consturctor 
        public GameObject(Rectangle position, Texture2D texture)
        {
            this.position = position;
            this.texture = texture;
        }

        /// <summary>
        /// method that calculates the distance from two points using pythag
        /// </summary>
        /// <param name="coord1"></param>
        /// <param name="coord2"></param>
        /// <returns></returns>
        protected Double DistanceTo(int coord1X, int coord1Y, int coord2X, int coord2Y)
        {
            int xDist = coord1X - coord2X;
            int yDist = coord1Y - coord2Y;
            return Math.Pow(Math.Pow(xDist, 2) + Math.Pow(yDist, 2), .5);
        }
    }
}
