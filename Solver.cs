namespace Solver
{
    public class Solver
    {
        public static int[] dx = { 1, -1, 0, 0 };
        public static int[] dy = { 0, 0, 1, -1 };
        public static char[] direction = { 'R', 'L', 'D', 'U' };
        public static char[] reverseDirection = { 'L', 'R', 'U', 'D' };
        public static int MX_ROW, MX_COL, cntNode, cntTreasure;
        public static string? solution;
        public static bool allTreasureFound;
        public static bool[,] visited;
        public static char[,] grid;
    }
}
