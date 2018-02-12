using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCI280_MazeRunner_Schmidt
{

    /// <summary>
    /// Class used to provide a tree structure that is not binary. 
    /// Algorithm:
    ///     Give it a 2D array of the maze in 1's and 0's (1 representing the path, 0 representing not the path)
    ///     an array of all the VectorPoints (even duplicates), the startpoint and the end point VectorPoint objects.
    ///     It will add the startPoint as its RootNode.
    ///     4. For every node:
    ///         For every VectorPoint in VectorPoint array:
    ///             Checks against the current VectorPoint in the node to see if its direction points back to the current node, if yes, adds to delete queue.
    ///             Checks if the current VectorPoint points to the next VectorPoint, if yes adds as child.
    ///     For every child node, go to 4.
    ///     Not-Binery-Tree-Structure is now made.
    ///     
    ///     The nodes keep track of the parent node as well, so to find the maze path the End node is found, and then the parent path back to the root node is found and the the VectorPoints are shown.
    ///     
    /// </summary>

    public class NotBinerySearchTree
    {
        private int m_Size;

        private Node m_RootNode;
        private int[,] m_Maze;
        private VectorPoint[] vectors;
        private VectorPoint m_Start;
        private VectorPoint m_End;
        private LinkedList<string> m_mazePath;
        private Node m_EndNode;

        private class Node
        {
            private int m_Key;
            private Object m_Obj;
            private Node rightNode;
            private Node leftNode;
            private int m_Size;
            private Node ParentNode;
            private LinkedList<Node> childNodes;

            public Node(int key, Object obj, int size, Node parentNode)
            {
                this.m_Key = key;
                this.m_Obj = obj;
                this.m_Size = size;
                childNodes = new LinkedList<Node>();
                ParentNode = parentNode;
            }

            public Node getParent()
            {
                return ParentNode;
            }
            

            public VectorPoint getObj(int i)
            {
                
                return (VectorPoint)m_Obj;
            }

            public Object getObj()
            {
                return m_Obj;
            }

            public int getKey()
            {
                return m_Key;
            }

            public Node getLeftNode()
            {
                return leftNode;
            }

            public void addChildNode(Node nodeNode)
            {
                childNodes.AddLast(nodeNode);
            }

            public LinkedList<Node> getChildren()
            {
                return childNodes;
            }

            public Node getRightNode()
            {
                return rightNode;
            }
            public void setLeftNode(Node LeftNode)
            {
                leftNode = LeftNode;
            }
            public void setRightNode(Node RightNode)
            {
                rightNode = RightNode;
            }
        }

        public NotBinerySearchTree()
        {
            m_Size = 0;
        }

        public NotBinerySearchTree(int[,] curMaze, VectorPoint[] vectors, VectorPoint startPoint, VectorPoint endPoint)
        {
            m_Size = 0;
            m_Maze = curMaze;
            this.addNode((startPoint.m_x * 100 + startPoint.m_y) * startPoint.m_Direction, startPoint);
            List<VectorPoint> listVector = vectors.ToList<VectorPoint>();
            endPoint.m_Direction = 5;
            //m_EndNode = new Node((100 * endPoint.m_x + endPoint.m_y) * endPoint.m_Direction, endPoint, 50, m_RootNode);
            listVector.Add(endPoint);
            



            addVectorNode( listVector);
           
            
        }

        

        
        /// <summary>
        /// Garbage code
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="curMaze"></param>
        /// <returns></returns>
        static VectorPoint[] GetOptions(int x, int y, int[,] curMaze)
        {

            List<VectorPoint> vectors = new List<VectorPoint>();

            if (curMaze[x, y + 1] == 1)
            {
                vectors.Add(new VectorPoint(x, y, 3));

            }
            if (curMaze[x, y - 1] == 1)
            {
                vectors.Add(new VectorPoint(x, y, 4));
            }
            if (curMaze[x + 1, y] == 1)
            {
                vectors.Add(new VectorPoint(x, y, 1));
            }
            if (curMaze[x - 1, y] == 1)
            {
                vectors.Add(new VectorPoint(x, y, 2));
            }
            return vectors.ToArray();



        }

        /// <summary>
        /// Returns size of the tree structure (how many nodes are in it)
        /// </summary>
        /// <returns></returns>
        public int getSize()
        {
            return m_Size;
        }

        /// <summary>
        /// Binary tree method
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        public void addNode(int key, Object obj)
        {
            if (key == 0 || obj == null)
            {
                return;
            }
            m_RootNode = addNode(m_RootNode, key, obj);
            m_Size++;
        }

        private Node addNode(Node node, int key, Object obj)
        {
            if (node == null)
            {
                return new Node(key, obj, 1, m_RootNode);
            }
            if(node.getKey() == key)
            {
                node.addChildNode(new Node(key, obj, m_Size , m_RootNode));
                
            }
            else
            {
                foreach(Node nodes in node.getChildren())
                {
                    addNode(nodes, key, obj);
                }
            }
            return node;
        }

        public string getDirection(int dir)
        {
            switch (dir)
            {
                case 1:
                    return "North";
                case 2:
                    return "Down";
                case 3:
                    return "Right";
                case 4:
                    return "Left";
                case 5:
                    return "End";
                default:
                    return "Not valid";
            }

        }

        public Object findNode(int key)
        {
            Node foundObject = findNode(m_RootNode, key);
            if (foundObject != null)
            {
                Console.WriteLine("Found Node with char: " + foundObject.getObj().ToString() + "; and a key of: " + foundObject.getKey());
                return foundObject.getObj();
            }
            else
            {
                Console.WriteLine("Nothing found.");
                return null;
            }

        }



        public string findMazePath()
        {
            m_mazePath = new LinkedList<string>();
            // findMazePath(findEndNode);
            findMazePath(m_EndNode);
            string mazePath = "";

            foreach (string s in m_mazePath)
            {
                mazePath += s +", "  ;
            }

            return mazePath;
        }

        /// <summary>
        /// Rand doesn't matter, this is to not use the binary structure.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="key"></param>
        /// <param name="rand"></param>
        /// <returns></returns>
        /// 


        private void findMazePath(Node node)
        {
            findEndNode();
            
            if(node.getKey() == m_EndNode.getKey())
            {
                m_mazePath.AddLast("End");
                findMazePath(node.getParent());
            }
            else if(node.getParent() != null)
            {
                m_mazePath.AddFirst(node.getObj(2).ToString());
                findMazePath(node.getParent());
            }
            else
            {
                m_mazePath.AddFirst("Start: " + node.getObj(2).ToString());
            }
        }

        

        private Node findEndNode()
        {
            findEndNode(m_RootNode);
            return m_EndNode;
        }

        private void findEndNode(Node node)
        {
            if(node.getObj(2).m_Direction == 5)
            {
                m_EndNode = node;
            }
            
            else if(node.getChildren() != null)
            {
                foreach(Node nodes in node.getChildren())
                {
                    findEndNode(nodes);
                }
                
            }
            

        }

        private Node findNode(Node node, int key)
        {
            Console.WriteLine("Looking at Node: " + node.getObj().ToString());
            if (node.getKey() == key)
            {
                return node;
            }
            else if (node.getKey() > key)
            {
                if (node.getLeftNode() == null)
                {
                    return null;
                }
                return findNode(node.getLeftNode(), key);
            }
            else if (node.getKey() < key)
            {
                if (node.getRightNode() == null)
                {
                    return null;
                }
                return findNode(node.getRightNode(), key);
            }
            else
            {
                return null;
            }

        }

        public void printPreorder()
        {
            Console.Write("Preorder: ");
            //Console.Write(m_RootNode.getObj().ToString() + ", ");
            // printPreorder(m_RootNode.getLeftNode());
            // printPreorder(m_RootNode.getRightNode());
            printPreorder(m_RootNode);
        }

        private void printPreorder(Node node)
        {

            if (node != null)
            {
                Console.Write(node.getObj().ToString() + ", ");

                if (node.getLeftNode() != null)
                {
                    printPreorder(node.getLeftNode());
                }
                if (node.getRightNode() != null)
                {
                    printPreorder(node.getRightNode());
                }
            }
        }

        public void printInorder(int i)
        {
            Console.Write("Inorder: ");
            printInorder(m_RootNode, 2);
        }
        private void printInorder(Node node, int o)
        {
            if(node != null)
            {
                if(node.getChildren() != null)
                {
                    Console.WriteLine(node.getObj().ToString() + ", ");
                    foreach(Node nodes in node.getChildren())
                    {
                        printInorder(nodes, 2);
                    }
                    
                }
            }
        }

        
        public void printInorder()
        {
            Console.Write("Inorder: ");
            printInorder(m_RootNode);
        }

        private void printInorder(Node node)
        {


            if (node != null)
            {
                if (node.getLeftNode() != null)
                {
                    printInorder(node.getLeftNode());
                }
                Console.Write(node.getObj().ToString() + ", ");
                if (node.getRightNode() != null)
                {
                    printInorder(node.getRightNode());
                }
            }
        }

        public void addVectorNode(VectorPoint vector)
        {
           
            this.m_Size = this.m_Size++;
            m_RootNode = AddVectorNode(m_RootNode, vector);

        }

        public void addVectorNode( List<VectorPoint> vector)
        {

            this.m_Size = this.m_Size++;
            m_RootNode = AddVectorNode(m_RootNode, vector.First());
            vector.Remove(vector.First());

            m_RootNode = AddVectorNode(m_RootNode, vector);

        }

        // LESSONS LEARNED: Correctly identifying and labeling the coordinate planes of the array will save hours of work. 

            // x = y and y = x

        private Node AddVectorNode(Node curNode, List<VectorPoint> m_vector)
        {
            if (m_RootNode == null)
            {
                return new Node((m_vector.First().m_x * 100 + m_vector.First().m_y) * m_vector.First().m_Direction, m_vector, this.m_Size + 1, m_RootNode);
            }

            List<VectorPoint> newVectors = new List<VectorPoint>();

            if (curNode != null  && m_RootNode != null)
            {
               
                
                foreach (VectorPoint vectors in m_vector)
                {
                    bool removed = false;

                    switch (vectors.m_Direction)
                    {
                        case 1:
                            if(vectors.m_x - 1 == curNode.getObj(2).m_x && curNode.getObj(2).m_y == vectors.m_y)
                            {

                                newVectors.Add(vectors);
                                removed = true;
                            }
                            break;
                        case 2:
                            if (vectors.m_x + 1 == curNode.getObj(2).m_x && curNode.getObj(2).m_y == vectors.m_y)
                            {
                                newVectors.Add(vectors);
                                removed = true;
                            }
                            break;
                        case 3:
                            if (vectors.m_y + 1 == curNode.getObj(2).m_y && curNode.getObj(2).m_x == vectors.m_x)
                            {
                                newVectors.Add(vectors);
                                removed = true;
                            }
                            break;
                        case 4:
                            if (vectors.m_y - 1 == curNode.getObj(2).m_y && curNode.getObj(2).m_x == vectors.m_x)
                            {
                                newVectors.Add(vectors);
                                removed = true;
                            }
                            break;
                       
                            
                        default:
                            break;
                    }

                    if (!removed)
                    {
                        switch (curNode.getObj(2).m_Direction)
                        {
                            case 1:

                                if (curNode.getObj(2).m_x - 1 == vectors.m_x && curNode.getObj(2).m_y == vectors.m_y)
                                {
                                    Console.WriteLine("Node added: " + vectors.ToString() + " to " + curNode.getObj(2).ToString());
                                    curNode.addChildNode(new Node((vectors.m_x * 100 + vectors.m_y) * vectors.m_Direction, vectors, this.m_Size + 1, curNode));
                                    
                                    newVectors.Add(vectors);

                                }
                                break;

                            case 2:
                                if (curNode.getObj(2).m_x + 1 == vectors.m_x && curNode.getObj(2).m_y == vectors.m_y)
                                {
                                    Console.WriteLine("Node added: " + vectors.ToString() + " to " + curNode.getObj(2).ToString());
                                    curNode.addChildNode(new Node((vectors.m_x * 100 + vectors.m_y) * vectors.m_Direction, vectors, this.m_Size + 1, curNode));
                                   
                                    newVectors.Add(vectors);
                                }
                                break;
                            case 3:
                                if (curNode.getObj(2).m_y + 1 == vectors.m_y && curNode.getObj(2).m_x == vectors.m_x)
                                {
                                    Console.WriteLine("Node added: " + vectors.ToString() + " to " + curNode.getObj(2).ToString());
                                    curNode.addChildNode(new Node((vectors.m_x * 100 + vectors.m_y) * vectors.m_Direction, vectors, this.m_Size + 1, curNode));
                                    
                                    newVectors.Add(vectors);

                                }
                                break;
                            case 4:
                                if (curNode.getObj(2).m_y - 1 == vectors.m_y && curNode.getObj(2).m_x == vectors.m_x)
                                {
                                    Console.WriteLine("Node added: " + vectors.ToString() + " to " + curNode.getObj(2).ToString());
                                    curNode.addChildNode(new Node((vectors.m_x * 100 + vectors.m_y) * vectors.m_Direction, vectors, this.m_Size + 1, curNode));
                                    
                                    newVectors.Add(vectors);

                                }
                                break;
                            
                            default:
                                break;

                        }
                    }
                }


                VectorPoint[] points = newVectors.ToArray();
                newVectors = m_vector;

                foreach (VectorPoint deleteVectors in points)
                {
                    Console.WriteLine("Vector Removed: " + deleteVectors.ToString());
                    newVectors.Remove(deleteVectors);
                }

              

                if (curNode.getChildren() != null )
                {
                    foreach (Node nodes in curNode.getChildren())
                    {
                        if(nodes.getObj(2).m_Direction == 5)
                        {
                            m_EndNode = nodes;
                        }
                        AddVectorNode(nodes, newVectors);
                    }
                }
                else
                {
                    return m_RootNode;
                }

            }
            else
            {
                return m_RootNode;
            }
            return m_RootNode;
        }


        private Node AddVectorNode(Node curNode, VectorPoint vector)
        {
            if(m_RootNode == null )
            {
                return new Node((vector.m_x * 100 + vector.m_y) * vector.m_Direction, vector, this.m_Size + 1, m_RootNode);
            }

            Node addNode = new Node((vector.m_x * 100 + vector.m_y) * vector.m_Direction, vector, this.m_Size + 1, m_RootNode);

            if (curNode != null && addNode != null && m_RootNode != null)
            {
                bool addedVector = false;
                switch (curNode.getObj(2).m_Direction)
                {
                    case 1:
                        if(curNode.getObj(2).m_y - 1 == addNode.getObj(2).m_y)
                        {
                            Console.WriteLine("Node Added");
                            curNode.addChildNode(addNode);
                            addedVector = true;
                            return m_RootNode;
                        }
                        break;

                    case 2:
                        if(curNode.getObj(2).m_y + 1 == addNode.getObj(2).m_y)
                        {
                            Console.WriteLine("Node Added");
                            curNode.addChildNode(addNode);
                            addedVector = true;
                            return m_RootNode;
                        }
                        break;
                    case 3:
                        if(curNode.getObj(2).m_x - 1 == addNode.getObj(2).m_x)
                        {
                            Console.WriteLine("Node Added");
                            curNode.addChildNode(addNode);
                            addedVector = true;
                            return m_RootNode;
                        }
                        break;
                    case 4:
                        if (curNode.getObj(2).m_x + 1 == addNode.getObj(2).m_x)
                        {
                            Console.WriteLine("Node Added");
                            curNode.addChildNode(addNode);
                            addedVector = true;
                            return m_RootNode;
                        }
                        break;
                    default:
                        return m_RootNode;

                }


                

                if(curNode.getChildren() != null && !addedVector)
                {
                    foreach(Node nodes in curNode.getChildren())
                    {
                        return AddVectorNode(nodes, vector);
                    }
                }
                else
                {
                    return m_RootNode;
                }
                
            }
            else
            {
                return m_RootNode;
            }
            return m_RootNode;
        }

        public void printNotBineryInOrder()
        {
            Console.Write("Inorder: ");
            printNotBineryInOrder(m_RootNode);
        }

        private void printNotBineryInOrder(Node node)
        {
            if(node != null)
            {
                if(node.getChildren() != null)
                {
                    foreach(Node nodes in node.getChildren())
                    {
                        printNotBineryInOrder(nodes);
                    }
                }
                Console.Write(node.getObj().ToString() + ", ");
            }
        }

        public void printPostOrder()
        {
            Console.Write("Postorder: ");
            printPostOrder(m_RootNode);
        }

        private void printPostOrder(Node node)
        {


            if (node != null)
            {
                if (node.getLeftNode() != null)
                {
                    printPostOrder(node.getLeftNode());
                }
                if (node.getRightNode() != null)
                {
                    printPostOrder(node.getRightNode());
                }
                Console.Write(node.getObj().ToString() + ", ");
            }

        }




    }


}
