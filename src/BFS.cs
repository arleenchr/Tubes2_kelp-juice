using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver
{
    public class BFSSolver : Solver
    {
        public static void BFS(Map map, ref string sol, ref int num_node, ref List<Point> pathPoints, ref long timeExec)
        {
            string solution = ""; // solution path
            allTreasureFound = false;
            numOfTreasure = map.numOfTreasure;

            visited = new bool[map.rows, map.cols]; // is-visited info

            Queue<Point> queue = new Queue<Point> { }; // queue of to-be-visited nodes (queue of Point)

            List<PointDir> pathPointDir = new List<PointDir>() { }; // list of PointDir

            List<Point> treasurePicked = new List<Point>() { }; // list of treasure picked 

            // start timer
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            // set current position
            int currentRow = map.start.rowId;
            int currentCol = map.start.colId;
            int tempStartRow = currentRow;
            int tempStartCol = currentCol;
            queue.Enqueue(new Point(currentRow, currentCol)); // add first position to queue
            pathPointDir.Add(new PointDir(currentRow, currentCol, -1)); // add first point to solution (x,y,direction index=-1)

            while (!allTreasureFound && queue.Count > 0)
            {
                visited[currentRow, currentCol] = true; // visited
                queue.Dequeue(); // dequeue

                if (numOfTreasure == 0)
                {
                    allTreasureFound = true; // all treasure found
                    break;
                }

                // visit node (direction priority: RLDU)
                for (int i = 0; i < 4; i++)
                {
                    int newRow = currentRow + dy[i];
                    int newCol = currentCol + dx[i];
                    if (newRow < 0 || newRow >= map.rows || newCol < 0 || newCol >= map.cols ||
                        visited[newRow, newCol] || map.grid[newRow, newCol] == 'X' || allTreasureFound)
                    {
                        continue; // won't be visited
                    }
                    // else
                    queue.Enqueue(new Point(newRow, newCol)); // enqueue nodes
                    pathPointDir.Add(new PointDir(newRow, newCol, i)); // add to solution (x,y,direction index)
                    visited[newRow, newCol] = true; // visited

                    if (map.grid[newRow, newCol] == 'T' && treasurePicked.FindIndex(p => p.rowId == newRow && p.colId == newCol) == -1)
                    {
                        // treasure found!
                        // mark as picked
                        numOfTreasure--; // treasure found
                        treasurePicked.Add(new Point(newRow, newCol));

                        // create path
                        createPath(pathPointDir, tempStartRow, tempStartCol, ref solution);

                        // begin again
                        queue.Clear(); // delete queue
                        queue.Enqueue(new Point(newRow, newCol)); // add first node (the last treasure node / current position)
                        visited = new bool[map.rows, map.cols]; // set all visited to false
                        tempStartRow = newRow; // set new start point
                        tempStartCol = newCol; // set new start point
                        break;
                    }
                }

                // next
                Point currentPoint = queue.Peek();
                currentRow = currentPoint.rowId;
                currentCol = currentPoint.colId;
            }

            // stop watch
            watch.Stop();

            // result
            pathPoints = convertPathPoints(pathPointDir);
            cntNode = pathPoints.Count;
            num_node = cntNode;
            sol = solution;
            timeExec = watch.ElapsedMilliseconds;
        }

        public static void BFSOneGoal(Map map, Point start, Point end, ref string solution, ref List<Point> _pathPoints)
        {
            // Initiate
            int curRow, curCol;
            visited = new bool[map.rows, map.cols];
            char[,] from = new char[map.rows, map.cols];
            Queue<Point> queue = new Queue<Point>();
            List<Point> pathPoints = new List<Point>();

            // Insert start point to the queue
            queue.Enqueue(new Point(start.rowId, start.colId));
            visited[start.rowId, start.colId] = true;
            from[start.rowId, start.colId] = 'X';

            while (queue.Count > 0)
            {
                // Get the top point of the queue then pop
                curRow = queue.Peek().rowId;
                curCol = queue.Peek().colId;
                queue.Dequeue();

                // Add point to the pathPoints
                pathPoints.Add(new Point(curRow, curCol));

                // Check if current point = end point
                if (curRow == end.rowId && curCol == end.colId)
                {
                    break;
                }

                // Visit possible node (direction priority: RLDU)
                for (int i = 0; i < 4; i++)
                {
                    int newRow = curRow + dy[i];
                    int newCol = curCol + dx[i];

                    // Skip condition
                    if (newRow < 0 || newRow >= map.rows || newCol < 0 || newCol >= map.cols ||
                        visited[newRow, newCol] || map.grid[newRow, newCol] == 'X')
                    {
                        continue;
                    }          
                    
                    // Add to the queue, set to visited, and from direction
                    queue.Enqueue(new Point(newRow, newCol));
                    visited[newRow, newCol] = true;
                    from[newRow, newCol] = direction[i];
                }
            }

            // create path (backtracking)
            curRow = end.rowId; curCol = end.colId;
            while (from[curRow, curCol] != 'X')
            {
                solution = from[curRow, curCol] + solution;
                switch (from[curRow, curCol])
                {
                    case 'R':
                        curCol--;
                        break;
                    case 'L':
                        curCol++;
                        break;
                    case 'D':
                        curRow--;
                        break;
                    case 'U':
                        curRow++;
                        break;
                    default:
                        Console.WriteLine("Direction undefined!");
                        break;
                }
            }
            _pathPoints = pathPoints;
        }

        public static void createPath(List<PointDir> pathPoints, int tempStartRow, int tempStartCol, ref string solution)
        {
            // create path
            // last element in pathPoint
            int currentPathRow = pathPoints[pathPoints.Count - 1].rowId;
            int currentPathCol = pathPoints[pathPoints.Count - 1].colId;
            int currentDirection = pathPoints[pathPoints.Count - 1].direction;

            bool reachStart = false;
            string tempSolution = "";
            while (!reachStart)
            {
                if (currentPathRow == tempStartRow && currentPathCol == tempStartCol)
                {
                    break; // if reached start
                }

                // path solution (reversed: from the last treasure to temp start)
                tempSolution += direction[currentDirection];

                // search for the previous node
                currentPathRow += reverse_dy[currentDirection];
                currentPathCol += reverse_dx[currentDirection];
                // search in pathPoints
                if (!(currentPathRow == tempStartRow && currentPathCol == tempStartCol))
                {
                    pathPoints.Reverse();
                    Predicate<Point> nextPoint = p => p.rowId == currentPathRow && p.colId == currentPathCol;
                    currentDirection = pathPoints[pathPoints.FindIndex(nextPoint)].direction;
                    pathPoints.Reverse();
                }
            }
            // reverse tempSolution
            int strIdx = tempSolution.Length - 1;
            string tempSolutionReversed = "";
            while (strIdx >= 0)
            {
                tempSolutionReversed += tempSolution[strIdx];
                strIdx--;
            }
            // result
            solution += tempSolutionReversed;
        }

        public static List<Point> convertPathPoints(List<PointDir> pathPoints)
        {
            // convert List of PointDir to List of Point (no direction index)
            List<Point> result = new List<Point>() { };
            foreach (PointDir p in pathPoints)
            {
                result.Add(new Point(p.rowId, p.colId));
            }
            return result;
        }
    }
}