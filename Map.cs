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
        public char[,] matrix { get; set; } // map matrix
        public int rows { get; set; } // row count
        public int cols { get; set; } // column count
        public int startRow { get; set; } // starting point (row)
        public int startCol { get; set; } // starting point (col)
        public int numOfTreasure { get; set; } // treasure count
        public ArrayList treasurePosition/* = default!*/{ get; set; } // ArrayList of List<int>(){x,y}

        /* methods */
        /* constructor */
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

        /* other methods */
        public bool checkTreasure(int row, int col)
        {
            List<int> check = new List<int>(){row,col};
            return treasurePosition.Contains(check);
        }
    }
}
