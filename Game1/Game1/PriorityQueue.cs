using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class PriorityQueue
    {
        /*
         * priorityQueue
         * sorted by ascending order with lowest value having the highest priority
         * used in this project to keep track of the nodes that still need to be considered
         * the nodes are sorted by their distance from the final node
         */


        //fields
        private List<Node> nodeQueue;

        //properties
        public List<Node> NodeQueue
        {
            get { return nodeQueue; }
        }

        public bool Empty
        {
            get
            {
                if(nodeQueue.Count == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //constructor
        public PriorityQueue()
        {
            nodeQueue = new List<Node>();
        }

        //methods
        public void Push(Node newNode)
        {
            nodeQueue.Add(newNode);
        }

        //naive implementation 0(1) insertion but 0(n) retrieval should implement a heap or something similar to improve complexity
        public Node Pull()
        {
            Node smallNode = nodeQueue[0];
            for(int i = 0; i < NodeQueue.Count; i++)
            {
                if(smallNode.DistT >= nodeQueue[i].DistT)
                {
                    smallNode = nodeQueue[i];
                }
            }

            return smallNode;
        }

        public bool Contains(Node nodeToCheck)
        {
            for(int i = 0; i < NodeQueue.Count; i++)
            {
                if(nodeQueue[i] == nodeToCheck)
                {
                    return true;
                }
            }
            return false;
        }

        public void Remove(Node targetNode)
        {
            nodeQueue.Remove(targetNode);
        }
        
    }
}
