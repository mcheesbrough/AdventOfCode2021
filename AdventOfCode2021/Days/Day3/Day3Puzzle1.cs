using System;
using System.IO;
using System.Linq;
using AdventOfCode2021.General;

namespace AdventOfCode2021.Days.Day3
{
    public class Day3Puzzle1: IPuzzleSolver
    {
        private readonly ICalculator _powerCalculator;
        public Day3Puzzle1(ICalculator powerCalculator)
        {
            _powerCalculator = powerCalculator;
        }

        public string Run()
        {
            var input = File.ReadAllLines(@"C:\\aoc\day3\3_1.txt").ToList();
            return _powerCalculator.Calculate(input).ToString();
        }
    }
}