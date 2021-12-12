using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2021.General;

namespace AdventOfCode2021.Days.Day13
{
    public class Day13Puzzle1: IPuzzleSolver
    {
        private readonly ISomething _something;
        public Day13Puzzle1(ISomething something)
        {
            _something = something;
        }

        public string Run()
        {
            var input = File
                .ReadAllLines(@"C:\\aoc\day13\13_1.txt")
                .ToList();
            throw new NotImplementedException();

        }
        
    }
}