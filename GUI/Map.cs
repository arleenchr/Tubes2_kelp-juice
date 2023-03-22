namespace Solver
{
    public class Map
    {
        /* attributes */
        public char[,] grid { get; set; } // map matrix
        public int rows { get; set; } // row count
        public int cols { get; set; } // column count
        public int startRow { get; set; } // starting point (row)
        public int startCol { get; set; } // starting point (col)
        public int numOfTreasure { get; set; } // treasure count

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
                        startRow = i;
                        startCol = j;
                    }
                    else if (grid[i, j] == 'T')
                    {
                        numOfTreasure++;
                    }
                }
            }
        }
    }
}