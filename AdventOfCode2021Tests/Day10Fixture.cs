using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Days.Day10;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AdventOfCode2021Tests
{
    [TestFixture]
    public class Day10Fixture
    {
        [Test]
        [InlineAutoMoqData("2113",  "0,0;1,0;0,1;1,1", "2,1,1,3")]
        public void LoadsHeatmap(
            string input,
            string expectedCoords,
            string expectedHeights,
            Something sut)
        {                                        

        }

    }
}
