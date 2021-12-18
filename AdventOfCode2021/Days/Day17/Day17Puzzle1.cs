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
        private readonly IProbeInputReader _probeInputReader;
        private readonly IProbeCalculator _probeCalculator;
        public Day17Puzzle1(IProbeInputReader probeInputReader, IProbeCalculator probeCalculator)
        {
            _probeInputReader = probeInputReader;
            _probeCalculator = probeCalculator;
        }

        public string Run()
        {
            var input = File
                .ReadAllText(@"C:\\aoc\day17\17_1.txt");
            var coords = _probeInputReader.Read(input);
            var velocities = _probeCalculator.FindAllVelocities(coords[0], coords[1]);
            return velocities.Count.ToString();
        }

    }
}