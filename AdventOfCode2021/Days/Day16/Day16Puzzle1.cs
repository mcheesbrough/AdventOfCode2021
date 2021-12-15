using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2021.General;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day16
{
    public class Day16Puzzle1: IPuzzleSolver
    {
        private readonly ISomething _something;
        public Day16Puzzle1(ISomething something)
        {
            _something = something;
        }

        public string Run()
        {
            var input = File
                .ReadAllLines(@"C:\\aoc\day16\16_1.txt")
                .ToList();
            throw new NotImplementedException();
        }

    }
}