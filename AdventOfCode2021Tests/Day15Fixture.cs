using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode2021.Days.Day14;
using AdventOfCode2021.Days.Day15;
using AdventOfCode2021.Model;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;

namespace AdventOfCode2021Tests
{
    [TestFixture]
    public class Day15Fixture
    {
        [Test]
        [InlineAutoMoqData(@"1163751742
1381373672
2136511328
3694931569
7463417111
1319128137
1359912421
3125421639
1293138521
2311944581", 40)]
 public void FindPaths(
            string inputString,
            int exoectedPathSum,
            ChitonPathFinder sut)
        {
            var input = inputString
                .Split("\r\n")
                .ToList();
            var loader = new ChitonMapLoader();
            var map = loader.Load(input);
            var dist = sut.Find(map, new Coordinate(0,0));
            Assert.That(dist, Is.EqualTo(exoectedPathSum));
        }

 [Test]
 [InlineAutoMoqData(@"18
69", 3, @"182931
697182
293142
718293
314253
829314")]
 public void GrowMap(
     string inputString,
     int repeats,
     string expectedString,
     ChitonMapLoader sut)
 {
     var input = inputString
         .Split("\r\n")
         .ToList();
     var map = sut.Load(input);
     var bigMap = sut.CombineMaps(map, repeats);
     var bigMapString = bigMap.Print();
     Assert.That(bigMapString, Is.EqualTo(expectedString));
 }
    }
}
