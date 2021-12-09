using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Days.Day2;
using AdventOfCode2021.Days.Day9;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AdventOfCode2021Tests
{
    [TestFixture]
    public class Day9Fixture
    {
        [Test]
        [InlineAutoMoqData(@"21
13",  "0,0;1,0;0,1;1,1", "2,1,1,3")]
        public void LoadsHeatmap(
            string input,
            string expectedCoords,
            string expectedHeights,
            HeatMapLoader sut)
        {
            var inputList = input.Split("\r\n").ToList();
            var coords = expectedCoords.Split(';')
                .Select(Coordinate.FromDescription)
                .ToList();
            var heights = expectedHeights.Split(',').Select(int.Parse).ToList();
            var expectedPoints = new List<HeatMapPoint>();
            for (int i = 0; i < coords.Count; i++)
            {
                expectedPoints.Add(new HeatMapPoint(coords[i], heights[i]));
            }

            var result = sut.Load(inputList).Points;
            Assert.That(result[0].Point, Is.EqualTo(expectedPoints[0].Point));
            Assert.That(result[0].Height, Is.EqualTo(expectedPoints[0].Height));
            Assert.That(result[1].Point, Is.EqualTo(expectedPoints[1].Point));
            Assert.That(result[1].Height, Is.EqualTo(expectedPoints[1].Height));
            Assert.That(result[2].Point, Is.EqualTo(expectedPoints[2].Point));
            Assert.That(result[2].Height, Is.EqualTo(expectedPoints[2].Height));
            Assert.That(result[3].Point, Is.EqualTo(expectedPoints[3].Point));
            Assert.That(result[3].Height, Is.EqualTo(expectedPoints[3].Height));

        }

        [Test]
        [InlineAutoMoqData("0,0;1,0;2,0;0,1;1,1;2,1;0,2;1,2;2,2", 3, 3, "1,1", "1,0;0,1;1,2;2,1")]
        [InlineAutoMoqData("0,0;1,0;2,0;0,1;1,1;2,1;0,2;1,2;2,2", 3, 3, "0,0", "1,0;0,1")]
        [InlineAutoMoqData("0,0;1,0;2,0;0,1;1,1;2,1;0,2;1,2;2,2", 3, 3, "2,2", "1,2;2,1")]
        [InlineAutoMoqData("0,0;1,0;2,0;0,1;1,1;2,1;0,2;1,2;2,2", 3, 3, "1,0", "0,0;2,0;1,1")]
        public void GetsAdjacent(
            string input,
            int width, int height,
            string test,
            string expectedPointsString)
        {
            var inputList = input.Split(';')
                .Select(x => new HeatMapPoint(Coordinate.FromDescription(x), 0))
                .ToList();
            var testPoint = new HeatMapPoint(Coordinate.FromDescription(test), 0);
            var expectedPoints = expectedPointsString.Split(';')
                .Select(Coordinate.FromDescription)
                .ToList();
            var map = new Map(inputList, width, height);
            var result = map.AdjacentPoints(testPoint);

            Assert.That(result.Count, Is.EqualTo(expectedPoints.Count));
            var resultCoords = result.Select(x => x.Point);
            Assert.That(resultCoords, Is.EquivalentTo(expectedPoints));
        }

        [Test]
        [InlineAutoMoqData("0,0;1,0;2,0;0,1;1,1;2,1;0,2;1,2;2,2", "011111111", "0,0")]
        [InlineAutoMoqData("0,0;1,0;2,0;0,1;1,1;2,1;0,2;1,2;2,2", "011111110", "0,0;2,2")]
        public void FindsLowPoints(
            string input,
            string heights,
            string expectLowsString,
            LowPointFinder sut)
        {
            var inputList = input.Split(';')
                .Select(Coordinate.FromDescription)
                .ToList();
            var points = new List<HeatMapPoint>();
            for (int i = 0; i < inputList.Count; i++)
            {
                points.Add(new HeatMapPoint(inputList[i], int.Parse(heights[i].ToString())));
            }

            var map = new Map(points, 3, 3);

            var expectLows = expectLowsString.Split(';')
                .Select(Coordinate.FromDescription)
                .ToList();
            var result = sut.Find(map);

            Assert.That(result.Count, Is.EqualTo(expectLows.Count));
            var resultCoords = result.Select(x => x.Point);
            Assert.That(resultCoords, Is.EquivalentTo(expectLows));
        }


        [Test]
        [InlineAutoMoqData("0,0;1,0;2,0;0,1;1,1;2,1;0,2;1,2;2,2", "019199119", 5)]
        [InlineAutoMoqData("0,0;1,0;2,0;0,1;1,1;2,1;0,2;1,2;2,2", "099999999", 1)]
        public void FindBasinSizes(
            string input,
            string heights,
            int expectedSize)
        {
            var sut = new BasinFinder(new LowPointFinder());
            var inputList = input.Split(';')
                .Select(Coordinate.FromDescription)
                .ToList();
            var points = new List<HeatMapPoint>();
            for (int i = 0; i < inputList.Count; i++)
            {
                points.Add(new HeatMapPoint(inputList[i], int.Parse(heights[i].ToString())));
            }

            var map = new Map(points, 3, 3);

            var result = sut.Find(map)[0];

            Assert.That(result, Is.EqualTo(expectedSize));
        }
    }
}
