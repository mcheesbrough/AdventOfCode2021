using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Days.Day19;
using AdventOfCode2021.Days.Day20;
using AdventOfCode2021.Days.Day21;
using AdventOfCode2021.Days.Day22;
using AdventOfCode2021.Days.Day23;
using AdventOfCode2021.Days.Day24;
using AdventOfCode2021.Days.Day25;
using AdventOfCode2021.Model;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AdventOfCode2021Tests
{
    [TestFixture]
    public class Day25Fixture
    {
        [Test]
        [InlineAutoMoqData(@"v.
.>", "01,00,00,10")]

        public void CanLoad(
            string inputString,
            string expectedString,
            SeaCucumberLoader sut)
        {
            var map = sut.Load(inputString.Split("\r\n").ToList());
            var expected = expectedString.Split(',').Select(x => x.ToCharArray().Select(y => int.Parse(y.ToString())).ToList()).ToList();
            Assert.That(map.Find(new Coordinate(0,0)).LeftRight, Is.EqualTo(expected[0][0]));
            Assert.That(map.Find(new Coordinate(0, 0)).UpDown, Is.EqualTo(expected[0][1]));
            Assert.That(map.Find(new Coordinate(1, 0)).LeftRight, Is.EqualTo(expected[1][0]));
            Assert.That(map.Find(new Coordinate(1, 0)).UpDown, Is.EqualTo(expected[1][1]));
            Assert.That(map.Find(new Coordinate(0, 1)).LeftRight, Is.EqualTo(expected[2][0]));
            Assert.That(map.Find(new Coordinate(0, 1)).UpDown, Is.EqualTo(expected[2][1]));
            Assert.That(map.Find(new Coordinate(1, 1)).LeftRight, Is.EqualTo(expected[3][0]));
            Assert.That(map.Find(new Coordinate(1, 1)).UpDown, Is.EqualTo(expected[3][1]));
        }

        [Test]
        [InlineAutoMoqData(@"v...>>.vv>
.vv>>.vv..
>>.>v>...v
>>v>>.>.v.
v>v.vv.v..
>.>>..v...
.vv..>.>v.
v.v..>>v.v
....v..v.>", 58)]

        public void CanMoveUntilStuck(
            string inputString,
            int expected,
            SeaCucumberMover sut)
        {
            var reader = new SeaCucumberLoader();
            var map = reader.Load(inputString.Split("\r\n").ToList());
            var result = sut.MoveUntilStuck(map);
            Assert.That(result, Is.EqualTo(expected));
        }

    }
}
