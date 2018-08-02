﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1 
{
    class Fly : Enemies
    {
        //fields
        private int timer;

        //properties

        //constructor
        public Fly(Random rng, int health, int damage, Rectangle position, Texture2D texture) :
            base(rng, health, damage, position, texture)
        {
            
        }

        //methods
        /// <summary>
        /// the fly enemy will fly towards the player
        /// </summary>
        /// <param name="player"></param>
        public override void Move(Characters player)
        {
            //every frame the character should move a certian distance towards the player
            //to keep that distance consistent a ratio needs to be used since the character is moving at a diagnol

            int xDist = player.Position.X - Position.X;
            int yDist = player.Position.Y - Position.Y;
            Double tDist = DistanceTo(player.Position.X, player.Position.Y, Position.X, Position.Y);

            double ratio = 3 / tDist;

            int xMov = (int)(ratio * xDist);
            int yMov = (int)(ratio * yDist);


            yMov +=(int)(3 * Math.Sin((timer * Math.PI) / 32));
            timer++;
            

            Rectangle temp = Position;
            temp.X += xMov;
            temp.Y += yMov;
            Position = temp;
        }

        /// <summary>
        /// method that calculates the distance from two points using pythag
        /// </summary>
        /// <param name="coord1"></param>
        /// <param name="coord2"></param>
        /// <returns></returns>
        private Double DistanceTo(int coord1X, int coord1Y, int coord2X, int coord2Y)
        {
            int xDist = coord1X - coord2X;
            int yDist = coord1Y - coord2Y;
            return Math.Pow(Math.Pow(xDist, 2) + Math.Pow(yDist, 2), .5);
        }
    }
}
