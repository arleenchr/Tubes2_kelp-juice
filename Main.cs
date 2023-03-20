using Solver;
using System.Collections;

public class Program
{
    static void Main()
    {
        char[,] matrix = {{'K', 'R', 'R', 'R'},
                          {'R', 'R', 'X', 'T'},
                          {'R', 'T', 'X', 'X'},
                          {'T', 'X', 'X', 'X'}};
        ArrayList treasures = new ArrayList();
        treasures.Add(new List<int>() { 1, 3 });
        treasures.Add(new List<int>() { 2, 1 });
        treasures.Add(new List<int>() { 3, 0 });

        Map map = new Map(matrix, 4, 4, 0, 0, 3, treasures);

        string sol = "";
        int numNode = 0;
        long timeExec = 0;

        Solver.DFSSolver.callDFS(map, ref sol, ref numNode, ref timeExec);

        Console.WriteLine("Nodes: " + numNode);
        Console.WriteLine("Steps: " + sol.Length);
        Console.WriteLine("Time Exec: " + timeExec + " ms");
        Console.Write("Route: ");
        foreach (char c in sol)
        {
            Console.Write(c + " ");
        }
    }
}
