using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Level
    {
        //fields
        private char[,] levelArray;
        private bool won;
        private List<Enemies> enemyList;
        private List<Obstacle> wallList;

        //properties
        public char[,] LevelArray
        {
            get { return levelArray; }
        }
        public List<Obstacle> WallList
        {
            get { return wallList; }
        }
        public bool Won
        {
            get { return won; }
        }

        //constructor
        public Level(char[,] levelArray, Manager manager)
        {
            wallList = new List<Obstacle>();
            enemyList = manager.EnemyList;
            this.levelArray = levelArray;
            won = false;
        }
        //methods
        public bool WinCheck()
        {
            won = true;
            for(int i = 0; i < enemyList.Count; i++)
            {
                if (enemyList[i] != null)
                {
                    won = false;
                }
            }
            return won;
        }
    }
}
