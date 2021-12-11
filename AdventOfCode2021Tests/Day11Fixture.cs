using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Days.Day10;
using AdventOfCode2021.Days.Day11;
using AdventOfCode2021.Days.Day2;
using AdventOfCode2021.Days.Day9;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AdventOfCode2021Tests
{
    [TestFixture]
    public class Day11Fixture
    {
        [Test]
        [InlineAutoMoqData("0,0;1,0;2,0;0,1;1,1;2,1;0,2;1,2;2,2", 3, 3, "1,1", "0,0;1,0;2,0;0,1;2,1;0,2;1,2;2,2")]
        [InlineAutoMoqData("0,0;1,0;2,0;0,1;1,1;2,1;0,2;1,2;2,2", 3, 3, "0,0", "1,0;0,1;1,1")]
        [InlineAutoMoqData("0,0;1,0;2,0;0,1;1,1;2,1;0,2;1,2;2,2", 3, 3, "2,2", "1,2;2,1;1,1")]
        [InlineAutoMoqData("0,0;1,0;2,0;0,1;1,1;2,1;0,2;1,2;2,2", 3, 3, "1,0", "0,0;2,0;0,1;1,1;2,1")]
        public void GetsAdjacent(
            string input,
            int width, int height,
            string test,
            string expectedPointsString)
        {
            var inputList = input.Split(';')
                .Select(x => new OctopusPoint(Coordinate.FromDescription(x), 0))
                .ToList();
            var testPoint = new OctopusPoint(Coordinate.FromDescription(test), 0);
            var expectedPoints = expectedPointsString.Split(';')
                .Select(Coordinate.FromDescription)
                .ToList();
            var map = new Map<OctopusPoint>(inputList, width, height);
            var result = map.AdjacentIncludingDiagonalPoints(testPoint);

            Assert.That(result.Count, Is.EqualTo(expectedPoints.Count));
            var resultCoords = result.Select(x => x.Coordinate);
            Assert.That(resultCoords, Is.EquivalentTo(expectedPoints));
        }

        [Test]
        [InlineAutoMoqData(@"11111
19991
19191
19991
11111", @"34543
40004
50005
40004
34543")]
        [InlineAutoMoqData(@"34543
40004
50005
40004
34543", @"45654
51115
61116
51115
45654")]
        public void RunsTurn(
            string input,
            string expected,
            OctopusTurnProcessor sut)
        {
            var inputList = input.Split("\r\n")
                .ToList();
            var expectedList = expected.Split("\r\n")
                .ToList();
            var inputMap = BuildMap(inputList);
            var expectedMap = BuildMap(expectedList);
            var resultMap = sut.Process(inputMap, 1);

            for (int y = 0; y < inputList.Count; y++)
            {
                for (int x = 0; x < inputList[0].Length; x++)
                {
                    var testCorrd = new Coordinate(x, y);
                    Assert.That(resultMap.Points.First(p => p.Coordinate == testCorrd).Brightness, 
                        Is.EqualTo(expectedMap.Points.First(p => p.Coordinate == testCorrd).Brightness));
                }
            }
        }

        [Test]
        [InlineAutoMoqData(@"11111
19991
19191
19991
11111", 1, 9)]
        [InlineAutoMoqData(@"5483143223
2745854711
5264556173
6141336146
6357385478
4167524645
2176841721
6882881134
4846848554
5283751526", 10, 204)]
        [InlineAutoMoqData(@"5483143223
2745854711
5264556173
6141336146
6357385478
4167524645
2176841721
6882881134
4846848554
5283751526", 100, 1656)]
        public void RunsTurns(
            string input,
            int turns,
            int expected)
        {
            var sut = new TurnRunner(new OctopusTurnProcessor());
            var inputList = input.Split("\r\n")
                .ToList();
            var inputMap = BuildMap(inputList);

            var result = sut.Run(inputMap, turns);
            Assert.That(result, Is.EqualTo(expected));
        }

        private Map<OctopusPoint> BuildMap(List<string> lines)
        {
            var points = new List<OctopusPoint>();
            for (int y = 0; y < lines.Count; y++)
            {
                for (int x = 0; x < lines[0].Length; x++)
                {
                    points.Add(new OctopusPoint(new Coordinate(x,y), int.Parse(lines[y][x].ToString())));
                }
            }

            return new Map<OctopusPoint>(points, lines.Count, lines.Count);
        }
    }


}
