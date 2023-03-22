namespace Solver
{
    public class Parser
    {
        static bool validate(char[,] grid)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] != 'K' && grid[i, j] != 'R' &&
                        grid[i, j] != 'X' && grid[i, j] != 'T')
                        return false;
                }
            }
            return true;
        }
        public static Map parse(string userFileName)
        {
            string directoryPath = "..\\test";
            userFileName = "..\\test\\" + userFileName;

            string[] files = Directory.GetFiles(directoryPath);
            bool foundFile = false;
            foreach (string file in files)
            {
                if (file == userFileName)
                    foundFile = true;
            }

            if (!foundFile) throw new FileNotFoundException("File tidak ditemukan!");

            char[,]? grid = null;
            using (StreamReader reader = new StreamReader(userFileName))
            {
                string? line = reader.ReadLine();
                if (line == null) throw new InvalidDataException("File tidak valid!");

                grid = new char[File.ReadAllLines(userFileName).Length, line.Length];
                int cntLine = 0;

                while (line != null)
                {
                    for (int i = 0; i < line.Length; i++)
                    {
                        grid[cntLine, i] = line[i];
                    }
                    cntLine++;
                    line = reader.ReadLine();
                }
            }

            if (!validate(grid)) throw new InvalidDataException("File tidak valid!");
            return new Map(grid);
        }
    }
}