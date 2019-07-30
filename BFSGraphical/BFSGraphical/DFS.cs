using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace BFSGraphical
{
    class DFS
    {

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

        public void runSearch1()
        {

            NodeTree search1Tree = new NodeTree();

            search(search1Tree, "Level 2 Child 2");

        }

        class Stack
        {

            List<Node> store = new List<Node>();

            public Node pop()
            {

                if (store.Count > 0)
                {

                    Node returnNode = store.ElementAt(0);

                    store.RemoveAt(0);

                    return returnNode;

                }
                else
                {

                    return null;

                }

            }

            public void push(Node element)
            {

                store.Insert(0, element);

            }

        }


        public Node search(NodeTree tree, String targetString)
        {

            // memory to maintain unvisited children

            Stack queue = new Stack();

            // dfs

            // read first child each node

            // if has first child

            //      store node

            //      current node = child node

            //  else

            //      print node data

            //      pop next node off stack

            queue.push(tree.GetTreeA());

            Node currentNode = null;

            while((currentNode = queue.pop()) != null)
            {

                Node nextNode = null;

                while((nextNode = currentNode.getNextChild()) != null)
                {

                    if (nextNode != null)
                    {

                        queue.push(currentNode);

                        currentNode = nextNode;

                    }
                    else
                    {

                        Console.WriteLine("Node Data: " + currentNode.getNodeStr());

                    }

                }

                Console.WriteLine("Node Data: " + currentNode.getNodeStr());

            }

            return null;

        }

    }
}
