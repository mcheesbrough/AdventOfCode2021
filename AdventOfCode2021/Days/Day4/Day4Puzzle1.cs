using System;
using System.IO;
using System.Linq;
using AdventOfCode2021.General;

namespace AdventOfCode2021.Days.Day4
{
    public class Day4Puzzle1: IPuzzleSolver
    {
        private readonly IBingoPlayer _bingoPlayer;
        public Day4Puzzle1(IBingoPlayer bingoPlayer)
        {
            _bingoPlayer = bingoPlayer;
        }

        public string Run()
        {
            var input = File.ReadAllText(@"C:\\aoc\day4\4_1.txt");
            return _bingoPlayer.PlayToLose(input).ToString();
        }
    }
}