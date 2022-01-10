using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Days.Day19;
using AdventOfCode2021.Days.Day20;
using AdventOfCode2021.Days.Day21;
using AdventOfCode2021.Model;
using NUnit.Framework;

namespace AdventOfCode2021Tests
{
    [TestFixture]
    public class Day21Fixture
    {
        [Test]
        [InlineAutoMoqData(1, 1)]
        [InlineAutoMoqData(2, 2)]
        [InlineAutoMoqData(6, 6)]
        [InlineAutoMoqData(101, 1)]
        [InlineAutoMoqData(106, 6)]
        [InlineAutoMoqData(201, 1)]

        public void CanRoll(
            int numRolls,
            int expected)
        {

            var dice = new Dice();
            var result = 0;
            for (int i = 0; i < numRolls; i++)
            {
                result = dice.Roll();
            }
            Assert.That(result, Is.EqualTo(expected));

        }

        [Test]
        [InlineAutoMoqData(4, 8, 745, 993)]

        public void CanPlay(
            int player1Start,
            int player2Start,
            long expectedLoser,
            long expectedDiceRolls,
            DiracDice sut)
        {

            sut.Play(player1Start, player2Start, 1000, out var loser, out var winner, out var rolls);
            Assert.That(loser, Is.EqualTo(expectedLoser));
            Assert.That(expectedDiceRolls, Is.EqualTo(expectedDiceRolls));

        }

        [Test]
        [InlineAutoMoqData(4, 8, 444356092776315)]

        public void CanPlayQuantum(
            int player1Start,
            int player2Start,
            long expectedWins,
            DiracDice sut)
        {

            var highestWins = sut.PlayQuantum(player1Start, player2Start, 21);
            Assert.That(expectedWins, Is.EqualTo(highestWins));

        }

    }
}
