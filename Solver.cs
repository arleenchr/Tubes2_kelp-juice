namespace Solver
{
    public class Solver
    {
        protected static int[] dx = { 1, -1, 0, 0 };
        protected static int[] dy = { 0, 0, 1, -1 };
        protected static int[] reversedx = { -1, 1, 0, 0 };
        protected static int[] reversedy = { 0, 0, -1, 1 };
        protected static char[] direction = { 'R', 'L', 'D', 'U' };
        protected static char[] reverseDirection = { 'L', 'R', 'U', 'D' };
        protected static int MX_ROW, MX_COL, ST_ROW, ST_COL, cntNode, cntTreasure;
        protected static string? solution;
        protected static bool allTreasureFound;
        protected static bool[,] visited = default!;
        protected static char[,] grid = default!;
    }
}
