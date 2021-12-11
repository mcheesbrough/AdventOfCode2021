using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Days.Day1;
using AdventOfCode2021.Days.Day2;
using AdventOfCode2021.Days.Day3;
using AdventOfCode2021.Days.Day4;
using AdventOfCode2021.Days.Day5;
using AdventOfCode2021.Model;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AdventOfCode2021Tests
{
    [TestFixture]
    public class Day5Fixture
    {
        [Test]
        [InlineAutoMoqData(1,1,5,1,true)]
        [InlineAutoMoqData(1, 1, 1, 1, true)]
        [InlineAutoMoqData(1, 1, 5, 2, false)]
        public void LineCalcsHorizontal(
            int x1,
            int y1,
            int x2,
            int y2,
            bool expectedResult)
        {
            var line = new Line(new Coordinate(x1,y1), new Coordinate(x2,y2));
            Assert.That(line.IsHorizontal, Is.EqualTo(expectedResult));

        }

        [Test]
        [InlineAutoMoqData(1, 1, 1, -3, true)]
        [InlineAutoMoqData(1, 1, 1, 1, true)]
        [InlineAutoMoqData(1, 1, 2, 2, false)]
        public void LineCalcsVertical(
            int x1,
            int y1,
            int x2,
            int y2,
            bool expectedResult)
        {
            var line = new Line(new Coordinate(x1, y1), new Coordinate(x2, y2));
            Assert.That(line.IsVertical, Is.EqualTo(expectedResult));

        }

        [Test]
        [InlineAutoMoqData(1, 1, 3, 1, "1,1;2,1;3,1")]
        [InlineAutoMoqData(1, 1, 1, 1, "1,1")]
        [InlineAutoMoqData(1, 1, -3, 1, "1,1;0,1;-1,1;-2,1;-3,1")]
        [InlineAutoMoqData(1, 1, 1, -3, "1,1;1,0;1,-1;1,-2;1,-3")]
        public void GetsPath(
            int x1,
            int y1,
            int x2,
            int y2,
            string expectedResultString)
        {
            var expectedResult = expectedResultString
                .Split(';')
                .Select(x => Coordinate.FromDescription(x))
                .ToList();
            var line = new Line(new Coordinate(x1, y1), new Coordinate(x2, y2));
            CollectionAssert.AreEquivalent(line.Path, expectedResult);

        }

        [Test]
        [InlineAutoMoqData(1, 1, 3, 1,2,2,2,0, "2,1")]
        [InlineAutoMoqData(1, 1, 3, 1, 2, 1, 5, 1, "2,1;3,1")]
        public void FindsIntersections(
            int x1,
            int y1,
            int x2,
            int y2,
            int x3,
            int y3,
            int x4,
            int y4,
            string expectedResultString,
            LineIntersectionFinder sut)
        {
            var expectedResult = expectedResultString
                .Split(';')
                .Select(x => Coordinate.FromDescription(x))
                .ToList();
            var line1 = new Line(new Coordinate(x1, y1), new Coordinate(x2, y2));
            var line2 = new Line(new Coordinate(x3, y3), new Coordinate(x4, y4));
            var lines = new List<Line> {line1, line2};
            var results = sut.FindIntersections(lines);
            CollectionAssert.AreEquivalent(results, expectedResult);

        }

        [Test]
        [InlineAutoMoqData("491,392 -> 34,392", 491, 392, 34, 392)]
        [InlineAutoMoqData(" 1, 2->  3,4", 1, 2, 3, 4)]
        public void CanParseLines(
            string input,
            int x1,
            int y1,
            int x2,
            int y2,
            LineParser sut)
        {
            var lineInputs = new List<string> {input};
            var line = sut.Parse(lineInputs).First();
            Assert.That(line.Start, Is.EqualTo(new Coordinate(x1, y1)));
            Assert.That(line.End, Is.EqualTo(new Coordinate(x2, y2)));
        }

        [Test]
        [InlineAutoMoqData(@"0,9 -> 5,9
            8, 0-> 0, 8
            9, 4-> 3, 4
            2, 2-> 2, 1
            7, 0-> 7, 4
            6, 4-> 2, 0
            0, 9-> 2, 9
            3, 4-> 1, 4
            0, 0-> 8, 8
            5, 5-> 8, 2", 12)]
        public void CanCountIntersections(
            string input,
            int expectedCount)
        {
            var inputLines = input.Split("\r\n").ToList();
            var sut = new IntersectionCounter(new LineParser(), new LineIntersectionFinder());
            var result = sut.Count(inputLines);
            Assert.That(result, Is.EqualTo(expectedCount));
        }
    }
}
