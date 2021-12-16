using System;
using System.Collections.Generic;
using AdventOfCode2021.Model;
using NUnit.Framework;

namespace AdventOfCode2021Tests
{
    [TestFixture]
    public class Day17Fixture
    {
        [Test]
        [InlineAutoMoqData("EE00D40C823060", "11101110000000001101010000001100100000100011000001100000")]
        public void CanConvertHexToBinString(
            string inputString,
            string expectedString)
        {
            Assert.That(Binary.BinaryStringFromHex(inputString), Is.EqualTo(expectedString));
        }

    }
}
