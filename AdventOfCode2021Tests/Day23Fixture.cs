using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Days.Day19;
using AdventOfCode2021.Days.Day20;
using AdventOfCode2021.Days.Day21;
using AdventOfCode2021.Days.Day22;
using AdventOfCode2021.Days.Day23;
using AdventOfCode2021.Model;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AdventOfCode2021Tests
{
    [TestFixture]
    public class Day23Fixture
    {
        [Test]
        [InlineAutoMoqData("3, 3, 1", "", 0)]
        [InlineAutoMoqData("3, 3, 1;3, 2, 10", "", 0)]
        [InlineAutoMoqData("5, 3, 1;5,2,10;5,4,10;5,5,10", "", 0)]
        [InlineAutoMoqData("5, 3, 1;3,5,10", "1,1;2,1;4,1;6,1;8,1;10,1;11,1", 37)]
        [InlineAutoMoqData("5, 3, 1;2,1,10;3,5,10", "4,1;6,1;8,1;10,1;11,1", 26)]
        [InlineAutoMoqData("5,5, 1", "3,5", 10)]
        [InlineAutoMoqData("6, 1, 1", "3,5", 7)]
        [InlineAutoMoqData("6, 1, 1;3,5,1", "3,4", 6)]
        [InlineAutoMoqData("1, 1, 1000;10,1,100", "9,5", 12000)]
        [InlineAutoMoqData("1, 1, 1000;9,3,1000", "9,2", 9000)]
        [InlineAutoMoqData("1, 1, 1000;9,3,100", "", 0)]

        public void CanFindPossibleMoves(
            string inputString,
            string expectedCoordsString,
            int energyToMove)
        {
            var input = inputString.Split(';').Select(Amphipod.FromDescription).ToList();
            var board = new AmphipodBoard(input);
            var expectedMoves = new List<Coordinate>();
            if (!string.IsNullOrEmpty(expectedCoordsString)) 
                expectedMoves = expectedCoordsString.Split(';').Select(Coordinate.FromDescription).ToList();
            var possibleMoves = board.PossibleMoves
                .Where(a => a.Amphipod.Position == input[0].Position).ToList();

            Assert.IsTrue(possibleMoves.All(a => a.Amphipod.Energy == input[0].Energy));
            CollectionAssert.AreEquivalent(expectedMoves, possibleMoves.Select(a => a.Target));
            Assert.That(energyToMove, Is.EqualTo(possibleMoves.Sum(m => m.EnergyRequired)));
        }


        [Test]
        [InlineAutoMoqData("3, 3, 1;3,2,10;5,3,1000;5,2,100;7,3,100;7,2,10;9,3,1;9,2,1000", 12521)]

        public void CanArrange(
            string inputString,
            int expected,
            AmphipodArranger sut)
        {
            var input = inputString.Split(';').Select(Amphipod.FromDescription).ToList();
            var board = new AmphipodBoard(input);
            var expectedMoves = new List<Coordinate>();
            var result = sut.Arrange(input);
            Assert.That(expected, Is.EqualTo(result));
        }
    }
}
