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
    class Manager
    {
        //data structures
        private List<Obstacle> obstacleList = new List<Obstacle>();
        private List<Enemies> enemyList = new List<Enemies>();
        private List<Enemies> affectedEnemies = new List<Enemies>();
        private List<Item> itemList = new List<Item>();
        private List<LightSource> lightList = new List<LightSource>();
        
        private List<Door> doorList = new List<Door>();
        //properties
        public List<Item> ItemList
        {
            get { return itemList; }
        }
        public List<Door> DoorList
        {
            get { return doorList; }
        }
        public List<Enemies> EnemyList
        {
            get { return enemyList; }
            set { enemyList = value; }
        }
        public List<Enemies> AffectedEnemies
        {
            get { return affectedEnemies; }
            set { affectedEnemies = value; }
        }
        public List<LightSource> LightList
        {
            get { return lightList; }
            set { lightList = value; }
        }


        //global entities
        Player player;

        //constructor
        public Manager(Player player)
        {
            this.player = player; //send in the player to alter it
        }

        //methods
        public void WeaponAffects(Player player, GameTime gameTime)
        {
            for(int i = 0; i < enemyList.Count; i++)
            {
                if(enemyList[i] != null)
                {
                    if(player.Weapon != null && !(enemyList[i] is Boss))
                    {
                        player.Weapon.WeaponAction(enemyList[i], gameTime);
                    }
                }
            }
        }

    }
}
