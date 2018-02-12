using System;
using System.Collections.Generic;
using System.IO;

namespace CSCI280_MazeRunner_Schmidt
{

    /// <summary>
    /// This provides the .txt maze reading programming and creating the VectorPoints to give to the not-binery-tree.
    /// To run, give the address and it will pull as args[0] and run that text document.
    /// Input:
    ///     Text document address with a maze in 1's and 0's
    ///     Example:
    ///         00001000
    ///         00001000
    ///         00001100
    ///         00000100
    ///     Any size and any rectangular shape should work. It does handle loops in the maze as well.
    ///     Example:
    ///         0001000
    ///         0001110
    ///         0001010
    ///         0001110
    ///         0001000
    ///     Entrance and exit must be on the outside of the rectangle, but it can be anywhere on the outside. They could even be on the same side. If there is more than two 1's (for exit and entrance
    ///         on the outside border then it will only register two of them as exit and entrance.
    ///     
    /// Output: 
    ///     The maze path will be written into the console, giving each VectorPoint.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {


            string mazePath = "C:\\Users\\brett\\Documents\\TestingTextFiles\\Maze1.txt";
            try
            {
                if (args[0] != null)
                {
                    mazePath = args[0];
                }
            }
            catch(Exception E)
            {

            }

            int[,] mazeDiagram = MazeReader(mazePath);


            VectorPoint[] vectors = GetStartandEnd(AllVectors(mazeDiagram), mazeDiagram);



            NotBinerySearchTree myTree = new NotBinerySearchTree(mazeDiagram, AllVectors(mazeDiagram), GetStartandEnd(AllVectors(mazeDiagram), mazeDiagram)[0], GetStartandEnd(AllVectors(mazeDiagram), mazeDiagram)[1]);
            Console.WriteLine(myTree.findMazePath());

            Console.ReadKey();
        }

        /// <summary>
        /// Originally used to move around the maze. Not used in current iteration.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="dir"></param>
        /// <returns></returns>
        
        static int[] MoveByPath(int x, int y, int dir)
        {
            switch (dir)
            {
                case 1:
                    return new int[] { x + 1, y, dir };
                case 2:
                    return new int[] { x - 1, y, dir };
                case 3:
                    return new int[] { x, y + 1, dir };
                case 4:
                    return new int[] { x, y - 1, dir };
                default:
                    return new int[] { x, y, dir };
            }
        }


        /// <summary>
        /// Method to find all the possible VectorPoint coordinates in the maze.
        /// </summary>
        /// <param name="curMaze"></param>
        /// <returns></returns>
        static VectorPoint[] AllVectors(int[,] curMaze)
        {
            List<VectorPoint> allVectors = new List<VectorPoint>();
            for(int i = 0; i < curMaze.GetLength(0); i++)
            {
                for(int j = 0; j < curMaze.GetLength(1); j++)
                {
                    if (curMaze[i, j] == 1)
                    {
                        foreach (VectorPoint p in GetOptions(i, j, curMaze))
                        {
                            allVectors.Add(p);
                        }
                    }
                }
            }
            return allVectors.ToArray();
        }

        /// <summary>
        /// Obtains the start and end VectorPoints by going through every VectorPoint and checking if it is on the outside of the border.
        /// It will store more than 2 exits/entrances if they exist, but the program only uses the first two.
        /// </summary>
        /// <param name="vectors"></param>
        /// <param name="curMaze"></param>
        /// <returns></returns>

        static VectorPoint[] GetStartandEnd(VectorPoint[] vectors, int[,] curMaze)
        {
            List<VectorPoint> vects = new List<VectorPoint>();
            foreach(VectorPoint p in vectors)
            {
                if(p.IsStartOrEnd(curMaze))
                {
                    vects.Add(p);
                }
            }

            return vects.ToArray();
        }

        /// <summary>
        /// Reads all the spaces around a given vector point and returns this point's VectorPoints in the form of an array. 
        /// It gives all the directions to any nearby points.
        /// Method to get all the possible VectorPoint directions from the maze.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="curMaze"></param>
        /// <returns></returns>

        static VectorPoint[] GetOptions(int x, int y, int[,] curMaze)
        {

            List<VectorPoint> vectors = new List<VectorPoint>();
            if (curMaze[x, y] == 1)
            {
                try
                {
                    if (curMaze[x, y - 1] == 1)
                    {
                        vectors.Add(new VectorPoint(x, y, 3));

                    }
                }
                catch (Exception E)
                {

                }
                try
                {
                    if (curMaze[x, y + 1] == 1)
                    {
                        vectors.Add(new VectorPoint(x, y, 4));
                    }

                }
                catch (Exception E)
                {

                }
                try
                {
                    if (curMaze[x - 1, y] == 1)
                    {
                        vectors.Add(new VectorPoint(x, y, 1));
                    }
                }
                catch (Exception E)
                {

                }
                try
                {
                    if (curMaze[x + 1, y] == 1)
                    {
                        vectors.Add(new VectorPoint(x, y, 2));
                    }
                }
                catch (Exception E)
                {

                }
            }
            return vectors.ToArray();



        }

        static int[,] MazeReader(string filePath)
        {

            StreamReader sr = new StreamReader(filePath);
            LinkedList<string> srList = new LinkedList<string>();



            using (sr)
            {
                while (!sr.EndOfStream)
                {
                    srList.AddLast(sr.ReadLine());
                }
            }

            int[,] mazeDiagram = new int[ srList.Count, srList.First.Value.Length];

            int counter = 0;
            int counter2 = 0;

            foreach (string c in srList)
            {
                foreach (char thisChar in c)
                {
                    mazeDiagram[counter, counter2] = (int)Char.GetNumericValue(thisChar);
                    Console.Write(thisChar);
                    counter2++;
                }
                counter2 = 0;
                Console.WriteLine("");
                counter++;
            }



            return mazeDiagram;

        }
    }
}
    
