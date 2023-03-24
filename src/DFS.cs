using System;
using System.Collections.Generic;
using System.Linq;

namespace Solver
{
    public class DFSSolver : Solver
    {
        static void DFS(int row, int col)
        {
            // Node check
            cntNode++; 
            visited[row, col] = true;

            // Found treasure
            if (map.grid[row, col] == 'T')
                numOfTreasure--;
            
            // All treasure found!
            if (numOfTreasure == 0)
            {
                allTreasureFound = true;
                return;
            }

            // All possible path to construct
            for (int i = 0; i < 4; i++) 
            {
                int newRow = row + dy[i];
                int newCol = col + dx[i];
                if (newRow < 0 || newRow >= map.rows || newCol < 0 || newCol >= map.cols ||
                    visited[newRow, newCol] || map.grid[newRow, newCol] == 'X' || allTreasureFound)
                    continue;

                // Recursive searching
                solution += direction[i];
                DFS(newRow, newCol);

                // Backtracking
                if (!allTreasureFound)
                    solution += reverseDirection[i];
            }
        }
        public static void CallDFS(Map _map, ref string _solution, ref int _cntNode, ref List<Point> _pathPoints, ref long timeExec)
        {
            // Initiate
            cntNode = 0;
            allTreasureFound = false;
            solution = "";
            map = _map;
            numOfTreasure = map.numOfTreasure;
            List<Point> pathPoints = new List<Point>() { new Point(map.start.rowId, map.start.colId) };
            visited = new bool[map.rows, map.cols];

            // Time execution
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            DFS(map.start.rowId, map.start.colId);
            watch.Stop();

            // Construct pathPoints from the solution path
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
