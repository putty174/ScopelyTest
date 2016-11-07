using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScopelyTreeTest
{
    class Node
    {
        private string nodeName;
        private Node parentNode;
        private List<Node> children;

        public Node(string newNodeName, Node newNodeParent = null)
        {
            nodeName = newNodeName;
            parentNode = newNodeParent;
            children = new List<Node>();
        }

        public void addChild(string newChildName)
        {
            Node newChild = new Node(newChildName, this);
            children.Add(newChild);
        }

        public Node findChild(string childName)
        {
            foreach (var node in children)
            {
                if (node.getName().Equals(childName))
                {
                    return node;
                }
            }
            return null;
        }

        public string getName()
        {
            return nodeName;
        }

        public string getFullPath(Node startingNode)
        {
            if (startingNode.getParent() == null)
            {
                return "/" + startingNode.getName();
            }
            else
            {
                return getFullPath(startingNode.getParent()) + "/" + startingNode.getName();
            }
        }

        public Node getParent()
        {
            return parentNode;
        }

        public List<Node> getChildren()
        {
            return children;
        }

        public void printTreeFrom(Node startingNode)
        {
            if (startingNode.getChildren().Count > 0)
            {
                foreach (var node in startingNode.getChildren())
                {
                    startingNode.printTreeFrom(node);
                }
            }
            else
            {
                Console.WriteLine(startingNode.getFullPath(startingNode));
            }
        }
    }
}