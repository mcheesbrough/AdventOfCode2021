using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Days.Day7;
using AdventOfCode2021.Days.Day8;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AdventOfCode2021Tests
{
    [TestFixture]
    public class Day8Fixture
    {
        [Test]
        [InlineAutoMoqData("16,1,2,0,4,2,7,1,2,14", "a" )]
        public void TestName(
            string input,
            string expectedResult,
            Something sut)
        {
            var inputList = input.Split(',').Select(int.Parse).ToList();
            Assert.That(sut.Do("a"), Is.EqualTo(expectedResult));

        }

    }
}
