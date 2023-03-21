namespace Solver
{
    public class DFSSolver : Solver
    {
        static void DFS(int row, int col)
        {
            cntNode++; // Node check
            visited[row, col] = true;

            if (map.grid[row, col] == 'T') // Found treasure
                numOfTreasure--;
            if (numOfTreasure == 0) // All treasure found!
            {
                allTreasureFound = true;
                return;
            }

            for (int i = 0; i < 4; i++)
            {
                int newRow = row + dy[i];
                int newCol = col + dx[i];
                if (newRow < 0 || newRow >= map.rows || newCol < 0 || newCol >= map.cols ||
                    visited[newRow, newCol] || map.grid[newRow, newCol] == 'X' || allTreasureFound)
                    continue;

                solution += direction[i];
                DFS(newRow, newCol);

                if (!allTreasureFound) // Backtracking
                    solution += reverseDirection[i];
            }
        }

        public static void callDFS(Map _map, ref string _solution, ref int _cntNode, ref long timeExec)
        {
            cntNode = 0;
            allTreasureFound = false;
            solution = "";
            map = _map;
            numOfTreasure = map.numOfTreasure;

            visited = new bool[map.rows, map.cols];

            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            DFS(map.startRow, map.startCol);
            watch.Stop();

            _solution = solution;
            _cntNode = cntNode;
            timeExec = watch.ElapsedMilliseconds;
        }
    }
}
