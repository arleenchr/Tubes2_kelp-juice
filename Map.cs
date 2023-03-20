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
        private char[,] matrix = default!; // map matrix
        private int rows; // row count
        private int cols; // column count
        private int startRow; // starting point (row)
        private int startCol; // starting point (col)
        private int numOfTreasure; // treasure count
        private ArrayList treasurePosition = default!; // ArrayList of List<int>(){x,y}

        /* methods */
        /* constructor */
        public Map()
        {
            for (int i=0; i<numOfTreasure; i++)
            {
                treasurePosition = new ArrayList();
            }
        }
        public Map(char[,] _matrix, int _rows, int _cols, int _startRow, int _startCol, int _numOfTreasure, ArrayList _treasurePosition)
        {
            matrix = _matrix;
            rows = _rows;
            cols = _cols;
            startRow = _startRow;
            startCol = _startCol;
            numOfTreasure = _numOfTreasure;
            treasurePosition = _treasurePosition;
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
        public int getStartRow()
        {
            return startRow;
        }
        public int getStartCol()
        {
            return startCol;
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
        public void setStartRow(int _startRow)
        {
            startRow = _startRow;
        }
        public void setStartCol(int _startCol)
        {
            startCol = _startCol;
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
