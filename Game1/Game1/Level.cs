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
        private List<Item> itemList;

        //properties
        public char[,] LevelArray
        {
            get { return levelArray; }
        }
        public bool Won
        {
            get { return won; }
        }
        public List<Enemies> EnemyList
        {
            get { return enemyList; }
            set { enemyList = value; }
        }
        public List<Item> ItemList
        {
            get { return itemList; }
            set { itemList = value; }
        }

        //constructor
        public Level(char[,] levelArray)
        {
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
