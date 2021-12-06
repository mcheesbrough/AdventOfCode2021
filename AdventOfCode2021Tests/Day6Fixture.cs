using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Days.Day1;
using AdventOfCode2021.Days.Day2;
using AdventOfCode2021.Days.Day3;
using AdventOfCode2021.Days.Day4;
using AdventOfCode2021.Days.Day5;
using AdventOfCode2021.Days.Day6;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AdventOfCode2021Tests
{
    [TestFixture]
    public class Day6Fixture
    {
        [Test]
        [InlineAutoMoqData("3, 4, 3, 1, 2", "2,3,2,0,1" )]
        [InlineAutoMoqData("2,3,2,0,1", "1,2,1,6,0,8")]
        [InlineAutoMoqData("1,2,1,6,0,8", "0,1,0,5,6,7,8")]
        [InlineAutoMoqData("0,1,0,5,6,7,8", "6,0,6,4,5,6,7,8,8")]
        public void LineCalcsHorizontal(
            string initial,
            string expectedNextDay,
            LanternFishCalculator sut)
        {
            var initialFish = initial.Split(',').Select(int.Parse).ToList();
            var fishCounts = new List<FishCount>();
            for (var i = 0; i <= 8; i++)
            {
                fishCounts.Add(new FishCount { FishTimer = i, Count = 0 });
            }
            foreach (var f in initialFish)
            {
                var count = fishCounts.First(x => x.FishTimer == f);
                count.Count++;
            }

            var nextFish = expectedNextDay.Split(',').Select(int.Parse).ToList();
            var result = sut.Calculate(fishCounts);
            Assert.That(result.Sum(x => x.Count), Is.EqualTo(nextFish.Count));

        }


    }
}
