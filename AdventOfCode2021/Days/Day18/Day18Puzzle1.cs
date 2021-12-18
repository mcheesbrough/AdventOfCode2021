using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2021.General;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day18
{
    public class Day18Puzzle1: IPuzzleSolver
    {

        public string Run()
        {
            var input = File
                .ReadAllLines(@"C:\\aoc\day18\18_1.txt")
                .Select(x => SnailFishNumber.FromDescription(x))
                .ToList();

            var largest = (long)0;
            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input.Count; j++)
                {
                    if (i == j) continue;
                    var result = input[i].Add(input[j]);
                    var mag = result.Magnitude;
                    if (mag > largest) largest = mag;
                }

            }

            return largest.ToString();
        }

    }
}