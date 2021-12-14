using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2021.General;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day13
{
    public class Day13Puzzle1: IPuzzleSolver
    {
        private readonly IFolder _folder;
        private readonly IInstructionPaperLoader _loader;
        public Day13Puzzle1(IFolder folder, IInstructionPaperLoader loader)
        {
            _loader = loader;
            _folder = folder;
        }

        public string Run()
        {
            var inputString = File
                .ReadAllText(@"C:\\aoc\day13\13_1.txt");
            var input = inputString
                .Split("\r\n\r\n");
            var inputCoords = _loader.LoadCoordinates(input[0]);
            var folds = _loader.LoadFolds(input[1]);
            
            var height = folds.Where(x => x.FoldAlong == FoldAlong.Horizontal).Max(x => x.Line) * 2 + 1;
            var width = folds.Where(x => x.FoldAlong == FoldAlong.Vertical).Max(x => x.Line) * 2 + 1;
            var paper = new Paper(inputCoords, width, height);

            foreach (var fold in folds)
            {
                paper = _folder.Fold(paper, fold);
            }

            Console.WriteLine(paper.Print());
            var dots = paper.Dots.Count;
            return dots.ToString();
        }

    }
}