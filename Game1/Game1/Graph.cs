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
        private Node[,] nodeMatrix;
        private Node playerSpawn;
        private LinkedList<Node> path;

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

        public Node[,] NodeMatrix
        {
            get { return nodeMatrix; }
        }

        public Node PlayerNode //this currently wont chang based on player location so change this
        {
            get {return playerSpawn; }
        }

        //constructor
        public Graph(char[,] levelMatrix)
        {
            this.levelMatrix = levelMatrix;
            levelNodes = new List<Node>();
            nodeMatrix = new Node[16, 10];
            path = new LinkedList<Node>();
        }

        //methods
        public void GenNodes()
        {
            for(int i = 0; i < levelMatrix.GetLength(1) - 1; i++)
            {
                for(int j = 0; j < levelMatrix.GetLength(0); j++)
                {
                    //rework for the fly class
                    if(LevelMatrix[j,i] == '-' || LevelMatrix[j,i] == 'R')//walls and rocks are not transfersable so they should not be made into nodes
                    {
                        nodeMatrix[j, i] = null;
                    }
                    else if(LevelMatrix[j, i] == 'P')
                    {
                        playerSpawn = new Node(j,i);
                        levelNodes.Add(playerSpawn);
                        nodeMatrix[j, i] = playerSpawn;
                    }
                    else
                    {
                        levelNodes.Add(new Node(j, i));
                        nodeMatrix[j, i] = new Node(j, i);
                    }
                }
            }
        }

        public LinkedList<Node> ShortestPath(Node startNode)
        {
            path.Clear();
            Node Current = nodeMatrix[playerSpawn.X, playerSpawn.Y];
            while(Current != startNode)
            {
                path.AddFirst(Current);
                Current = Current.Parent;
            }
            return path;
        }
    }
}
