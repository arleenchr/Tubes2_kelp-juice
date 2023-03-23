using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver
{
    public class Point
    {
        public int rowId { get; set; } // row
        public int colId { get; set; } // col

        /* constructor */
        public Point(int _rowId, int _colId)
        {
            rowId = _rowId;
            colId = _colId;
        }
    }
    public class PointDir : Point
    {
        public int direction { get; set; }

        /* constructor */
        public PointDir(int _rowId, int _colId, int _direction) : base(_rowId, _colId)
        {
            direction = _direction;
        }

        /* operators */
        public override bool Equals(object? p)
        {
            if (p is PointDir)
            {
                PointDir p2 = (PointDir)p;
                return p2.rowId == rowId && p2.colId == colId && p2.direction == direction;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return rowId ^ colId ^ direction;
        }
    }
}
