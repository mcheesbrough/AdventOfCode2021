using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2021.General;

namespace AdventOfCode2021.Days.Day12
{
    public class Day12Puzzle1: IPuzzleSolver
    {
        private readonly ICaveLoader _caveLoader;
        private readonly ICavePathFinder _cavePathFinder;
        public Day12Puzzle1(ICaveLoader caveLoader, ICavePathFinder cavePathFinder)
        {
            _caveLoader = caveLoader;
            _cavePathFinder = cavePathFinder;
        }

        public string Run()
        {
            var input = File
                .ReadAllLines(@"C:\\aoc\day12\12_1.txt")
                .ToList();
            var caves = _caveLoader.Load(input);
            var paths = _cavePathFinder.Find(caves, 2);
            var numPaths = paths.Count;
            return numPaths.ToString();

        }
        
    }
}