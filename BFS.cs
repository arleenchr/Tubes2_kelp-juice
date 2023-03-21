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
        public static void BFS(Map map, ref string sol, ref int num_node)
        {
            cntNode = 0;
            string solution = ""; // solution path
            allTreasureFound = false;

            visited = new bool[map.rows, map.cols]; // is-visited info

            Queue<Point> queue = new Queue<Point> { }; // queue of to-be-visited nodes (queue of Point)

            List<PointDir> pathPoints = new List<PointDir>() { }; // list of PointDir

            List<Point> treasurePicked = new List<Point>() { }; // list of treasure picked 

            int currentRow = map.startRow;
            int currentCol = map.startCol;
            int tempStartRow = currentRow;
            int tempStartCol = currentCol;
            queue.Enqueue(new Point(currentRow, currentCol)); // add first position to queue

            while (!allTreasureFound && queue.Count > 0)
            {
                //Console.WriteLine("----------");
                //Console.WriteLine(string.Format("evaluate ({0},{1})", queue.Peek().rowId, queue.Peek().colId));

                cntNode += 1; // node check
                visited[currentRow, currentCol] = true; // visited
                queue.Dequeue(); // dequeue

                if (map.grid[currentRow, currentCol] == 'T')
                {
                    /*map.numOfTreasure--; // treasure found
                    treasurePicked.Add(new Point(currentRow, currentCol));
                    Console.WriteLine("Treasure left: " + map.numOfTreasure);
                    Console.WriteLine("Treasure Picked:");
                    for (int idx = 0; idx < treasurePicked.Count; idx++)
                    {
                        Console.Write(string.Format("({0},{1}), ", treasurePicked[idx].rowId, treasurePicked[idx].colId));
                    }*/
                }
                if (map.numOfTreasure == 0)
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
                        //Console.WriteLine(string.Format("sekip. ({0},{1}) dir = {2}", newRow, newCol, i));
                        continue;
                    }
                    // else
                    queue.Enqueue(new Point(newRow, newCol)); // enqueue nodes
                    pathPoints.Add(new PointDir(newRow, newCol, i)); // add to solution (x,y,direction index)
                    visited[newRow, newCol] = true; // visited
                    //Console.WriteLine(string.Format("({0},{1}), direction index = {2}", newRow, newCol,i));

                    if (map.grid[newRow, newCol] == 'T' && treasurePicked.FindIndex(p => p.rowId == newRow && p.colId == newCol) == -1)
                    {
                        //Console.WriteLine("Ketemu gesss");

                        // mark picked
                        map.numOfTreasure--; // treasure found
                        treasurePicked.Add(new Point(newRow, newCol));

                        //Console.WriteLine("Treasure left: " + map.numOfTreasure);
                        //Console.WriteLine("Treasure Picked:");
                        //for (int idx = 0; idx < treasurePicked.Count; idx++)
                        //{
                            //Console.Write(string.Format("({0},{1}), ", treasurePicked[idx].rowId, treasurePicked[idx].colId));
                        //}

                        // create path
                        // last element in pathPoint
                        int currentPathRow = pathPoints[pathPoints.Count - 1].rowId;
                        int currentPathCol = pathPoints[pathPoints.Count - 1].colId;
                        int currentDirection = pathPoints[pathPoints.Count - 1].direction;
                        //Console.WriteLine(string.Format("temp start = ({0},{1})", tempStartRow, tempStartCol));
                        //Console.WriteLine(string.Format("beginning path = ({0},{1}), dir = {2}", currentPathRow, currentPathCol, currentDirection));

                        bool reachStart = false;
                        string tempSolution = "";
                        while (!reachStart)
                        {
                            if (currentPathRow==tempStartRow && currentPathCol==tempStartCol)
                            {
                                break;
                            }
                            //Console.WriteLine(string.Format("current path = ({0},{1}), dir = {2}", currentPathRow, currentPathCol, currentDirection));

                            // concat solution
                            tempSolution += reverseDirection[currentDirection];
                            //Console.WriteLine(tempSolution);

                            // search for the previous node
                            currentPathRow += reverse_dy[currentDirection];
                            currentPathCol += reverse_dx[currentDirection];
                            // search in pathPoints
                            if (!(currentPathRow==tempStartRow && currentPathCol==tempStartCol))
                            {
                                pathPoints.Reverse();
                                Predicate<Point> nextPoint = p => p.rowId == currentPathRow && p.colId == currentPathCol;
                                currentDirection = pathPoints[pathPoints.FindIndex(nextPoint)].direction;
                                pathPoints.Reverse();
                                //Console.WriteLine(string.Format("Next = ({0},{1}), dir = {2}", currentPathRow, currentPathCol, currentDirection));
                            }
                        }
                        solution = tempSolution + solution;
                        //Console.WriteLine(solution);

                        // simpen treasure yang udah diambil
                        // if (new) treasure found
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

            num_node = cntNode;
            sol = solution;
        }
    }
}
