namespace Solver
{
    public class DFSSolver : Solver
    {
        static void DFS(int row, int col)
        {
            cntNode++; // Node check
            visited[row, col] = true;

            if (grid[row, col] == 'T') // Found treasure
                cntTreasure--;
            if (cntTreasure == 0) // All treasure found!
            {
                allTreasureFound = true;
                return;
            }

            for (int i = 0; i < 4; i++)
            {
                int newRow = row + dy[i];
                int newCol = col + dx[i];
                if (newRow < 0 || newRow >= MX_ROW || newCol < 0 || newCol >= MX_COL ||
                    visited[newRow, newCol] || grid[newRow, newCol] == 'X' || allTreasureFound)
                    continue;

                solution += direction[i];
                DFS(newRow, newCol);

                if (!allTreasureFound) // Backtracking
                    solution += reverseDirection[i];
            }
        }

        public static void callDFS(Map map, ref string sol, ref int numNode, ref long timeExec)
        {
            cntNode = 0;
            cntTreasure = map.getNumOfTreasure();
            allTreasureFound = false;
            solution = "";

            MX_ROW = map.getRows();
            MX_COL = map.getCols();
            visited = new bool[MX_ROW, MX_COL];
            grid = new char[MX_ROW, MX_COL];

            for (int i = 0; i < MX_ROW; i++)
            {
                for (int j = 0; j < MX_COL; j++)
                {
                    grid[i, j] = map.getMatrix()[i, j];
                }
            }

            ST_ROW = map.getStartRow();
            ST_COL = map.getStartCol();
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            DFS(ST_ROW, ST_COL);
            watch.Stop();

            sol = solution;
            numNode = cntNode;
            timeExec = watch.ElapsedMilliseconds;
        }
    }
}
