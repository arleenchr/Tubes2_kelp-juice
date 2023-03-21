namespace Solver
{
    public class Solver
    {
        protected static int[] dx = { 1, -1, 0, 0 };
        protected static int[] dy = { 0, 0, 1, -1 };
        protected static int[] reverse_dx = { -1, 1, 0, 0 };
        protected static int[] reverse_dy = { 0, 0, -1, 1 };
        protected static char[] direction = { 'R', 'L', 'D', 'U' };
        protected static char[] reverseDirection = { 'L', 'R', 'U', 'D' };
        protected static int cntNode;
        protected static string solution = default!;
        protected static bool allTreasureFound;
        protected static bool[,] visited = default!;
        protected static Map map = default!;
    }
}
