using System;
using System.Collections.Generic;
using System.Text;

namespace CSCI280_MazeRunner_Schmidt
{

    /// <summary>
    /// 3 dimensional vector that holds the direction, x, and y axis coordinates.
    /// Since the maze is viewed upside down, the direction coorsponds to if it were right-side up. This caused many issues and is confusing.
    /// </summary>
    
        public class VectorPoint
        {
            public int m_x { get; set; }
            public int m_y { get; set; }

            // 1 = North, 2 = South, 3 = Right, 4 = Left, 5 = end
            public int m_Direction { get; set; }

            public VectorPoint()
            {
                m_x = 0;
                m_y = 0;
                m_Direction = 0;
            }

            public VectorPoint(int x, int y, int dir)
            {
                m_x = x;
                m_y = y;
                m_Direction = dir;
            }
        override
            public string ToString()
        {
            return string.Format("({0}, {1}) {2}", this.m_y, this.m_x, this.getDirection(m_Direction));
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
        public bool IsStartOrEnd(int[,] curMaze)
            {
            
                if (m_x == 0 || m_y == 0 || m_x == curMaze.GetLength(0)-1 || m_y == curMaze.GetLength(1)-1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public bool CheckCoords(int x, int y)
            {
                if (m_x == x && m_y == y)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public bool CompareTo(VectorPoint pointToCompare)
            {
                if (this.m_x == pointToCompare.m_x && this.m_y == pointToCompare.m_y && this.m_Direction == pointToCompare.m_Direction)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }



        }
    }
