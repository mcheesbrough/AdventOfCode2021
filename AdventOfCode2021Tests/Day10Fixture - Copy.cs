using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Days.Day10;
using AdventOfCode2021.Days.Day11;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AdventOfCode2021Tests
{
    [TestFixture]
    public class Day11Fixture
    {
        [Test]
        [InlineAutoMoqData("(}", "}")]
        public void CanFindInvalidLine(
            string line,
            string expectedInvalidChar,
            ISomething sut)
        {
            var result = sut.Do(line);
            Assert.That(result, Is.EqualTo(expectedInvalidChar));
        }

    }
}
