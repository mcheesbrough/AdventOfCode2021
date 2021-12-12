using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Days.Day10;
using AdventOfCode2021.Days.Day11;
using AdventOfCode2021.Days.Day12;
using AdventOfCode2021.Days.Day13;
using AdventOfCode2021.Days.Day2;
using AdventOfCode2021.Days.Day9;
using AdventOfCode2021.Model;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AdventOfCode2021Tests
{
    [TestFixture]
    public class Day13Fixture
    {
        [Test]
        [InlineAutoMoqData(@"start-A", "start,A")]
        public void CanLoadNames(
            string inputString,
            string expectedString,
            Something sut)
        {
        }

    }
}
