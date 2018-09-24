using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class TreeNode
    {
        /*
         * object for the binary search tree to sort nodes
         */

        //fields
        private TreeNode parent;
        private TreeNode leftBranch;
        private TreeNode rightBranch;
        private Node data;

        //properties
        public TreeNode Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public TreeNode LeftBranch
        {
            get { return leftBranch; }
            set { leftBranch = value; }
        }

        public TreeNode RightBranch
        {
            get { return rightBranch; }
            set { rightBranch = value; }
        }

        public int NodeData //only data that really matters from the node is the DistF property
        {
            get { return data.DistF; }
        }

        public Node Data
        {
            get { return data; }
        }


        //constructor
        public TreeNode(Node data)
        {
           this.data = data;
        }

        
    }
}
