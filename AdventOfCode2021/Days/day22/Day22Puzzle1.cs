using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2021.General;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day22
{
    public class Day22Puzzle1: IPuzzleSolver
    {
        private readonly IReactorRebootLoader _loader;
        private readonly IReactorRebooter _rebooter;
        public Day22Puzzle1(IReactorRebootLoader loader, IReactorRebooter rebooter)
        {
            _loader = loader;
            _rebooter = rebooter;
        }

        public string Run()
        {
            var input = File
                .ReadAllLines(@"C:\\aoc\day22\22_1.txt")
                .ToList();
            var instructions = _loader.Load(input);
            var cubes = _rebooter.Reboot(instructions);
            return cubes.Count.ToString();
        }

    }
}