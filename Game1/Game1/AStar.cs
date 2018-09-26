using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class AStar
    {
        //fields
        private Node currentNode;
        private Node startNode;
        private Node endNode;
        private Node nodeChecking;
        private LinkedList<Node> path;
        private Graph nodeGraph;
        private PriorityQueue openSet;
        private BinarySearchTree closedSet;

        //open set
        //closed set

        //properties

        //Constructor
        public AStar(Node startNode, Node endNode, Graph nodeGraph)
        {
            closedSet = new BinarySearchTree();
            openSet = new PriorityQueue();
            this.startNode = startNode;
            this.endNode = endNode;
            currentNode = startNode;
            this.nodeGraph = nodeGraph;
        }

        //methods
      public LinkedList<Node> FindPath(Characters player, Characters pathFinder)
      {
          path.Clear();
          nodeGraph.GenNodes();
          closedSet = new BinarySearchTree();
          openSet = new PriorityQueue();
          currentNode =nodeGraph.NodeMatrix[(pathFinder.Position.X / 120), (pathFinder.Position.Y / 120) + 1];
          //if(player.Position.X/120 != endNode.X || player.Position.Y/120 != endNode.Y)
          //{
            //endNode = new Node((player.Position.X / 120), (player.Position.Y / 120) + 1);
          //}
      
          return FindPath();
      
          
      }

        public LinkedList<Node> FindPath()
        {
            if (path != null)
            {
                path.Clear();

            }
            closedSet.Add(currentNode);
            do
            {

                //loop through all adjacent nodes
                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {

                        if (currentNode.X == 0 && j == -1)
                        {
                            //if the node is on the far left edge of the array there is no node to the left of it
                            continue;
                        }
                        else if (currentNode.X == 15 && j == 1)
                        {
                            //if the node is on the far right edge of the array there is no node to the right of it
                            continue;
                        }
                        else if (currentNode.Y == 0 && i == -1)
                        {
                            //if the node is on the top row of the array there are no nodes above it
                            continue;
                        }
                        else if (currentNode.Y == 8 && i == 1)
                        {
                            //if the node is on the bottom of the array there are no nodes below it
                            continue;
                        }
                        else if(i == 0 && j== 0)
                        {
                            //dont check the current node
                            continue;
                        }
                        else if(i != 0 && j != 0)
                        {
                            continue;
                        }
                        else if (nodeGraph.NodeMatrix[currentNode.X + j, currentNode.Y + i] != null)
                        {
                            nodeChecking = nodeGraph.NodeMatrix[currentNode.X + j, currentNode.Y + i]; //set an temporary instance of the node being checked

                            if (closedSet.Contains(nodeChecking))
                            {
                                continue;
                            }
                            else if (openSet.Contains(nodeChecking))
                            {
                                int newP = nodeChecking.DistTo(currentNode);
                                //if the node being checked is closer to the current node than its current parent set it to the parent
                                if (newP < nodeChecking.DistP)
                                {
                                    nodeChecking.DistP = newP;
                                    nodeChecking.Parent = currentNode;
                                    nodeChecking.DistT = nodeChecking.DistF + nodeChecking.DistP;
                                }
                            }
                            //if node has not been adopted then adopt it and calculate its distances
                            else
                            {
                                if (currentNode.Parent != nodeChecking)
                                {
                                    nodeChecking.Parent = currentNode;
                                    nodeChecking.DistP = nodeChecking.DistTo(currentNode);
                                    nodeChecking.DistF = nodeChecking.DistTo(endNode);
                                    nodeChecking.DistT = nodeChecking.DistF + nodeChecking.DistP;
                                    openSet.Push(nodeChecking);

                                }
                            }

                        }

                    }

                }
                if (openSet.Empty)
                {
                    break;
                }
                    
                currentNode = openSet.Pull();
                openSet.Remove(currentNode);
                closedSet.Add(currentNode);
                
            } while (currentNode != endNode);
            path = new LinkedList<Node>();
            path = nodeGraph.ShortestPath(startNode);
            return path;
        }

    }
}
