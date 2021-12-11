using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2021.Days.Day5;
using AdventOfCode2021.General;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day6
{
    public class Day6Puzzle1: IPuzzleSolver
    {
        private readonly ILanternFishCalculator _calculator;
        public Day6Puzzle1(ILanternFishCalculator calculator)
        {
            _calculator = calculator;
        }

        public string Run()
        {
            var fish = File
                .ReadAllText(@"C:\\aoc\day6\6_1.txt")
                .Split(',')
                .Select(int.Parse)
                .ToList();
            var fishCounts = new List<FishCount>();
            for (var i = 0; i <= 8; i++)
            {
                fishCounts.Add(new FishCount{FishTimer = i, Count = 0});
            }
            foreach (var f in fish)
            {
                var count = fishCounts.First(x => x.FishTimer == f);
                count.Count++;
            }
            for (var i = 0; i < 256; i++)
            {
                fishCounts = _calculator.Calculate(fishCounts);
            }

            return fishCounts.Sum(x => x.Count).ToString();
        }
    }
}