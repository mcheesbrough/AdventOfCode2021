using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Days.Day17;
using AdventOfCode2021.Model;
using NUnit.Framework;

namespace AdventOfCode2021Tests
{
    [TestFixture]
    public class Day18Fixture
    {
        [Test]
        [InlineAutoMoqData("target area: x=20..30, y=-10..-5", "20,10;30,-5")]
        [InlineAutoMoqData("target area: x=207..263, y=-115..-63", "207,115;263,-63")]

        public void CanRead(
            string inputString,
            string expectedString,
            ProbeInputReader sut)
        {
            var expectedPoints = expectedString
                .Split(';')
                .Select(x => Coordinate.FromDescription(x))
                .ToList();
            var result = sut.Read(inputString);
            Assert.That(result, Is.EquivalentTo(expectedPoints));
        }

    }
}
