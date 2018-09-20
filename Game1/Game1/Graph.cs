using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Graph
    {
        //fields
        private char[,] levelMatrix;
        private List<Node> levelNodes;
        private Node playerSpawn;//do i need this

        //properties
        public char[,] LevelMatrix
        {
            get { return levelMatrix; }
            set { levelMatrix = value; }
        }

        public List<Node> LevelNodes
        {
            get { return levelNodes; }
        }

        //constructor
        public Graph(char[,] levelMatrix)
        {
            this.levelMatrix = levelMatrix;
            levelNodes = new List<Node>();
        }

        //methods
        public void GenNodes()
        {
            for(int i = 0; i < levelMatrix.GetLength(0); i++)
            {
                for(int j = 0; j < levelMatrix.GetLength(1); j++)
                {
                    //rework for the fly class
                    if(LevelMatrix[i,j] == '-' || LevelMatrix[i,j] == 'R')//walls and rocks are not transfersable so they should not be made into nodes
                    {
                        continue;
                    }
                    else if(LevelMatrix[i,j] == 'P')
                    {
                        playerSpawn = new Node(j, i);
                        levelNodes.Add(playerSpawn);
                    }
                    else
                    {
                        levelNodes.Add(new Node(j, i));
                    }
                }
            }
        }

    }
}
