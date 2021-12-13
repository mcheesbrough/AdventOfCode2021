using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Days.Day13;
using AdventOfCode2021.Model;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;

namespace AdventOfCode2021Tests
{
    [TestFixture]
    public class Day13Fixture
    {
        [Test]
        [InlineAutoMoqData(@"6,10
0,14
9,10
0,3
10,4
4,11
6,0
6,12
4,1
0,13
10,12
3,4
3,0
8,4
1,10
2,14
8,10
9,0

fold along y=7
fold along x=5", 17)]
        public void CanLoadNames(
            string inputString,
            int expectedDots,
            Folder sut)
        {
            var loader = new InstructionPaperLoader();
            var input = inputString
                .Split("\r\n\r\n");
            var inputCoords = loader.LoadCoordinates(input[0]);
            var folds = loader.LoadFolds(input[1]);


            var height = folds.Where(x => x.FoldAlong == FoldAlong.Horizontal).Max(x => x.Line) * 2 + 1;
            var width = folds.Where(x => x.FoldAlong == FoldAlong.Vertical).Max(x => x.Line) * 2 + 1;
            var paper = new Paper(inputCoords, width, height);

            var result = sut.Fold(paper, folds[0]);

            Assert.That(result.Dots.Count, Is.EqualTo(expectedDots));
        }

        [Test]
        [InlineAutoMoqData("fold along y=4", FoldAlong.Horizontal, 4)]
        [InlineAutoMoqData("fold along x=6", FoldAlong.Vertical, 6)]
        public void CanCreateFoldFromDescription(
            string description,
            FoldAlong expectedDirection,
            int expectedLine)
        {
            var sut = Fold.FromDescription(description);
            Assert.That(sut.FoldAlong, Is.EqualTo(expectedDirection));
            Assert.That(sut.Line, Is.EqualTo(expectedLine));
        }

        [Test]
        [InlineAutoMoqData("1,0;2,0;1,1;2,1", 4,2, @".##.
.##.")]
        [InlineAutoMoqData("1,1;2,1;1,2;2,2", 4, 4, @"....
.##.
.##.
....")]
        public void CanPrint(
            string input,
            int width,
            int height,
            string expectedPrint)
        {
            var inputCoords = input
                .Split(';')
                .Select(Coordinate.FromDescription)
                .ToList();
            var paper = new Paper(inputCoords, width, height);
            var result = paper.Print();
            Assert.That(result, Is.EqualTo(expectedPrint));
        }

    }
}
