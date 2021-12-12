using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day12
{
    public interface ICavePathFinder
    {
        List<string> Find(List<Cave> caves, int visitLimit);
    }

    public class CavePathFinder : ICavePathFinder
    {
        public List<string> Find(List<Cave> caves, int visitLimit)
        {
            var start = caves.First(c => c.IsStart);
            var smallCaves = caves.Where(x => !x.IsBig);
            var finalPaths = new List<string>();
            foreach (var smallCave in smallCaves)
            {
                var cavePaths = new List<List<Cave>>();
                FindRecursive(cavePaths, new List<Cave>(), start, visitLimit, smallCave);
                AddDistinctPaths(finalPaths, cavePaths);
            }
            return finalPaths;
        }

        private void AddDistinctPaths(List<string> paths, List<List<Cave>> cavePathsToAdd)
        {
            foreach (var cavePathToAdd in cavePathsToAdd)
            {
                var pathString = string.Join(',', cavePathToAdd.Select(x => x.Name));
                if (!paths.Contains(pathString)) paths.Add(pathString);
            }
        }

        private void FindRecursive(List<List<Cave>> cavePaths, List<Cave> currentPath, Cave cave, int visitLimit, Cave small)
        {
            if (!TryAddCave(currentPath, cave, visitLimit, small))
            {
                return;
            }

            if (cave.IsEnd)
            {
                cavePaths.Add(currentPath);
                return;
            };

            foreach (var connectedCave in cave.ConnectedCaves)
            {
                var path = new List<Cave>(currentPath);
                FindRecursive(cavePaths, path, connectedCave, visitLimit, small);
            }

        }

        private bool TryAddCave(List<Cave> cavePath, Cave cave, int visitLimit, Cave small)
        {
            if (!cave.IsBig)
            {
                if ((cave.IsStart || cave.IsEnd || cave != small) &&
                    cavePath.Contains(cave)) return false;
                if (cavePath.Count(x => x == cave) >= visitLimit) return false;
            }


            cavePath.Add(cave);
            cave.Visits++;
            return true;
        }
    }
}