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
    class Enemies : Characters
    {
        //fields
        private Random rng;
        private int initialX;
        private int initialY;
        private int direction;
        private double moveTimer;
        private double moveInterval;
        private bool hit;
        protected bool affected;
        protected Rectangle prevPos2;


        protected double hitDuration;

        //properties
        public bool Hit
        {
            get { return hit; }
            set { hit = value; }
        }
        public bool Affected
        {
            get { return affected; }
            set { affected = value; }
        }
        public double HitDuration
        {
            get { return hitDuration; }
            set { hitDuration = value; }
        }


        //constructor
        public Enemies(Random rng, int health, int damage, Rectangle position, Texture2D texture) : base(health, damage, position, texture)
        {
            hit = false;
            affected = false;
            this.rng = rng;
            initialX = Position.X;
            initialY = Position.Y;
            moveSpeed = 3;
            direction = rng.Next(0, 4);
            hitDuration = 0;
            moveInterval = 2;
            moveTimer = 0;
        }

        public override void Move(Characters player, GameTime gameTime)
        {
            prevPos = Position;
            int xDist = player.Position.X - Position.X;
            int yDist = player.Position.Y - Position.Y;
            Double tDist = DistanceTo(player.Position.X, player.Position.Y, Position.X, Position.Y);

            int xMov = 0;
            int yMov = 0;

            if (tDist != 0)
            {
                double xRatio = xDist / tDist;
                double yRatio = yDist / tDist;

                xMov = (int)(moveSpeed * xRatio);
                yMov = (int)(moveSpeed * yRatio);
            }

            Rectangle temp = Position;
            if(tDist < 500)
            {
                temp.X += xMov;
                temp.Y += yMov;
                Position = temp;

                if (prevPos2.X == Position.X && xMov != 0)
                {
                    if(yDist < 0)
                    {
                        temp.Y -= moveSpeed;
                    }
                    else
                    {
                        temp.Y += moveSpeed;
                    }

                    Position = temp;
                }
                if (prevPos2.Y == Position.Y && (yMov != 0))
                {
                    if(xDist <= 0)
                    {
                        temp.X -= moveSpeed;

                    }
                    else
                    {
                        temp.X += moveSpeed;
                    }

                }

            }
            
            if(tDist >= 500)
            {

                moveTimer += gameTime.ElapsedGameTime.TotalSeconds;
                if (moveTimer < moveInterval)
                {

                    if (direction == 0)
                    {
                        temp.Y -= moveSpeed;
                    }
                    else if (direction == 1)
                    {
                        temp.X += moveSpeed;
                    }
                    else if (direction == 2)
                    {
                        temp.Y += moveSpeed;
                    }
                    else
                    {
                        temp.X -= moveSpeed;
                    }
                }
                else
                {
                    moveTimer -= moveInterval;
                    direction = rng.Next(0, 4);
                }
                
            }
            
            Position = temp;
            prevPos2 = Position;

        }

        public override void TakeDamage(Player damaged, Characters damager)
        {
            if (damager.Position.Intersects(damaged.Position) && !damaged.Hit)
            {
                if(damaged.Armor is ThornArmor)
                {
                    damaged.Armor.ArmorAction(this);
                }
                else if(damaged.Armor != null)
                {
                    damaged.Armor.ArmorAction();
                }
                if(damaged.Armor is ShieldArmor temp)
                {
                    temp = (ShieldArmor)damaged.Armor;
                    if (temp.Popped)
                    {
                        damaged.Health -= damage - damaged.Armor.Defense;
                    }
                }
                else if(damaged.Armor != null)
                {
                    damaged.Health -= damage - damaged.Armor.Defense;
            
                }
                else
                {
                    damaged.Health -= damage;
                }
            }
        }
    }
}

