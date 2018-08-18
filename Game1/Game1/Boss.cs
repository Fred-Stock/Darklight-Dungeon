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
    /* this is the boss enemy
     * they have three possible attacks listed below
     * they follow the player for a time and then stop and do a very fast dash at the player
     * they run extremely fast at the player untill hit
     * 
     * they can only take damage when attacking
     * they only die after a certain amount of hits not hp damage
     */
    class Boss : Enemies
    {
        //fields
        private bool stunned;
        private bool attacking;
        private double attackTimer;
        private double attackBreak;
        private double stunDuration;
        private double timeStunned;
        private double shootTimer;
        private double rateOfFire;
        private Rectangle prevPlayerPos;
        private Random rng;
        private List<Projectile> projList;
        private Texture2D projTexture;

        //properties
        public List<Projectile> ProjList
        {
            get { return projList; }
        }

        public bool Stunned
        {
            get { return stunned; }
            set { stunned = value; }
        }


        //constructor
        public Boss(Texture2D projTexture, Random rng, int health, int damage, Rectangle position, Texture2D texture) : base(rng, health, damage, position, texture)
        {
            moveSpeed = 7;
            this.rng = rng;
            attacking = false;
            attackBreak = 5.0;
            rateOfFire = 0.5;
            projList = new List<Projectile>();
            this.projTexture = projTexture;
            stunned = false;
        }

        //methods
        public override void Move(Characters player, GameTime gameTime)
        {
            prevPos = Position;
            int xDist = player.Position.X - Position.X;
            int yDist = player.Position.Y - Position.Y;
            int xMov;
            int yMov;
            Double tDist = DistanceTo(player.Position.X, player.Position.Y, Position.X, Position.Y);

            if (prevPlayerPos == null)
            {
                prevPlayerPos = player.Position;
            }

            if (!attacking && !stunned)
            {
                Rectangle temp = Position;
                if(tDist < 500)
                {
                    if(moveSpeed > 7)
                    {
                        moveSpeed = 7;
                    }
                    double xRatio = xDist / tDist;
                    double yRatio = yDist / tDist;

                    xMov = (int)(moveSpeed * xRatio);
                    yMov = (int)(moveSpeed * yRatio);

                    temp.X -= xMov;
                    temp.Y += yMov;
                }
                else
                {
                    
                    xMov = player.Position.X - prevPlayerPos.X;
                    yMov = player.Position.Y - prevPlayerPos.Y;
                    temp.X += xMov;
                    temp.Y += yMov;
                }
                Position = temp;

            }

            if (stunned)
            {
                timeStunned += gameTime.ElapsedGameTime.TotalSeconds;

                if(timeStunned >= stunDuration)
                {
                    stunned = false;
                    attacking = false;
                    timeStunned = 0;
                }
            }
            
            prevPos2 = Position;
            prevPlayerPos = player.Position;
        }

        public void Attack(Player player, GameTime gameTime)
        {
            int xDist = player.Position.X - Position.X;
            int yDist = player.Position.Y - Position.Y;
            Double tDist = DistanceTo(player.Position.X, player.Position.Y, Position.X, Position.Y);

            double xRatio = xDist / tDist;
            double yRatio = yDist / tDist;

            int xMov = (int)(moveSpeed * xRatio);
            int yMov = (int)(moveSpeed * yRatio);

            if (!attacking && !stunned)
            {
                attackTimer += gameTime.ElapsedGameTime.TotalSeconds;
                shootTimer += gameTime.ElapsedGameTime.TotalSeconds;

                if(attackTimer >= attackBreak)
                {
                    attacking = true;
                    attackTimer -= attackBreak;
                }

                if(shootTimer >= rateOfFire)
                {
                    shootTimer -= rateOfFire;
                    projList.Add(new Projectile(1, 2, new Rectangle(Position.X, Position.Y, 20, 20), projTexture));
                    if(Math.Abs(xDist) > Math.Abs(yDist))
                    {
                        if(xDist > 0)
                        {
                            projList[projList.Count - 1].Direction = Direction.right;
                        }
                        else
                        {
                            projList[projList.Count - 1].Direction = Direction.left;
                        }
                    }
                    else
                    {
                        if(yDist > 0)
                        {
                            projList[projList.Count - 1].Direction = Direction.down;
                        }
                        else
                        {
                            projList[projList.Count - 1].Direction = Direction.up;
                        }
                    }
                }
                
                
            }
            

            else if(attacking && !stunned)
            {
                Rectangle temp = Position;

               
                moveSpeed = 15;
                temp.X += xMov;
                temp.Y += yMov;
                

                if (player.Position.Intersects(Position))
                {
                    attacking = false;
                    player.TakeDamage(player, this);
                }

                Position = temp;
            }


            for(int i = 0; i < projList.Count; i++)
            {
                projList[i].Move(gameTime);
                projList[i].TakeDamage(player, projList[i]);
                if(projList[i].Health == 0)
                {
                    projList.RemoveAt(i);
                }
            }
        }

        
    }
}
