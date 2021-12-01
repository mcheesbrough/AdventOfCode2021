using System.IO;
using System.Linq;
using AdventOfCode2021.General;

namespace AdventOfCode2021.Days.Day1
{
    public class Day1Puzzle1: IPuzzleSolver
    {
        private readonly IDepthChangeCalc _depthChangeCalc;

        public Day1Puzzle1(IDepthChangeCalc depthChangeCalc)
        {
            _depthChangeCalc = depthChangeCalc;
        }

        public string Run()
        {
            var input = File.ReadAllLines(@"C:\\aoc\day1\1_1.txt").Select(x => int.Parse(x)).ToList();
            return _depthChangeCalc.NumberOfIncreases(input).ToString();
        }
    }
}