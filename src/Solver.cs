namespace Solver
{
    public class Solver
    {
        /* attributes */
        protected static int[] dx = { 1, -1, 0, 0 }; // column difference (to evaluate nodes in the right, left, down, and up)
        protected static int[] dy = { 0, 0, 1, -1 }; // row difference (to evaluate nodes in the right, left, down, and up)
        protected static int[] reverse_dx = { -1, 1, 0, 0 }; // column difference reversed (to evaluate nodes in the left, right, up, and down)
        protected static int[] reverse_dy = { 0, 0, -1, 1 }; // row difference reversed (to evaluate nodes in the left, right, up, and down)
        protected static char[] direction = { 'R', 'L', 'D', 'U' }; // direction priority (RLDU)
        protected static char[] reverseDirection = { 'L', 'R', 'U', 'D' }; // reversed direction priority (LRUD)
        protected static int cntNode; // number of nodes evaluated
        protected static int numOfTreasure; // number of treasures left in the map
        protected static string solution = default!; // solution path
        protected static bool allTreasureFound; // check if all treasure in the map has been found
        protected static bool[,] visited = default!; // node status (true = visited, false = not visited)
        protected static Map map = default!; // map info
    }
}
