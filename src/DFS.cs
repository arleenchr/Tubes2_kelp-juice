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

        public static void CallDFS(Map _map, ref string _solution, ref int _cntNode, ref List<Point> _pathPoints, ref long timeExec)
        {
            cntNode = 0;
            allTreasureFound = false;
            solution = "";
            map = _map;
            numOfTreasure = map.numOfTreasure;
            List<Point> pathPoints = new List<Point>() { new Point(map.start.rowId, map.start.colId) };
            visited = new bool[map.rows, map.cols];

            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            DFS(map.start.rowId, map.start.colId);
            watch.Stop();

            foreach (char direction in solution)
            {
                switch (direction)
                {
                    case 'R':
                        pathPoints.Add(new Point(pathPoints.Last().rowId + 0, pathPoints.Last().colId + 1));
                        break;
                    case 'L':
                        pathPoints.Add(new Point(pathPoints.Last().rowId + 0, pathPoints.Last().colId - 1));
                        break;
                    case 'D':
                        pathPoints.Add(new Point(pathPoints.Last().rowId + 1, pathPoints.Last().colId + 0));
                        break;
                    case 'U':
                        pathPoints.Add(new Point(pathPoints.Last().rowId - 1, pathPoints.Last().colId + 0));
                        break;
                    default:
                        Console.WriteLine("Direction undefined!");
                        break;
                }
            }

            _solution = solution;
            _cntNode = cntNode;
            _pathPoints = pathPoints;
            timeExec = watch.ElapsedMilliseconds;
        }
    }
}
