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
        private Point<int, int>[] treasurePosition;

        /* methods */
        /* constructor */
        public Map()
        {
            for (int i=0; i<numOfTreasure; i++)
            {
                treasurePosition[i] = new Tuple<int, int>();
            }
        }

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
        public Point<int, int>[] getTreasurePosition()
        {
            return treasurePosition;
        }
        public Point<int,int> getTreasurePositionIdx(int idx)
        {
            return treasurePosition[idx];
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
        public void setTreasurePosition(Point<int, int>[] _treasurePosition)
        {
            treasurePosition = _treasurePosition;
        }

        /* other methods */
        public bool checkTreasure(int row, int col)
        {
            bool found;
            int i = 0;
            while (!found)
            {
                if (treasurePosition[i].Item1 == row && treasurePosition[i].Item2 == col)
                {
                    found = true;
                } else
                {
                    i++;
                }
            }
            return found;
        }
    }
}
