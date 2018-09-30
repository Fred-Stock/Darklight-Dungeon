using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Node
    {
        //fields
        private Node parent;
        private int distF; //distance from end node
        private int distP; //distance from parent node
        private int distT; //total distance from parent to end
        private int x;
        private int y;

        //properties
        public int DistF
        {
            get { return distF; }
            set { distF = value; }
        }
        public int DistP
        {
            get { return distP; }
            set { distP = value; }
        }
        public int DistT
        {
            get { return distT; }
            set { distT = value; }
        }
        public int X
        {
            get { return x; }
            set { x = value; }
        }
        public int Y
        {
            get { return y; }
            set { y = value; }
        }
        public Node Parent
        {
            get { return parent; }
            set { parent = value; }
        }
        //constructor
        public Node(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        //methods

        /// <summary>
        /// calculates the distance to a certain node
        /// </summary>
        /// <param name="targetNode"></param>
        /// <returns></returns>
        public int DistTo(Node targetNode)
        {
            return Math.Abs(x - targetNode.X) + Math.Abs(y - targetNode.Y);//manhattan distance formula
        }
    }
}
