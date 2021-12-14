using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Days.Day14;
using AdventOfCode2021.Days.Day15;
using AdventOfCode2021.Model;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;

namespace AdventOfCode2021Tests
{
    [TestFixture]
    public class Day15Fixture
    {
        [Test]
        [InlineAutoMoqData(@"NNCB", "NC,1;CN,1;NB,1;BC,1;CH,1;HB,1", 1)]
 public void CanInsertPolymers(
            string inputString,
            string expectedString,
            int iterations,
            Something sut)
        {
            var input = inputString
                .Split("\r\n\r\n")
                .ToList();

        }

    }
}
