using Solver;

public class Program
{
    static void Main()
    {
        char[,] grid = {{'K', 'R', 'R', 'R'},
                        {'R', 'R', 'X', 'T'},
                        {'R', 'T', 'X', 'X'},
                        {'T', 'X', 'X', 'X'}};

        Map map = new Map(grid);

        string solution = "";
        int cntNode = 0;
        long timeExec = 0;

        Solver.DFSSolver.callDFS(map, ref solution, ref cntNode, ref timeExec);
        Console.WriteLine("DFS");
        Console.WriteLine("Nodes: " + cntNode);
        Console.WriteLine("Steps: " + solution.Length);
        Console.WriteLine("Time Exec: " + timeExec + " ms");
        Console.Write("Route: ");
        foreach (char c in solution)
        {
            Console.Write(c + " ");
        }
        /*
        Solver.BFSSolver.BFS(map, ref solution, ref cntNode);
        Console.WriteLine("BFS");
        Console.WriteLine("Nodes: " + cntNode);
        Console.WriteLine("Steps: " + solution.Length);
        //Console.WriteLine("Time Exec: " + timeExec + " ms");
        Console.Write("Route: ");
        foreach (char c in solution)
        {
            Console.Write(c + " ");
        }*/
    }
}
