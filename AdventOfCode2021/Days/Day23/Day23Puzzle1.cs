using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2021.General;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day23
{
    public class Day23Puzzle1: IPuzzleSolver
    {
        private readonly IAmphipodArranger _arranger;
        public Day23Puzzle1(IAmphipodArranger arranger)
        {
            _arranger = arranger;
        }

        public string Run()
        {
            var testInputString1 = "3,5,1;3,4,1;3, 3, 1;3,2,10;5,5,10;5,4,10;5,3,1000;5,2,100;7,5,100;7,4,100;7,3,100;7,2,10;9,5,1000;9,4,1000;9,3,1;9,2,1000";
            var testInputString2 = "3,5,1;3,4,1000;3, 3, 1000;3,2,10;5,5,1000;5,4,10;5,3,100;5,2,100;7,5,100;7,4,1;7,3,10;7,2,10;9,5,1;9,4,100;9,3,1;9,2,1000";
            var inputString = "3, 5, 1000;3,4,1000;3, 3, 1000;3,2,100;5,5,1000;5,4,10;5,3,100;5,2,1;7,5,10;7,4,1;7,3,10;7,2,10;9,5,1;9,4,100;9,3,1;9,2,100";
            var input = inputString.Split(';').Select(Amphipod.FromDescription).ToList();
            var board = new AmphipodBoard(input);
            var result = _arranger.Arrange(input);

            return result.ToString();
        }

    }
}