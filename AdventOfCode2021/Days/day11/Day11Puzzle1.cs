using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2021.Days.Day2;
using AdventOfCode2021.Days.Day5;
using AdventOfCode2021.Days.Day9;
using AdventOfCode2021.General;

namespace AdventOfCode2021.Days.Day11
{
    public class Day11Puzzle1: IPuzzleSolver
    {
        private readonly ITurnRunner _turnRunner;
        public Day11Puzzle1(ITurnRunner turnRunner)
        {
            _turnRunner = turnRunner;
        }

        public string Run()
        {
            var input = File
                .ReadAllLines(@"C:\\aoc\day11\11_1.txt")
                .ToList();
            var map = BuildMap(input);
            var flashes = _turnRunner.Run(map, 500);
            return flashes.ToString();

        }

        private Map<OctopusPoint> BuildMap(List<string> lines)
        {
            var points = new List<OctopusPoint>();
            for (int y = 0; y < lines.Count; y++)
            {
                for (int x = 0; x < lines[0].Length; x++)
                {
                    points.Add(new OctopusPoint(new Coordinate(x, y), int.Parse(lines[y][x].ToString())));
                }
            }

            return new Map<OctopusPoint>(points, lines[0].Length, lines.Count);
        }


    }
}