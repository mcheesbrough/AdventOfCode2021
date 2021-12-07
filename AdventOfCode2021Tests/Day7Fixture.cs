using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Days.Day7;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AdventOfCode2021Tests
{
    [TestFixture]
    public class Day7Fixture
    {
        [Test]
        [InlineAutoMoqData("16,1,2,0,4,2,7,1,2,14", 37 )]
        public void LineCalcsHorizontal(
            string input,
            int expectedBestPosition,
            BestCrabPositionFinder sut)
        {
            var positions = input.Split(',').Select(int.Parse).ToList();
            Assert.That(sut.Find(positions), Is.EqualTo(expectedBestPosition));

        }

        [Test]
        [InlineAutoMoqData("16,1,2,0,4,2,7,1,2,14", 168)]
        public void LineCalcsHorizontal(
            string input,
            int expectedBestPosition,
            BestCrabPositionFinderExponential sut)
        {
            var positions = input.Split(',').Select(int.Parse).ToList();
            Assert.That(sut.Find(positions), Is.EqualTo(expectedBestPosition));

        }
    }
}
