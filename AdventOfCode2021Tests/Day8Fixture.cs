using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Days.Day7;
using AdventOfCode2021.Days.Day8;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AdventOfCode2021Tests
{
    [TestFixture]
    public class Day8Fixture
    {
        [Test]
        [InlineAutoMoqData("fdgacbe cefdb cefbgd gcbe", 2 )]
        public void FindsEasyDigits(
            string input,
            int expectedResult,
            OutputDigitCounter sut)
        {
            var inputList = input.Split(' ').Select(x => new Digit(x)).ToList();
            Assert.That(sut.Count(inputList), Is.EqualTo(expectedResult));

        }

        [Test]
        [InlineAutoMoqData("ab", true)]
        [InlineAutoMoqData("abc", true)]
        [InlineAutoMoqData("abcdefg", true)]
        [InlineAutoMoqData("abcd", true)]
        [InlineAutoMoqData("a", false)]
        public void IdentifiesEasyDigits(
            string input,
            bool expectedResult
            )
        {
            var digit = new Digit(input);
            Assert.That(digit.IsEasyDigit, Is.EqualTo(expectedResult));

        }

        [Test]
        [InlineAutoMoqData("ab", 1)]
        [InlineAutoMoqData("abc", 7)]
        [InlineAutoMoqData("abcdefg", 8)]
        [InlineAutoMoqData("abcd", 4)]
        [InlineAutoMoqData("a", -1)]
        public void ReturnsEasyDigits(
            string input,
            int expectedResult
        )
        {
            var digit = new Digit(input);
            Assert.That(digit.EasyValue, Is.EqualTo(expectedResult));

        }

        [Test]
        [InlineAutoMoqData("acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab", 0, "cagedb")]
        [InlineAutoMoqData("acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab", 1, "ab")]
        [InlineAutoMoqData("acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab", 2, "gcdfa")]
        [InlineAutoMoqData("acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab", 3, "fbcad")]
        [InlineAutoMoqData("acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab", 4, "eafb")]
        [InlineAutoMoqData("acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab", 5, "cdfbe")]
        [InlineAutoMoqData("acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab", 6, "cdfgeb")]
        [InlineAutoMoqData("acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab", 7, "dab")]
        [InlineAutoMoqData("acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab", 8, "acedgfb")]
        [InlineAutoMoqData("acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab", 9, "cefabd")]
        public void FindsDigit(
            string inputString,
            int digit,
            string expectedResult,
            DigitFinder sut
        )
        {
            var input = inputString.Split(' ').Select(x => new Digit(x.Trim())).ToList();
            Assert.That(sut.GetMappings(input)[digit], Is.EquivalentTo(expectedResult));

        }

        [Test]
        [InlineAutoMoqData("acedgb", "gab", true)]
        [InlineAutoMoqData("acedgb", "gaf", false)]
        public void Contains(
            string input,
            string charsToMatch,
            bool expectedResult
        )
        {
            var digit = new Digit(input);
            Assert.That(digit.Contains(charsToMatch.ToCharArray()), Is.EqualTo(expectedResult));

        }

        [Test]
        [InlineAutoMoqData("abc", "bca", true)]
        [InlineAutoMoqData("a", "a", true)]
        [InlineAutoMoqData("abc", "bag", false)]
        public void Equals(
            string input1,
            string input2,
            bool expectedResult
        )
        {
            var digit1 = new Digit(input1);
            var digit2 = new Digit(input2);
            Assert.That(digit1.Equals(digit2), Is.EqualTo(expectedResult));

        }

        [Test]
        [InlineAutoMoqData("acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab",
            "cagedb,ab,gcdfa,fbcad,eafb,cdfbe,cdfgeb,dab,acedgfb,cefabd")]
        public void FindsMappings(
            string inputString,
            string expectedResultString,
            DigitFinder sut
        )
        {
            var input = inputString.Split(' ').Select(x => new Digit(x.Trim())).ToList();
            var expectedResult = expectedResultString.Split(',').ToList();
            Assert.That(sut.GetMappings(input), Is.EquivalentTo(expectedResult));

        }

        [Test]
        [InlineAutoMoqData("acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab", "ba", 1)]
        [InlineAutoMoqData("acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab", "caegdb", 0)]
        [InlineAutoMoqData("acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab", "acegdfb", 8)]
        [InlineAutoMoqData("acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab", "bda", 7)]
        public void CanFind(
            string inputString,
            string input,
            int expectedResult,
            DigitFinder sut
        )
        {
            var mappingInput = inputString.Split(' ').Select(x => new Digit(x.Trim())).ToList();

            var mappings = sut.GetMappings(mappingInput);
            Assert.That(sut.FindDigit(mappings, input), Is.EqualTo(expectedResult));

        }
    }
}
