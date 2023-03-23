using System.Collections.Generic;

namespace Solver
{
    public class Map
    {
        /* attributes */
        public char[,] grid { get; set; } // map matrix
        public int rows { get; set; } // row count
        public int cols { get; set; } // column count
        public Point start { get; set; } // starting point (row,col)
        public int numOfTreasure { get; set; } // treasure count
        public List<Point> treasureLoc { get; set; } = new List<Point>(); // treasure location

        /* methods */
        /* constructor */
        public Map(char[,] _grid)
        {
            grid = _grid;
            rows = grid.GetLength(0);
            cols = grid.GetLength(1);
            numOfTreasure = 0;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (grid[i, j] == 'K')
                    {
                        start = new Point(i, j);
                    }
                    else if (grid[i, j] == 'T')
                    {
                        numOfTreasure++;
                        treasureLoc.Add(new Point(i, j));
                    }
                }
            }
        }
    }
}