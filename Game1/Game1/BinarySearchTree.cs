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
        private bool contained;
        //properties

        //constructor
        public BinarySearchTree()
        {
            tree = new List<TreeNode>();
        }

        //methods
        public void Add(Node node)
        {
            Add(node, parent);
           
        }

        private void Add(Node node, TreeNode current)
        {
            currentNode = current;
            if (tree.Count == 0)
            {
                TreeNode addedNode = new TreeNode(node);
                parent = addedNode;
                tree.Add(addedNode);

            }
            else
            {

                if (currentNode.NodeData < node.DistF)
                {
                    if (currentNode.RightBranch == null)
                    {
                        TreeNode addedNode = new TreeNode(node);
                        currentNode.RightBranch = addedNode;
                        tree.Add(addedNode);
                        return;
                    }
                    else
                    {
                        currentNode = currentNode.RightBranch;
                        Add(node, currentNode);
                    }
                }
                else
                {
                    if (currentNode.LeftBranch == null)
                    {
                        TreeNode addedNode = new TreeNode(node);
                        currentNode.LeftBranch = addedNode;
                        tree.Add(addedNode);
                        return;
                    }
                    else
                    {
                        currentNode = currentNode.LeftBranch;
                        Add(node, currentNode);
                    }
                }
            }
        }

        //public LinkedList<Node> ShortestPath()
        //{
        //    LinkedList<Node> path = new LinkedList<Node>();
        //    currentNode = parent;
        //    while(currentNode != null)
        //    {
        //        path.AddLast(currentNode.Data);
        //        currentNode = currentNode.LeftBranch;
        //    }
        //    return path;
        //}

        //method to see if a node is contained with the tree
        public bool Contains(Node nodeToCheck)
        {
            contained = false;
            return Contains(nodeToCheck, parent);
            
            
        }

        private bool Contains(Node nodeToCheck, TreeNode current)
        {
            currentNode = current;
            
            if (currentNode.Data == nodeToCheck)
            {
                contained = true;

            }
            else if (nodeToCheck.DistF < currentNode.NodeData)
            {
                if (currentNode.LeftBranch != null)
                {
                    currentNode = currentNode.LeftBranch;
                    Contains(nodeToCheck, currentNode);

                }
                else
                {
                    contained = false;
                }
            }
            else
            {
                if (currentNode.RightBranch != null)
                {
                    currentNode = currentNode.RightBranch;
                    Contains(nodeToCheck, currentNode);

                }
                else
                {
                    contained = false;
                }
            }
            return contained;
        }
    }
}
