using System;
using System.Linq;
using AdventOfCode2021.Days.Day1;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AdventOfCode2021Tests
{
    [TestFixture]
    public class Day1Fixture
    {
        [Test]
        [InlineAutoMoqData("100,101,102,101,103", 3)]
        [InlineAutoMoqData("100,99,90,101,100", 1)]
        public void FirstTestPlaceholder(
            string depthMeasurements, 
            int expectedNumberOfChanges, 
            DepthChangeCalc sut)
        {
            var measurements = depthMeasurements.Split(',').Select(x => int.Parse(x)).ToList();
            Assert.That(sut.NumberOfIncreases(measurements), Is.EqualTo(expectedNumberOfChanges));
        }

        [Test]
        [InlineAutoMoqData("1,2,3", 0)]
        [InlineAutoMoqData("1,2,3,4,5", 2)]
        [InlineAutoMoqData("1,2,0,1,2,3,1", 1)]
        public void ThreeMeasurementWindowIncrease(
            string depthMeasurements,
            int expectedNumberOfChanges,
            ThreeMeasurementDepthChangeCalc sut)
        {
            var measurements = depthMeasurements.Split(',').Select(x => int.Parse(x)).ToList();
            Assert.That(sut.NumberOfIncreases(measurements), Is.EqualTo(expectedNumberOfChanges));
        }
    }
}
