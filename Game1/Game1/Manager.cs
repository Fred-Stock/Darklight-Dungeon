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
        List<Enemies> enemyList = new List<Enemies>();
        List<Obstacle> obstacleList = new List<Obstacle>();
        List<Item> itemList = new List<Item>();

        //global entities
        Player player;

        //constructor
        public Manager(Player player)
        {
            this.player = player; //send in the player to alter it
        }
    }
}
