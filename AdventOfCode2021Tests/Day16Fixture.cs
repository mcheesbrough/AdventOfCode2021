using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode2021.Days.Day16;
using AdventOfCode2021.Model;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;

namespace AdventOfCode2021Tests
{
    [TestFixture]
    public class Day16Fixture
    {
        [Test]
        [InlineAutoMoqData(@"1163751742", 40)]
        public void FindPaths(
            string inputString,
            int expectedPathSum,
            Something sut)
        {
            var input = inputString
                .Split("\r\n")
                .ToList();
        }

    }
}
