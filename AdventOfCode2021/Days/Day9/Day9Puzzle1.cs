using System;
using System.IO;
using System.Linq;
using System.Xml.Schema;
using AdventOfCode2021.Days.Day8;
using AdventOfCode2021.General;

namespace AdventOfCode2021.Days.Day9
{
    public class Day9Puzzle1: IPuzzleSolver
    {
        private readonly IHeatMapLoader _loader;
        private readonly IBasinFinder _finder;
        public Day9Puzzle1(IHeatMapLoader loader, IBasinFinder finder)
        {
            _loader = loader;
            _finder = finder;
        }

        public string Run()
        {
            var input = File
                .ReadAllLines(@"C:\\aoc\day9\9_1.txt")
                .ToList();
            var map = _loader.Load(input);
            var basinSizes = _finder.Find(map);
            var threeLargest = basinSizes.OrderByDescending(x => x).Take(3);
            var product = threeLargest.Aggregate(1, (acc, val) => acc * val);
            return product.ToString();
        }
    }
}