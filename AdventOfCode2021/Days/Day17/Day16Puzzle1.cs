using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2021.General;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day17
{
    public class Day17Puzzle1: IPuzzleSolver
    {
        private readonly ISomething _something;
        public Day17Puzzle1(ISomething something)
        {
            _something = something;
        }

        public string Run()
        {
            var input = File
                .ReadAllText(@"C:\\aoc\day17\17_1.txt");
            throw new NotImplementedException();
        }

    }
}