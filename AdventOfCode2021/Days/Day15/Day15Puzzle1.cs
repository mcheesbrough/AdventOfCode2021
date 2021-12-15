using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2021.General;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day15
{
    public class Day15Puzzle1: IPuzzleSolver
    {
        private readonly IChitonPathFinder _chitonPathFinder;
        private readonly IChitonMapLoader _loader;
        public Day15Puzzle1(IChitonPathFinder chitonPathFinder, IChitonMapLoader loader)
        {
            _chitonPathFinder = chitonPathFinder;
            _loader = loader;
        }

        public string Run()
        {
            var input = File
                .ReadAllLines(@"C:\\aoc\day15\15_1.txt")
                .ToList();
            var map = _loader.Load(input);
            var bigMap = _loader.CombineMaps(map, 5);
            var result = _chitonPathFinder.Find(bigMap, new Coordinate(0, 0));
            return result.ToString();
        }

    }
}