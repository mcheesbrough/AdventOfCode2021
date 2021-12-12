using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Days.Day10;
using AdventOfCode2021.Days.Day11;
using AdventOfCode2021.Days.Day12;
using AdventOfCode2021.Days.Day2;
using AdventOfCode2021.Days.Day9;
using AdventOfCode2021.Model;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AdventOfCode2021Tests
{
    [TestFixture]
    public class Day12Fixture
    {
        [Test]
        [InlineAutoMoqData(@"start-A", "start,A")]
        [InlineAutoMoqData(@"start-A
A-end", "start,A,end")]
        public void CanLoadNames(
            string inputString,
            string expectedString,
            CaveLoader sut)
        {
            var inputList = inputString.Split("\r\n")
                .ToList();
            var expectedCaves = expectedString.Split(',')
                .ToList();
            var result = sut.Load(inputList);

            CollectionAssert.AreEquivalent(result.Select(c => c.Name), expectedCaves);
        }

        [Test]
        [InlineAutoMoqData(@"start-A", "start,A")]
        [InlineAutoMoqData(@"start-A
A-end", "start,A;A,start,end")]

        public void LoadsConnections(
            string inputString,
            string expectedString,
            CaveLoader sut)
        {
            var inputList = inputString.Split("\r\n")
                .ToList();
            var expectedCaveConnections = expectedString.Split(';')
                .ToList();
            var result = sut.Load(inputList);
            foreach (var expectedCaveConnection in expectedCaveConnections)
            {
                var caves = expectedCaveConnection.Split(',');
                var cave = result.First(x => x.Name == caves.First());
                CollectionAssert.AreEquivalent(cave.ConnectedCaves.Select(x => x.Name), caves.Skip(1));
            }
        }

        [Test]
        [InlineAutoMoqData(@"start-A
start-b
A-c
A-b
b-d
A-end
b-end", @"start,A,b,A,c,A,end
start,A,b,A,end
start,A,b,end
start,A,c,A,b,A,end
start,A,c,A,b,end
start,A,c,A,end
start,A,end
start,b,A,c,A,end
start,b,A,end
start,b,end", 1)]
        [InlineAutoMoqData(@"start-A
A-end", @"start,A,end", 1)]
        [InlineAutoMoqData(@"dc-end
HN-start
start-kj
dc-start
dc-HN
LN-dc
HN-end
kj-sa
kj-HN
kj-dc", @"start,HN,dc,HN,end
start,HN,dc,HN,kj,HN,end
start,HN,dc,end
start,HN,dc,kj,HN,end
start,HN,end
start,HN,kj,HN,dc,HN,end
start,HN,kj,HN,dc,end
start,HN,kj,HN,end
start,HN,kj,dc,HN,end
start,HN,kj,dc,end
start,dc,HN,end
start,dc,HN,kj,HN,end
start,dc,end
start,dc,kj,HN,end
start,kj,HN,dc,HN,end
start,kj,HN,dc,end
start,kj,HN,end
start,kj,dc,HN,end
start,kj,dc,end", 1)]
        [InlineAutoMoqData(@"start-A
start-b
A-c
A-b
b-d
A-end
b-end", @"start,A,b,A,b,A,c,A,end
start,A,b,A,b,A,end
start,A,b,A,b,end
start,A,b,A,c,A,b,A,end
start,A,b,A,c,A,b,end
start,A,b,A,c,A,c,A,end
start,A,b,A,c,A,end
start,A,b,A,end
start,A,b,d,b,A,c,A,end
start,A,b,d,b,A,end
start,A,b,d,b,end
start,A,b,end
start,A,c,A,b,A,b,A,end
start,A,c,A,b,A,b,end
start,A,c,A,b,A,c,A,end
start,A,c,A,b,A,end
start,A,c,A,b,d,b,A,end
start,A,c,A,b,d,b,end
start,A,c,A,b,end
start,A,c,A,c,A,b,A,end
start,A,c,A,c,A,b,end
start,A,c,A,c,A,end
start,A,c,A,end
start,A,end
start,b,A,b,A,c,A,end
start,b,A,b,A,end
start,b,A,b,end
start,b,A,c,A,b,A,end
start,b,A,c,A,b,end
start,b,A,c,A,c,A,end
start,b,A,c,A,end
start,b,A,end
start,b,d,b,A,c,A,end
start,b,d,b,A,end
start,b,d,b,end
start,b,end", 2)]

        public void FindsPaths(
            string inputString,
            string expectedString,
            int limit,
            CavePathFinder sut)
        {
            var loader = new CaveLoader();
            var inputList = inputString.Split("\r\n")
                .ToList();
            var caves = loader.Load(inputList);
            var expectedCavePaths = expectedString.Split("\r\n")
                .ToList();
            var result = sut.Find(caves, limit);
            Assert.That(result.Count(), Is.EqualTo(expectedCavePaths.Count));
            foreach (var expectedCavePath in expectedCavePaths)
            {
                
                CollectionAssert.Contains(result, expectedCavePath);
            }
        }

    }


}
