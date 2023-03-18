public class Program
{
    static void Main()
    {
        char[,] matrix = {{'K', 'R', 'R', 'R'},
                          {'R', 'R', 'X', 'T'},
                          {'R', 'T', 'X', 'X'},
                          {'T', 'X', 'X', 'X'}};

        string sol = "";
        int num_node = 0;

        Solver.DFSSolver.callDFS(3, matrix, 4, 4, ref sol, ref num_node);

        Console.WriteLine(num_node);
        foreach (char c in sol)
        {
            Console.Write(c + " ");
        }
    }
}
