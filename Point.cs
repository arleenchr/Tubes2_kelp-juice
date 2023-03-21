using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver
{
    public class Point
    {
        public int x { get; set; }
        public int y { get; set; }

        /* constructor */
        public Point(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
    }
    public class PointDir : Point
    {
        public int direction { get; set; }

        /* constructor */
        public PointDir(int _x, int _y, int _direction) : base(_x,_y)
        {
            direction = _direction;
        }

        /* operators */
        public override bool Equals(object p)
        {
            if (p is PointDir)
            {
                PointDir p2 = (PointDir)p;
                return p2.x == x && p2.y == y && p2.direction == direction;
            }
            return false;
        }
    }
}
