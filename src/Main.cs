using Solver;
using System;
using System.Collections.Generic;

public class Program
{
    static void Main()
    {
        try
        {
            Map map = Solver.Parser.parse("a.txt");

            for (int i = 0; i < map.rows; i++)
            {
                for (int j = 0; j < map.cols; j++)
                {
                    Console.Write(map.grid[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("Num of treasures: " + map.numOfTreasure);

            // string solution = "";
            // int cntNode = 0;
            // long timeExec = 0;

            // List<PointDir> points = new List<PointDir>() { };
            /*
            Solver.DFSSolver.callDFS(map, ref solution, ref cntNode, ref timeExec);
            Console.WriteLine("DFS");
            Console.WriteLine("Nodes: " + cntNode);
            Console.WriteLine("Steps: " + solution.Length);
            Console.WriteLine("Time Exec: " + timeExec + " ms");
            Console.Write("Route: ");
            foreach (char c in solution)
            {
                Console.Write(c + " ");
            }*/

            //Console.WriteLine();
            // Console.WriteLine("BFS");
            // Solver.BFSSolver.BFS(map, ref solution, ref cntNode, ref points, ref timeExec);
            // Console.WriteLine("Nodes: " + cntNode);
            // Console.WriteLine("Steps: " + solution.Length);
            // Console.WriteLine("Time Exec: " + timeExec + " ms");
            // Console.Write("Route: ");
            // foreach (char c in solution)
            // {
            //     Console.Write(c + " ");
            // }
            // Console.WriteLine("Path Points:");
            // foreach (PointDir p in points)
            // {
            //     Console.WriteLine(string.Format("({0},{1}), direction index = {2}", p.rowId, p.colId, p.direction));
            // }
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (InvalidDataException ex)
        {
            Console.WriteLine(ex.Message);
        }



    }
}