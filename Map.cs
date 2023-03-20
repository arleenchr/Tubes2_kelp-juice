using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tubes2_kelp_juice
{
    public class Map
    {
        /* attributes */
        private char[,] matrix;
        private int rows;
        private int cols;
        private int numOfTreasure;

        /* methods */
        /* getter */
        public char[,] getMatrix()
        {
            return matrix;
        }
        public int getRows()
        {
            return rows;
        }
        public int getCols()
        {
            return cols;
        }
        public int getNumOfTreasure()
        {
            return numOfTreasure;
        }

        /* setter */
        public void setMatrix(char[,] _matrix)
        {
            matrix = _matrix;
        }
        public void setRows(int _rows)
        {
            rows = _rows;
        }
        public void setCols(int _cols)
        {
            cols = _cols;
        }
        public void setNumOfTreasure(int _numOfTreasure)
        {
            numOfTreasure = _numOfTreasure;
        }
    }
}
