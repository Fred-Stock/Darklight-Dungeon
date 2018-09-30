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
        protected bool affected;
        protected Rectangle prevPos2;
        private AStar astar;
        private Level level;
        private Node currentNode;
        private LinkedList<Node> path;
        private Node next;
        private int step;
        private Graph levelGraph;

        protected double hitDuration;

        //properties
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
        public Enemies(Level level, Random rng, int health, int damage, Rectangle position, Texture2D texture) : base(health, damage, position, texture)
        {

            this.level = new Level(level.LevelArray, level.MAnager);
            currentNode = new Node((position.X / 120), (position.Y / 120));
            levelGraph = this.level.NodeGraph;
            levelGraph.GenNodes();
            astar = new AStar(currentNode, this.level.NodeGraph.PlayerNode, this.level.NodeGraph);
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
            step = 0;
            path = astar.FindPath();
        }

        /// <summary>
        /// calculates a path using a* and moves the enemy a long that path
        /// </summary>
        /// <param name="player"></param>
        /// <param name="gameTime"></param>
        public override void Move(Characters player, GameTime gameTime)
        {

            prevPos = Position;
            Rectangle temp = Position;


            if (step < path.Count)
            {
                next = path.ElementAt(step);
            }
            if(next.X > currentNode.X || next.X < currentNode.X)
            {
                if (((next.X)) > currentNode.X)
                {
                    temp.X += moveSpeed;
                    if (temp.X >= ((next.X) * 120))
                    {
                        step++;
                        currentNode.X = Position.X / 120;
                        //path.Clear();
                        //path = astar.FindPath(player, this);
                    }
                }
                else if (((next.X)) < currentNode.X)
                {
                    temp.X -= moveSpeed;
                    if (temp.X <= ((next.X) * 120) + (moveSpeed*4))
                    {
                        step++;
                        currentNode.X = Position.X / 120;
                        //path.Clear();
                        //path = astar.FindPath(player, this);
                    }
                }
            }
            else
            {
                if (((next.Y)) > currentNode.Y)
                {
                    temp.Y += moveSpeed;
                    if (temp.Y >= ((next.Y - 1) * 120))
                    {
                        step++;
                        currentNode.Y = (Position.Y / 120) + 1;
                        //path.Clear();
                        //path = astar.FindPath(player, this);
                    }
                }
                else if (((next.Y)) <= currentNode.Y)
                {
                    temp.Y -= moveSpeed;
                    if (temp.Y <= ((next.Y - 1) * 120))
                    {
                        step++;
                        currentNode.Y = (Position.Y / 120) + 1;
                        //path.Clear();
                        //path = astar.FindPath(player, this);
                    }
                }

            }



            //int xDist = player.Position.X - Position.X;
            //int yDist = player.Position.Y - Position.Y;
            //Double tDist = DistanceTo(player.Position.X, player.Position.Y, Position.X, Position.Y);

            //int xMov = 0;
            //int yMov = 0;

            //if (tDist != 0)
            //{
            //    double xRatio = xDist / tDist;
            //    double yRatio = yDist / tDist;

            //    xMov = (int)(moveSpeed * xRatio);
            //    yMov = (int)(moveSpeed * yRatio);
            //}

            //if(tDist < 500)
            //{
            //    temp.X += xMov;
            //    temp.Y += yMov;
            //    Position = temp;

            //    if (prevPos2.X == Position.X && xMov != 0)
            //    {
            //        if(yDist < 0)
            //        {
            //            temp.Y -= moveSpeed;
            //        }
            //        else
            //        {
            //            temp.Y += moveSpeed;
            //        }

            //        Position = temp;
            //    }
            //    if (prevPos2.Y == Position.Y && (yMov != 0))
            //    {
            //        if(xDist <= 0)
            //        {
            //            temp.X -= moveSpeed;

            //        }
            //        else
            //        {
            //            temp.X += moveSpeed;
            //        }

            //    }

            //}

            //if(tDist >= 500)
            //{

            //    moveTimer += gameTime.ElapsedGameTime.TotalSeconds;
            //    if (moveTimer < moveInterval)
            //    {

            //        if (direction == 0)
            //        {
            //            temp.Y -= moveSpeed;
            //        }
            //        else if (direction == 1)
            //        {
            //            temp.X += moveSpeed;
            //        }
            //        else if (direction == 2)
            //        {
            //            temp.Y += moveSpeed;
            //        }
            //        else
            //        {
            //            temp.X -= moveSpeed;
            //        }
            //    }
            //    else
            //    {
            //        moveTimer -= moveInterval;
            //        direction = rng.Next(0, 4);
            //    }

            //}


            Position = temp;
            prevPos2 = Position;

        }

        /// <summary>
        /// deals damage to a player
        /// </summary>
        /// <param name="damaged"></param>
        /// <param name="damager"></param>
        public override void TakeDamage(Player damaged, Characters damager)
        {
            if (damager.Position.Intersects(damaged.Position) && !damaged.Hit) //check if the character has had damage dealt to them already from this collision
            {
                //if the player has special armor inact its effect and then deal damage
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
                //if the player has standard damage just apply normal damage reduction
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

