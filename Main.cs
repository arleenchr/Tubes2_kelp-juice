public class Program
{
    static void Main()
    {
        char[,] matrix = {{'K', 'R', 'R', 'R'},
                          {'R', 'R', 'X', 'T'},
                          {'R', 'T', 'X', 'X'},
                          {'T', 'X', 'X', 'X'}};

        string sol = "";
        int numNode = 0;
        long timeExec = 0;

        Solver.DFSSolver.callDFS(3, matrix, 4, 4, 0, 0, ref sol, ref numNode, ref timeExec);

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
