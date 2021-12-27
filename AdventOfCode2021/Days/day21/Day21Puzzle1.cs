using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2021.General;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day21
{
    public class Day21Puzzle1: IPuzzleSolver
    {
        private readonly IDiracDice _diracDice;
        public Day21Puzzle1(IDiracDice diracDice)
        {
            _diracDice = diracDice;
        }

        public string Run()
        {
            var input = File
                .ReadAllLines(@"C:\\aoc\day21\test.txt");

            var player1Start = int.Parse(input[0].Last().ToString());
            var player2Start = int.Parse(input[1].Last().ToString());
            var highestScore = _diracDice.PlayQuantum(player1Start, player2Start, 21);

            return highestScore.ToString();
        }

    }
}