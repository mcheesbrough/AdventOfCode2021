using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using AdventOfCode2021.General;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day2
{
    public class Day2Puzzle1: IPuzzleSolver
    {
        private readonly IMovementInstructionsParser _parser;
        private readonly IMover _mover;

        public Day2Puzzle1(IMovementInstructionsParser parser, IMover mover)
        {
            _parser = parser;
            _mover = mover;
        }

        public string Run()
        {
            var input = File.ReadAllLines(@"C:\\aoc\day2\2_1.txt");
            var instructions = _parser.Parse(input);
            var finalState = _mover.Move(new SubState(new Coordinate( 0,0), 0), instructions);
            return (finalState.Position.X * finalState.Position.Y).ToString();
        }
    }
}