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

            int currentRow = map.start.rowId;
            int currentCol = map.start.colId;
            int tempStartRow = currentRow;
            int tempStartCol = currentCol;
            queue.Enqueue(new Point(currentRow, currentCol)); // add first position to queue

            while (!allTreasureFound && queue.Count > 0)
            {
                visited[currentRow, currentCol] = true; // visited
                queue.Dequeue(); // dequeue

                if (map.grid[currentRow, currentCol] == 'T')
                {
                    /*numOfTreasure--; // treasure found
                    treasurePicked.Add(new Point(currentRow, currentCol));
                    Console.WriteLine("Treasure left: " + numOfTreasure);
                    Console.WriteLine("Treasure Picked:");
                    for (int idx = 0; idx < treasurePicked.Count; idx++)
                    {
                        Console.Write(string.Format("({0},{1}), ", treasurePicked[idx].rowId, treasurePicked[idx].colId));
                    }*/
                }
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
                        // won't be visited
                        continue;
                    }
                    // else
                    queue.Enqueue(new Point(newRow, newCol)); // enqueue nodes
                    pathPointDir.Add(new PointDir(newRow, newCol, i)); // add to solution (x,y,direction index)
                    visited[newRow, newCol] = true; // visited

                    if (map.grid[newRow, newCol] == 'T' && treasurePicked.FindIndex(p => p.rowId == newRow && p.colId == newCol) == -1)
                    {
                        // mark picked
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
            pathPoints = convertPathPoints(pathPointDir);
            cntNode = pathPoints.Count + 1;
            num_node = cntNode;
            sol = solution;
            timeExec = watch.ElapsedMilliseconds;
        }

        public static void BFSOneTreasure(Map map, Point start, ref Point treasurePosition, ref string sol, ref int num_node, ref List<PointDir> pathPoints)
        {
            // search for one treasure only
            string solution = ""; // solution path
            bool isTreasureFound = false;
            numOfTreasure = map.numOfTreasure;

            visited = new bool[map.rows, map.cols]; // is-visited info

            Queue<Point> queue = new Queue<Point> { }; // queue of to-be-visited nodes (queue of Point)

            pathPoints = new List<PointDir>() { }; // list of PointDir

            List<Point> treasurePicked = new List<Point>() { }; // list of treasure picked 

            int currentRow = start.rowId;
            int currentCol = start.colId;
            int tempStartRow = currentRow;
            int tempStartCol = currentCol;
            queue.Enqueue(new Point(currentRow, currentCol)); // add first position to queue

            while (!isTreasureFound && queue.Count > 0)
            {
                visited[currentRow, currentCol] = true; // visited
                queue.Dequeue(); // dequeue

                if (numOfTreasure == 0)
                {
                    isTreasureFound = true; // all treasure found
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
                        // won't be visited
                        continue;
                    }
                    // else
                    queue.Enqueue(new Point(newRow, newCol)); // enqueue nodes
                    pathPoints.Add(new PointDir(newRow, newCol, i)); // add to solution (x,y,direction index)
                    visited[newRow, newCol] = true; // visited

                    if (map.grid[newRow, newCol] == 'T' && treasurePicked.FindIndex(p => p.rowId == newRow && p.colId == newCol) == -1)
                    {
                        // mark picked
                        numOfTreasure--; // treasure found
                        treasurePicked.Add(new Point(newRow, newCol));

                        // create path
                        createPath(pathPoints, tempStartRow, tempStartCol, ref solution);

                        // return
                        treasurePosition.rowId = newRow; // set new start point
                        treasurePosition.colId = newCol; // set new start point
                        return;
                    }
                }

                // next
                Point currentPoint = queue.Peek();
                currentRow = currentPoint.rowId;
                currentCol = currentPoint.colId;
            }

            cntNode = pathPoints.Count + 1;
            num_node = cntNode;
            sol = solution;
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
                    break;
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
            solution += tempSolutionReversed;
        }

        public static List<Point> convertPathPoints(List<PointDir> pathPoints)
        {
            List<Point> result = new List<Point>() { };
            foreach (PointDir p in pathPoints)
            {
                result.Add(new Point(p.rowId, p.colId));
            }
            return result;
        }
    }
}