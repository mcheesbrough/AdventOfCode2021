using System;
using System.IO;
using System.Linq;
using System.Xml.Schema;
using AdventOfCode2021.General;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day8
{
    public class Day8Puzzle1: IPuzzleSolver
    {
        private readonly IDigitFinder _digitFinder;
        public Day8Puzzle1(IDigitFinder digitFinder)
        {
            _digitFinder = digitFinder;
        }

        public string Run()
        {
            var input = File
                .ReadAllLines(@"C:\\aoc\day8\8_1.txt")
                .Select(x => x.Split('|'))
                .ToList();
            var inputDigits = input
                .Select(x => x[0])
                .Select(x => x.Trim().Split(' ').Select(y => new Digit(y.Trim())
                ))
                .ToList();

            var outputDigits = input
                .Select(x => x[1])
                .Select(x => x.Trim().Split(' ').Select(y => y.Trim()
                ))
                .ToList();
            var total = 0;
            for (var i = 0; i < inputDigits.Count; i++)
            {
                var mappings = _digitFinder.GetMappings(inputDigits[i].ToList());
                var number = string.Empty;
                foreach (var outputDigit in outputDigits[i])
                {
                    number += _digitFinder.FindDigit(mappings, outputDigit);
                }
                total += int.Parse(number);
            }

            return total.ToString();
        }
    }
}