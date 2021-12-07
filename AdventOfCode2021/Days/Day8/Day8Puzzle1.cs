using System;
using System.IO;
using System.Linq;
using AdventOfCode2021.General;

namespace AdventOfCode2021.Days.Day8
{
    public class Day8Puzzle1: IPuzzleSolver
    {
        public Day8Puzzle1()
        {
        }

        public string Run()
        {
            var input = File
                .ReadAllText(@"C:\\aoc\day8\8_1.txt")
                .Split(',')
                .Select(int.Parse)
                .ToList();
            throw new NotImplementedException();
        }
    }
}