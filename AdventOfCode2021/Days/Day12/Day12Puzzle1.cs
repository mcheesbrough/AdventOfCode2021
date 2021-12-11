using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2021.General;

namespace AdventOfCode2021.Days.Day12
{
    public class Day12Puzzle1: IPuzzleSolver
    {
        private readonly ISomething _something;
        public Day12Puzzle1(ISomething something)
        {
            _something = something;
        }

        public string Run()
        {
            var input = File
                .ReadAllLines(@"C:\\aoc\day12\12_1.txt")
                .ToList();
            throw new NotImplementedException();

        }
        
    }
}