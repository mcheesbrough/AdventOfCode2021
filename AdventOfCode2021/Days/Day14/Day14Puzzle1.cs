using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2021.Days.Day13;
using AdventOfCode2021.General;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day14
{
    public class Day14Puzzle1: IPuzzleSolver
    {
        private readonly IPairInsertionRuleLoader _pairInsertionRuleLoader;
        private readonly IPolymerInserter _polymerInserter;
        public Day14Puzzle1(IPairInsertionRuleLoader pairInsertionRuleLoader, IPolymerInserter polymerInserter)
        {
            _pairInsertionRuleLoader = pairInsertionRuleLoader;
            _polymerInserter = polymerInserter;
        }

        public string Run()
        {
            var inputString = File
                .ReadAllText(@"C:\\aoc\day14\14_1.txt");
            var input = inputString
                .Split("\r\n\r\n")
                .ToList();
            var polymerTemplate = input[0];
            var rules = _pairInsertionRuleLoader.LoadPairInsertionRule(input[1].Split("\r\n").ToList());

            var first = polymerTemplate.First();
            var last = polymerTemplate.Last();

            var pairs = _polymerInserter.Insert(polymerTemplate, rules, 40);

            var letterCounts = _polymerInserter.CountLetters(pairs, first, last);

            var orderedCounts = letterCounts
                .Select(x => x.Value/2)
                .OrderBy(x => x)
                .ToList();
            var result = orderedCounts.Last() - orderedCounts.First();
            return result.ToString();
        }

    }
}