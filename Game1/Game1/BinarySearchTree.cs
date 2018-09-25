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
        /// <summary>
        /// method that adds a new node to the tree
        /// </summary>
        /// <param name="node">data to be added to the tree</param>
        public void Add(Node node)
        {
            //this method is used so outside classes can easily add things to the tree without knowing the parent node
            Add(node, parent);
        }

        //adds a node to the tree
        //this method is 
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

        /// <summary>
        /// method to see if a node is contained within the tree
        /// </summary>
        /// <param name="nodeToCheck"></param>
        /// <returns></returns>
        public bool Contains(Node nodeToCheck)
        {
            //this method is used so that an outside class can easily use the contains method without knowing the parent node
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
