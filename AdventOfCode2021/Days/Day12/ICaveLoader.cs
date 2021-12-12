using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day12
{
    public interface ICaveLoader

    {
        List<Cave> Load(List<string> caveConnections);
    }

    public class CaveLoader : ICaveLoader
    {
        public List<Cave> Load(List<string> caveConnections)
        {
            var caves = new List<Cave>();
            foreach (var caveConnection in caveConnections)
            {
                var cavePair = caveConnection.Split('-');
                var cave1 = TryAddCave(caves, cavePair[0], cavePair[1]);
                var cave2 = TryAddCave(caves, cavePair[1], cavePair[0]);
                cave1.AddConnection(cave2);
                cave2.AddConnection(cave1);

            }

            return caves;
        }


        private Cave TryAddCave(List<Cave> caves, string caveToAdd, string cave2)
        {
            var cave = caves.FirstOrDefault(c => c.Name == caveToAdd);
            if (cave != null) return cave;
            cave = new Cave(caveToAdd);
            caves.Add(cave);

            return cave;
        }
    }
}