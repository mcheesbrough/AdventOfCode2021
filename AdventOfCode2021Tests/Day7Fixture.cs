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
        [InlineAutoMoqData("3, 4, 3, 1, 2", "2,3,2,0,1" )]
        public void LineCalcsHorizontal(
            string initial,
            string expectedNextDay,
            Something sut)
        {
            
            Assert.That(sut.Do("a"), Is.EqualTo("b"));

        }


    }
}
