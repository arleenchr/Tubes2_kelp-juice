using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver
{
    public class Map
    {
        /* attributes */
        private char[,] matrix; // map matrix
        private int rows; // row count
        private int cols; // column count
        private int numOfTreasure; // treasure count
        private ArrayList treasurePosition; // ArrayList of List<int>(){x,y}

        /* methods */
        /* constructor */
        public Map()
        {
            for (int i=0; i<numOfTreasure; i++)
            {
                treasurePosition = new ArrayList();
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
        public ArrayList getTreasurePosition()
        {
            return treasurePosition;
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
        public void setTreasurePosition(ArrayList _treasurePosition)
        {
            treasurePosition = _treasurePosition;
        }

        /* other methods */
        public bool checkTreasure(int row, int col)
        {
            List<int> check = new List<int>(){row,col};
            return treasurePosition.Contains(check);
        }
    }
}
