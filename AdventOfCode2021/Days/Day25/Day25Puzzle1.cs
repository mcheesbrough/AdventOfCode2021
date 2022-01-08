using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2021.General;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day25
{
    public class Day25Puzzle1: IPuzzleSolver
    {
        private readonly ISeaCucumberLoader _loader;
        private readonly ISeaCucumberMover _mover;
        public Day25Puzzle1(ISeaCucumberLoader loader, ISeaCucumberMover mover)
        {
            _loader = loader;
            _mover = mover;
        }

        public string Run()
        {
            var input = File
                .ReadAllLines(@"C:\\aoc\day25\25_1.txt")
                .ToList();
            var map = _loader.Load(input);
            var results = _mover.MoveUntilStuck(map);
            return results.ToString();
        }


    }
}