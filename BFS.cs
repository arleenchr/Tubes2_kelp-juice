﻿using System;
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
            cntTreasure = map.numOfTreasure;
            allTreasureFound = false;

            MX_ROW = map.rows;
            MX_COL = map.cols;
            visited = new bool[MX_ROW, MX_COL]; // is-visited info

            Queue<Point> queue = new Queue<Point> { }; // queue of to-be-visited nodes (queue of Point)

            List<PointDir> pathPoints = new List<PointDir>() { }; // list of PointDir

            List<Point> treasurePicked = new List<Point>() { }; // list of treasure picked 

            grid = new char[MX_ROW, MX_COL];
            for (int i = 0; i < MX_ROW; i++)
            {
                for (int j = 0; j < MX_COL; j++)
                {
                    grid[i, j] = map.matrix[i, j];
                }
            }

            int currentRow = map.startRow;
            int currentCol = map.startCol;
            int tempStartRow = currentRow;
            int tempStartCol = currentCol;
            queue.Enqueue(new Point(currentRow, currentCol)); // add first position to queue

            while (!allTreasureFound && queue.Count > 0)
            {
                cntNode++; // node check
                visited[currentRow, currentCol] = true; // visited
                queue.Dequeue(); // dequeue

                if (grid[currentRow, currentCol] == 'T')
                {
                    cntTreasure--; // treasure found
                    treasurePicked.Add(new Point(currentRow,currentCol));
                }
                if (cntTreasure == 0)
                {
                    allTreasureFound = true; // all treasure found
                    return;
                }

                // visit node (direction priority: RLDU)
                for (int i = 0; i < 4; i++)
                {
                    int newRow = currentRow + dy[i];
                    int newCol = currentCol + dx[i];
                    if (newRow < 0 || newRow >= MX_ROW || newCol < 0 || newCol >= MX_COL ||
                        visited[newRow, newCol] || grid[newRow, newCol] == 'X' || allTreasureFound)
                    {
                        // won't be visited
                        continue;
                    }
                    // else
                    queue.Enqueue(new Point(newRow, newCol)); // enqueue nodes
                    pathPoints.Add(new PointDir( newRow, newCol, i )); // add to solution (x,y,direction index)

                    //Console.WriteLine(string.Format("({0},{1}), direction index = {2}", newRow, newCol,i));

                    if (grid[newRow, newCol] == 'T' && treasurePicked.FindIndex(p => p.x==newRow && p.y==newCol)==-1)
                    {
                        //Console.WriteLine("Ketemu gesss");
                        // create path
                        // last element in pathPoint
                        int currentPathRow = pathPoints[pathPoints.Count - 1].y;
                        int currentPathCol = pathPoints[pathPoints.Count - 1].x;
                        int directionIndex = pathPoints[pathPoints.Count - 1].direction;

                        while (currentPathRow!=tempStartRow && currentPathCol != tempStartCol)
                        {
                            // concat solution
                            solution += reverseDirection[directionIndex];

                            // search for the previous node
                            currentPathRow += reversedy[directionIndex];
                            currentPathCol += reversedx[directionIndex];
                            // search in pathPoints
                            if (currentPathRow != tempStartRow && currentPathCol != tempStartCol)
                            {
                                Predicate<Point> nextPoint = p => p.x == currentPathCol && p.y == currentPathRow;
                                directionIndex = pathPoints[pathPoints.FindIndex(nextPoint)].direction;
                            }
                        }

                        // simpen treasure yang udah diambil
                        // if (new) treasure found
                        queue.Clear(); // delete queue
                        queue.Enqueue(new Point(newRow, newCol)); // add first node (the last treasure node / current position)
                        visited = new bool[MX_ROW, MX_COL]; // set all visited to false
                        tempStartRow = newRow; // set new start point
                        tempStartCol = newCol; // set new start point
                        break;
                    }
                }

                // next
                Point currentPoint = queue.Peek();
                currentRow = currentPoint.y;
                currentCol = currentPoint.x;
            }

            num_node = cntNode;
            sol = solution;
        }
    }
}
