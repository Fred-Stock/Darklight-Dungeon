using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class BinarySearchTree
    {
        //fields
        private List<TreeNode> tree;
        private TreeNode parent;
        private TreeNode currentNode;

        //properties

        //constructor
        public BinarySearchTree()
        {
            tree = new List<TreeNode>();
        }

        //methods
        public void Add(TreeNode node)
        {
            if(tree.Count == 0)
            {
                parent = node;

            }
            else
            {
                currentNode = parent;
                if(currentNode.Data < node.Data)
                {
                    if(currentNode.LeftBranch == null)
                    {
                        currentNode.LeftBranch = node;
                    }
                    else
                    {
                        currentNode = currentNode.LeftBranch;
                        Add(node);
                    }
                }
                else
                {
                    if (currentNode.RightBranch == null)
                    {
                        currentNode.RightBranch = node;
                    }
                    else
                    {
                        currentNode = currentNode.RightBranch;
                        Add(node);
                    }
                }
            }
        }

        public LinkedList<Node> ShortestPath()
        {
            LinkedList<Node> path = new LinkedList<Node>();
            currentNode = parent;
            while(currentNode != null)
            {
                path.AddLast(currentNode.DataNode);
                currentNode = currentNode.LeftBranch;
            }
            return path;
        }

    }
}
