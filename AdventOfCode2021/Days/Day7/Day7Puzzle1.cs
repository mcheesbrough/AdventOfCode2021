using System;
using System.IO;
using System.Linq;
using AdventOfCode2021.General;

namespace AdventOfCode2021.Days.Day7
{
    public class Day7Puzzle1: IPuzzleSolver
    {
        private readonly ISomething _something;
        public Day7Puzzle1(ISomething something)
        {
            _something = something;
        }

        public string Run()
        {
            var input = File
                .ReadAllText(@"C:\\aoc\day7\7_1.txt")
                .Split(',')
                .Select(int.Parse)
                .ToList();
            throw new NotImplementedException();
        }
    }
}