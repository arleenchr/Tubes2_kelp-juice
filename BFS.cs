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
        public static void BFS(Map map, int startRow, int startCol, ref string sol, ref int num_node)
        {
            cntNode = 0;
            string solution = ""; // solution path
            cntTreasure = map.getNumOfTreasure();
            allTreasureFound = false;

            MX_ROW = map.getRows();
            MX_COL = map.getCols();
            visited = new bool[MX_ROW, MX_COL]; // is-visited info

            ArrayList queue = new ArrayList { }; // queue of to-be-visited nodes (queue of List<int> (x,y))

            ArrayList pathPoints = new ArrayList(); // array of (x,y,direction)

            grid = new char[MX_ROW, MX_COL];
            for (int i = 0; i < MX_ROW; i++)
            {
                for (int j = 0; j < MX_COL; j++)
                {
                    grid[i, j] = map.getMatrix()[i, j];
                }
            }

            int currentRow = startRow;
            int currentCol = startCol;
            queue.Add(new List<int>() { currentRow, currentCol }); // add first position to queue

            while (!allTreasureFound && queue.Count > 0)
            {
                cntNode++; // node check
                visited[currentRow, currentCol] = true; // visited
                queue.RemoveAt(0); // dequeue

                if (grid[currentRow, currentCol] == 'T')
                {
                    cntTreasure--; // treasure found
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
                    queue.Add(new List<int>() { newRow, newCol }); // enqueue nodes
                    pathPoints.Add(new ArrayList() { newRow, newCol, direction[i] }); // add to solution (x,y,direction)

                    if (grid[newRow, newCol] == 'T' && !map.checkTreasure(newRow, newCol))
                    {
                        // if (new) treasure found
                        queue.Clear(); // delete queue
                        queue.Add(new List<int>() { newRow, newCol }); // add first node (the last treasure node / current position)
                        visited = new bool[MX_ROW, MX_COL]; // set all visited to false
                        break;
                    }
                }

                // next
                int idx = 0;
                int[] tempCurrentPosition = new int[2];
                foreach(List<int> point in queue)
                {
                    foreach(int p in point)
                    {
                        tempCurrentPosition[idx] = p;
                        idx++;
                    }
                }
                currentRow = tempCurrentPosition[0];
                currentCol = tempCurrentPosition[1];
            }

            num_node = cntNode;
            sol = solution;
        }
    }
}
