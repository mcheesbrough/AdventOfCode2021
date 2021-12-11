using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Days.Day1;
using AdventOfCode2021.Days.Day2;
using AdventOfCode2021.Days.Day3;
using AdventOfCode2021.Days.Day4;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AdventOfCode2021Tests
{
    [TestFixture]
    public class Day4Fixture
    {
        [Test]
        [InlineAutoMoqData(@"1,2,3,4,5

1 2 3 4 5
6 7 8 9 10
11 12 13 14 15
16 17 18 19 20
21 22 23 24 25", 1)]
        [InlineAutoMoqData(@"1,2,3,4,5

1 2 3 4 5
6 7 8 9 10
11 12 13 14 15
16 17 18 19 20
21 22 23 24 25

1 2 3 4 5
6 7 8 9 10
11 12 13 14 15
16 17 18 19 20
21 22 23 24 25", 2)]
        public void CreatesCorrectNumberOfCards(
            string input, 
            int expectedNumCards,
            BingoParser sut)
        {
            var cards = sut.Parse(input, out var numbers);
            Assert.That(cards.Count, Is.EqualTo(expectedNumCards));

        }

        [Test]
        [InlineAutoMoqData(@"1,2,3,4,5

1 2 3 4 5
6 7 8 9 10
11 12 13 14 15
16 17 18 19 20
21 22 23 24 25", 5)]
        [InlineAutoMoqData(@"1,2,3,4,5

1 2 3 4 5
6 7 8 9 10
11 12 13 14 15
16 17 18 19 20
21 22 23 24 25

1 2 3 4 5
6 7 8 9 10
11 12 13 14 15
16 17 18 19 20
21 22 23 24 25", 5)]
        public void GetsNumbers(
            string input,
            int expectedNumbers,
            BingoParser sut)
        {
            var cards = sut.Parse(input, out var numbers);
            Assert.That(numbers.Count, Is.EqualTo(expectedNumbers));

        }

        [Test]
        [InlineAutoMoqData("2 2 2 2 2 3 3 3 3 3 4 4 4 4 4 5 5 5 5 5 6 6 6 6 6", 5, 5, 10, 15, 20, 25, 30)]
        public void GetsRows(
            string input,
            int expectedRows,
            int expectedCols,
            int expectedSum1,
            int expectedSum2,
            int expectedSum3,
            int expectedSum4,
            int expectedSum5)
        {
            var numbers = input.Split(' ').Select(int.Parse).ToList();
            var card = new BingoCard(numbers);
            var rows = card.Rows.ToList();
            Assert.That(rows.Count, Is.EqualTo(expectedRows));
            Assert.That(rows[0].Count(), Is.EqualTo(expectedCols));
            Assert.That(rows[0].Sum(x => x.Number), Is.EqualTo(expectedSum1));
            Assert.That(rows[1].Sum(x => x.Number), Is.EqualTo(expectedSum2));
            Assert.That(rows[2].Sum(x => x.Number), Is.EqualTo(expectedSum3));
            Assert.That(rows[3].Sum(x => x.Number), Is.EqualTo(expectedSum4));
            Assert.That(rows[4].Sum(x => x.Number), Is.EqualTo(expectedSum5));

        }

        [Test]
        [InlineAutoMoqData("2 2 2 2 1 3 3 3 3 1 4 4 4 4 1 5 5 5 5 1 6 6 6 6 1", 5, 5, 20, 20, 20, 20, 5)]
        public void GetsCols(
            string input,
            int expectedRows,
            int expectedCols,
            int expectedSum1,
            int expectedSum2,
            int expectedSum3,
            int expectedSum4,
            int expectedSum5)
        {
            var numbers = input.Split(' ').Select(int.Parse).ToList();
            var card = new BingoCard(numbers);
            var cols = card.Cols.ToList();
            Assert.That(cols.Count, Is.EqualTo(expectedRows));
            Assert.That(cols[0].Count(), Is.EqualTo(expectedCols));
            Assert.That(cols[0].Sum(x => x.Number), Is.EqualTo(expectedSum1));
            Assert.That(cols[1].Sum(x => x.Number), Is.EqualTo(expectedSum2));
            Assert.That(cols[2].Sum(x => x.Number), Is.EqualTo(expectedSum3));
            Assert.That(cols[3].Sum(x => x.Number), Is.EqualTo(expectedSum4));
            Assert.That(cols[4].Sum(x => x.Number), Is.EqualTo(expectedSum5));

        }

        [Test]
        [InlineAutoMoqData("2 2 2 2 2 3 3 3 3 3 4 4 4 4 4 5 5 5 5 5 6 6 6 6 6", 3, 85)]
        public void CanCheckAndSumUnchecked(
            string input,
            int n,
            int expectedUncheckedSum)
        {
            var numbers = input.Split(' ').Select(int.Parse).ToList();
            var card = new BingoCard(numbers);
            card.Check(n);
            var sum = card.SumUnchecked;
            Assert.That(sum, Is.EqualTo(expectedUncheckedSum));
        }

    }
}
