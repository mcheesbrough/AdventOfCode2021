using System;
using System.IO;
using System.Linq;
using AdventOfCode2021.Days.Day5;
using AdventOfCode2021.General;

namespace AdventOfCode2021.Days.Day5
{
    public class Day5Puzzle1: IPuzzleSolver
    {
        private readonly IIntersectionCounter _counter;
        public Day5Puzzle1(IIntersectionCounter counter)
        {
            _counter = counter;
        }

        public string Run()
        {
            var lineDefs = File.ReadAllLines(@"C:\\aoc\day5\5_1.txt").ToList();
            return _counter.Count(lineDefs).ToString();
        }
    }
}