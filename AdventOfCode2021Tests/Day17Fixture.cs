using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Days.Day17;
using AdventOfCode2021.Model;
using NUnit.Framework;

namespace AdventOfCode2021Tests
{
    [TestFixture]
    public class Day17Fixture
    {
        [Test]
        [InlineAutoMoqData("target area: x=20..30, y=-10..-5", "20,-5;30,-10")]
        [InlineAutoMoqData("target area: x=207..263, y=-115..-63", "207,-63;263,-115")]

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

        [Test]
        [InlineAutoMoqData("target area: x=20..30, y=-10..-5", "7,2", true, "28,-7", 3)]
        [InlineAutoMoqData("target area: x=20..30, y=-10..-5", "6,3", true, "21,-9", 6)]
        [InlineAutoMoqData("target area: x=20..30, y=-10..-5", "6,9", true, "21,-10", 45)]
        [InlineAutoMoqData("target area: x=20..30, y=-10..-5", "9,0", true, "30,-6", 0)]
        [InlineAutoMoqData("target area: x=20..30, y=-10..-5", "17,-4", false, "33,-9", 0)]
        [InlineAutoMoqData("target area: x=20..30, y=-10..-5", "0,10", false, "0,10", 0)]

        public void HitsTarget(
            string inputString,
            string startVel,
            bool expectedResult,
            string expectedFinalString,
            int expectedHighest,
            ProbeCalculator sut)
        {
            var reader = new ProbeInputReader();
            var startVelocity = Velocity.FromDescription(startVel);
            var input = reader.Read(inputString);
            var result = sut.HitsTarget(startVelocity, input[0], input[1], out var finalPos, out var highest);
            Assert.That(result, Is.EqualTo(expectedResult));
            Assert.That(finalPos, Is.EqualTo(Coordinate.FromDescription(expectedFinalString)));
            Assert.That(highest, Is.EqualTo(expectedHighest));
        }

        [Test]
        [InlineAutoMoqData("target area: x=20..30, y=-10..-5", 45)]

        public void FindsHighest(
            string inputString,
            int exoectedHighest,
            ProbeCalculator sut)
        {
            var reader = new ProbeInputReader();
            var input = reader.Read(inputString);
            var result = sut.FindHighestStart( input[0], input[1]);
            Assert.That(result, Is.EqualTo(exoectedHighest));
        }

        [Test]
        [InlineAutoMoqData("target area: x=20..30, y=-10..-5", 112)]

        public void FindsAll(
            string inputString,
            int expected,
            ProbeCalculator sut)
        {
            var reader = new ProbeInputReader();
            var input = reader.Read(inputString);
            var result = sut.FindAllVelocities(input[0], input[1]);
            Assert.That(result.Count, Is.EqualTo(expected));
        }
    }
}
