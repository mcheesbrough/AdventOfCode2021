using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Days.Day1;
using AdventOfCode2021.Days.Day2;
using AdventOfCode2021.Days.Day3;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AdventOfCode2021Tests
{
    [TestFixture]
    public class Day3Fixture
    {
        [Test]
        [InlineAutoMoqData("000,101,111", "101")]
        public void CanFindMostCommon(
            string input, 
            string expectedOutput,
            GammaEpsilonFinder sut)
        {
            var binaries = input.Split(',').ToList();
            sut.Find(binaries, out var most, out var least);
            Assert.That(most, Is.EqualTo(expectedOutput));
        }

        [Test]
        [InlineAutoMoqData("000,101,111", "010")]
        public void CanFindLeastCommon(
            string input,
            string expectedOutput,
            GammaEpsilonFinder sut)
        {
            var binaries = input.Split(',').ToList();
            sut.Find(binaries, out var most, out var least);
            Assert.That(least, Is.EqualTo(expectedOutput));
        }

        [Test]
        [InlineAutoMoqData("101", "010", 10)]
        [InlineAutoMoqData("000", "010", 0)]
        public void CanCalculatePower(
            string gamma,
            string epsilon,
            int expectedOutput,
            string input,
            [Frozen] Mock<IGammaEpsilonFinder> geFinder,
            PowerCalculator sut)
        {
            var binaries = input.Split(',').ToList();
            geFinder.Setup(x => x.Find(It.IsAny<List<string>>(), out gamma, out epsilon));
            var result = sut.Calculate(binaries);
            Assert.That(result, Is.EqualTo(expectedOutput));
        }

        [Test]
        [InlineAutoMoqData("101", "010", 10)]
        [InlineAutoMoqData("000", "010", 0)]
        public void CanCalculateLifeSupport(
            string oxygen,
            string cO2,
            int expectedOutput,
            string input,
            [Frozen] Mock<IOxygenCo2Finder> geFinder,
            LifeSupportCalculator sut)
        {
            var binaries = input.Split(',').ToList();
            geFinder.Setup(x => x.Find(It.IsAny<List<string>>(), out oxygen, out cO2));
            var result = sut.Calculate(binaries);
            Assert.That(result, Is.EqualTo(expectedOutput));
        }


        [Test]
        [InlineAutoMoqData("000,101,111", "111")]
        [InlineAutoMoqData("00100,11110,10110,10111,10101,01111,00111,11100,10000,11001,00010,01010", "10111")]
        public void CanFindOxygen(
            string input,
            string expectedOutput,
            OxygenCo2Finder sut)
        {
            var binaries = input.Split(',').ToList();
            sut.Find(binaries, out var oxygen, out var cO2);
            Assert.That(oxygen, Is.EqualTo(expectedOutput));
        }

        [Test]
        [InlineAutoMoqData("000,101,111", "000")]
        [InlineAutoMoqData("00100,11110,10110,10111,10101,01111,00111,11100,10000,11001,00010,01010", "01010")]
        public void CanFindCO2(
            string input,
            string expectedOutput,
            OxygenCo2Finder sut)
        {
            var binaries = input.Split(',').ToList();
            sut.Find(binaries, out var oxygen, out var cO2);
            Assert.That(cO2, Is.EqualTo(expectedOutput));
        }
    }
}
