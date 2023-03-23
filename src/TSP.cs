using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver
{

    public class TSPSolver : Solver
    {
        static IEnumerable<IEnumerable<Point>> GetPermutations(List<Point> list)
        {
            if (list.Count == 0)
            {
                yield return Enumerable.Empty<Point>();
            }
            else
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Point item = list[i];
                    List<Point> remaining = new List<Point>(list);
                    remaining.RemoveAt(i);
                    foreach (IEnumerable<Point> permutation in GetPermutations(remaining))
                    {
                        yield return new[] { item }.Concat(permutation);
                    }
                }
            }
        }
        public static void CallTSP(Map _map, ref string _solution, ref int _cntNode, ref List<Point> _pathPoints, ref long timeExec)
        {
            map = _map;
            int cntSolution = int.MaxValue;

            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            IEnumerable<IEnumerable<Point>> allPermutations = GetPermutations(map.treasureLoc);
            foreach (IEnumerable<Point> permutation in allPermutations)
            {
                string permutationSolution = "";
                List<Point> permutationPathPoints = new List<Point>();

                for (int i = 0; i <= map.numOfTreasure; i++)
                {
                    string curSolution = "";
                    List<Point> curPathPoints = new List<Point>();
                    if (i == 0)
                    {
                        BFSSolver.BFSOneGoal(map, map.start, permutation.ElementAt(i), ref curSolution, ref curPathPoints);
                    }
                    else if (i == map.numOfTreasure)
                    {
                        BFSSolver.BFSOneGoal(map, permutation.ElementAt(i - 1), map.start, ref curSolution, ref curPathPoints);
                    }
                    else
                    {
                        BFSSolver.BFSOneGoal(map, permutation.ElementAt(i - 1), permutation.ElementAt(i), ref curSolution, ref curPathPoints);
                    }
                    permutationSolution = permutationSolution + curSolution;
                    permutationPathPoints.AddRange(curPathPoints);
                }

                if (permutationSolution.Length < cntSolution)
                {
                    cntSolution = permutationSolution.Length;
                    _solution = permutationSolution;
                    _pathPoints = permutationPathPoints;
                }
            }

            watch.Stop();

            _cntNode = _pathPoints.Count;
            timeExec = watch.ElapsedMilliseconds;
        }
    }
}