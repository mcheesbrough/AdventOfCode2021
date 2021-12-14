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
        private readonly ISomething _something;
        public Day15Puzzle1(ISomething something)
        {
            _something = something;
        }

        public string Run()
        {
            var inputString = File
                .ReadAllText(@"C:\\aoc\day14\14_1.txt");
            throw new NotImplementedException();
        }

    }
}