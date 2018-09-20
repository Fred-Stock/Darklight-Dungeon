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
            this.startNode = startNode;
            this.endNode = endNode;
            currentNode = startNode;
            this.nodeGraph = nodeGraph;
        }

        //methods


    }
}
