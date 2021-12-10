using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2021.Days.Day5;
using AdventOfCode2021.General;

namespace AdventOfCode2021.Days.Day10
{
    public class Day10Puzzle1: IPuzzleSolver
    {
        private readonly ISyntaxChecker _syntaxChecker;
        private readonly ISyntaxCompleter _syntaxCompleter;
        public Day10Puzzle1(ISyntaxChecker syntaxChecker, ISyntaxCompleter syntaxCompleter)
        {
            _syntaxChecker = syntaxChecker;
            _syntaxCompleter = syntaxCompleter;
        }

        public string Run()
        {
            var input = File
                .ReadAllLines(@"C:\\aoc\day10\10_1.txt")
                .ToList();
            var middleScore = _syntaxCompleter.CalcCompletionScore(input);
            return middleScore.ToString();
        }


    }
}