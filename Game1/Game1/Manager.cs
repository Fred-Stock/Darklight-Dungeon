using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Manager
    {
        //data structures
        private List<Obstacle> obstacleList = new List<Obstacle>();
        private List<Enemies> enemyList = new List<Enemies>();
        private List<Item> itemList = new List<Item>();

        //properties
        public List<Item> ItemList
        {
            get { return itemList; }
        }

        //global entities
        Player player;

        //constructor
        public Manager(Player player)
        {
            this.player = player; //send in the player to alter it
        }
    }
}
