using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace BFSGraphical
{
    class BFS
    {

        private int width = -1;

        private int height = -1;

        private int nodeSize = -1;

        public BFS(int newWidth, int newHeight, int newNodeSize)
        {

            width = newWidth;

            height = newHeight;

            nodeSize = newNodeSize;

        }

        class TestThreadStart
        {


            delegate void testDelegate();

            public static void setColor()
            {

                Console.WriteLine("Test Thread Has Executed!");

            }

            public static void testMethod()
            {

            }


        }

        public void runThread()
        {

            Thread testThread = new Thread(TestThreadStart.setColor);

            testThread.Start();

        }

        // Tree Node

        public class Node
        {

            String nodeStr = "";

            List<Node> children = new List<Node>();

            public Node(String nodeStr)
            {

                this.nodeStr = nodeStr;

            }
            
            public String getNodeStr()
            {

                return nodeStr;

            }

            public void addChild(Node child)
            {

                children.Add(child);

            }

            public Node getNextChild()
            {

                if(children.Count > 0)
                {

                    Node n = children.ElementAt(0);

                    children.RemoveAt(0);

                    return n;

                }
                else
                {

                    return null;

                }

            }

        }

        public class DisplayNode
        {

            private int x = -1;

            private int y = -1;

            private DisplayNode parent = null;

            private int level = -1;

            private String displayStr;

            public int GetX()
            {

                return x;

            }

            public int GetY()
            {

                return y;

            }

            public void SetX(int newX)
            {

                x = newX;

            }

            public void SetY(int newY)
            {

                y = newY;

            }

            public DisplayNode GetParent()
            {

                return parent;

            }

            public void SetParent(DisplayNode newParent)
            {

                parent = newParent;

            }

            public int GetLevel()
            {

                return level;

            }

            public void SetLevel(int newLevel)
            {

                level = newLevel;

            }

            public String GetDisplayStr()
            {

                return displayStr;

            }

            public void SetDisplayStr(String newDisplayStr)
            {

                displayStr = newDisplayStr;

            }

            public override String ToString()
            {

                String outputString = "";

                outputString += "X: " + x;

                outputString += " Y: " + y;

                outputString += " Level: " + level;

                outputString += " Display String: " + displayStr;

                outputString += " Parent: " + parent;

                return outputString;

            }
        
        }

        private DisplayTree displayTree = null;

        public class DisplayTree
        {

            List<DisplayNode> nodes = new List<DisplayNode>();

            int width = -1;

            int height = -1;

            int nodeSize = -1;

            public List<DisplayNode> GetNodes()
            {

                return nodes;

            }

            public DisplayNode getDisplayNodeAt(int index)
            {

                return nodes.ElementAt(index);

            }

            public int GetDisplayNodeCount()
            {

                return nodes.Count;

            }

            public DisplayTree(int width, int height, int nodeSize)
            {

                this.width = width;

                this.height = height;

                this.nodeSize = nodeSize;

            }

            // add display node

            // @params

            // Node newNode -> Node contains all data for the Node. This is a simple way to instantiate its display representation;

            // Node parentNode -> parent node is stored to draw parent/child relationships graphically.

            // int level -> Level indicates the depth of the Node. This will be stored to later represent the Y position of the Node.

            // int numberLevelNodes -> value indicates the number of nodes at the passed level value. This is used to calculate the X position of the node.

            // int levelPosition -> value indicates X position of the current node. Level may contain 0 - numberLevelNodes nodes. X position will depend on the value contained herein.

            // DisplayNode displayNode -> if parameter is null, instantiate new display node; else -> use this parameter as adding display node.

            public void AddDisplayNode(Node newNode, DisplayNode parentNode, int level, int numberLevelNodes, int levelPosition, DisplayNode newDisplayNode )
            {

                DisplayNode displayNode = null;


                if (newDisplayNode == null)
                {

                    displayNode = new DisplayNode();

                }
                else
                {

                    displayNode = newDisplayNode;

                }

                // TODO: Display Node can contain node string

                displayNode.SetDisplayStr(newNode.getNodeStr());

                displayNode.SetParent(parentNode);

                displayNode.SetLevel(level);

                // node partition width

                int nodePartitionWidth = width / numberLevelNodes;

                int displayNodeX = nodePartitionWidth * levelPosition;

                nodePartitionWidth /= 2;

                displayNodeX += nodePartitionWidth;

                displayNodeX -= nodeSize / 2;

                displayNode.SetX(displayNodeX);

                nodes.Add(displayNode);

                Console.WriteLine("Node: " + displayNode);

                Console.WriteLine("Add Display Node Parameters; Level: " + level + " NumberLevelNodes: " + numberLevelNodes + " LevelPosition: " + levelPosition);

            }

            
            public void CalculateDisplayTreeYValues(int totalLevels)
            {

                int levelHeight = height / totalLevels;

                for(int nodeCounter = 0; nodeCounter < nodes.Count; nodeCounter++ )
                {

                    int nodeLevel = nodes.ElementAt(nodeCounter).GetLevel() - 1;

                    int nodeY = levelHeight * nodeLevel;

                    nodes.ElementAt(nodeCounter).SetY(nodeY + levelHeight / 2);

                    Console.WriteLine("Calculate Y Values: " + nodes.ElementAt(nodeCounter));

                }

            }
            

        }

        // Node Tree

        public class NodeTree
        {

            public Node GetTreeA()
            {

                Node rootNode = new Node("Root");

                Node lvl2chl1 = new Node("Level 2 Child 1");

                Node lvl2chl2 = new Node("Level 2 Child 2");

                rootNode.addChild(lvl2chl1);

                rootNode.addChild(lvl2chl2);

                Node lvl2chl3 = new Node("Level 2 Child 3");

                rootNode.addChild(lvl2chl3);

                Node lvl3chl5 = new Node("Level 2 Child 5");

                lvl2chl3.addChild(lvl3chl5);

                Node lvl3chl1 = new Node("Level 3 Child 1");

                Node lvl3chl2 = new Node("Level 3 Child 2");

                lvl2chl1.addChild(lvl3chl1);

                lvl2chl1.addChild(lvl3chl2);

                Node lvl3chl3 = new Node("Level 3 Child 3");

                Node lvl3chl4 = new Node("Level 3 Child 4");

                lvl2chl2.addChild(lvl3chl3);

                lvl2chl2.addChild(lvl3chl4);

                Node lvl4chl1 = new Node("Level 4 Child 1");

                Node lvl4chl2 = new Node("Level 4 Child 2");

                lvl3chl1.addChild(lvl4chl1);

                lvl3chl1.addChild(lvl4chl2);

                Node lvl4chl3 = new Node("Level 4 Child 3");

                Node lvl4chl4 = new Node("Level 4 Child 4");

                lvl3chl2.addChild(lvl4chl3);

                lvl3chl2.addChild(lvl4chl4);

                Node lvl4chl5 = new Node("Level 4 Child 5");

                Node lvl4chl6 = new Node("Level 4 Child 6");

                lvl3chl3.addChild(lvl4chl5);

                lvl3chl3.addChild(lvl4chl6);

                Node lvl4chl7 = new Node("Level 4 Child 7");

                Node lvl4chl8 = new Node("Level 4 Child 8");

                lvl3chl4.addChild(lvl4chl7);

                lvl3chl4.addChild(lvl4chl8);

                return rootNode;

            }

        }

        public DisplayTree runSearch1()
        {

            NodeTree search1Tree = new NodeTree();

            return search(search1Tree, "Level 2 Child 2");

        }

        public DisplayTree search(NodeTree tree, String targetString)
        {

            // memory to maintain unvisited children

            List<Node> queue = new List<Node>();

            // iterate over all nodes (breadth first search)

            queue.Add(tree.GetTreeA());

            int currentLevelCount = 1;

            int nextLevelCount = 0;

            int currentLevel = 1;

            Console.WriteLine("First Level: " + currentLevel);

            Node currentNode = null;

            // counter to begin iterating over newly added children to instantiate display nodes

            int displayIterBegin = 1;

            // counter to end iterating over newly added children to instantiate display nodes

            int displayIterEnd = 1;

            // test if display tree has been instantiated

            if (displayTree == null)
            {

                displayTree = new DisplayTree(width, height, nodeSize);

            }

            DisplayNode currentParentDisplayNode = new DisplayNode();

            // add the root display node

            displayTree.AddDisplayNode(queue.ElementAt(0), null, currentLevel, 1, nextLevelCount, currentParentDisplayNode);

            List<DisplayNode> childDisplayNodes = new List<DisplayNode>();

            List<DisplayNode> parentDisplayNodes = new List<DisplayNode>();

            parentDisplayNodes.Add(currentParentDisplayNode);

            List<Node> childNodes = new List<Node>();

            while (queue.Count > 0)
            {

                currentNode = queue.ElementAt(0);

                queue.RemoveAt(0);

                // get current parent display node

                currentParentDisplayNode = parentDisplayNodes.ElementAt(0);

                parentDisplayNodes.RemoveAt(0);

                Console.WriteLine("Current Node Str: " + currentNode.getNodeStr());

                Node currentNodeChild = null;

                while ((currentNodeChild = currentNode.getNextChild()) != null){

                    queue.Add(currentNodeChild);

                    nextLevelCount++;

                    displayIterEnd++;

                    DisplayNode displayNode = new DisplayNode();

                    displayNode.SetParent(currentParentDisplayNode);

                    childDisplayNodes.Add(displayNode);

                    childNodes.Add(currentNodeChild);

                }

                currentLevelCount--;

                if(queue.Count > 0)
                {

                    if (currentLevelCount == 0)
                    {

                        parentDisplayNodes.Clear();

                        currentLevel++;

                        for (int levelCounter = 0; levelCounter < childDisplayNodes.Count; levelCounter++)
                        {

                            // add the display node

                            displayTree.AddDisplayNode(childNodes.ElementAt(levelCounter), childDisplayNodes.ElementAt(levelCounter).GetParent(), currentLevel, nextLevelCount, levelCounter, childDisplayNodes.ElementAt(levelCounter));

                            parentDisplayNodes.Add(childDisplayNodes.ElementAt(levelCounter));

                        }

                        Console.WriteLine("Level has incremented! " + currentLevel);

                        currentLevelCount = nextLevelCount;

                        nextLevelCount = 0;

                        childDisplayNodes.Clear();

                        childNodes.Clear();

                    }

                }

            }

            Console.WriteLine("Final Current Level: " + currentLevel);

            displayTree.CalculateDisplayTreeYValues(currentLevel);

            return displayTree;

        }

    }
}
