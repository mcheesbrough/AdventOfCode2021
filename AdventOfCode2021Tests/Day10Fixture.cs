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
        [InlineAutoMoqData("(}", "}")]
        [InlineAutoMoqData("(}<", "}")]
        [InlineAutoMoqData("([]>", ">")]
        [InlineAutoMoqData("([]{}>", ">")]
        [InlineAutoMoqData("([]{(>})", ">")]
        [InlineAutoMoqData("([<}]{(}})", "}")]
        [InlineAutoMoqData("{([(<{}[<>[]}>{[]{[(<()>",  "}")]
        public void CanFindInvalidLine(
            string line,
            string expectedInvalidChar,
            SyntaxChecker sut)
        {
            var result = sut.FindInvalidCharacter(line);
            Assert.That(result.Value.ToString(), Is.EqualTo(expectedInvalidChar));
        }

        [Test]
        [InlineAutoMoqData("[(()[<>])]({[<{<<[]>>(")]
        public void ReturnsNullForUnfinishedValidLine(
            string line,
            SyntaxChecker sut)
        {
            var result = sut.FindInvalidCharacter(line);
            Assert.That(result, Is.Null);
        }

        [Test]
        [InlineAutoMoqData("[]{", "}")]
        [InlineAutoMoqData("[({(<(())[]>[[{[]{<()<>>", "}}]])})]")]
        public void CanComplete(
            string input,
            string expected,
            SyntaxCompleter sut)
        {
            var result = sut.Complete(input);
            var resultString = result
                .Aggregate("", (acc, v) => acc += v.Value);
            Assert.That(resultString, Is.EqualTo(expected));
        }

        [Test]
        [InlineAutoMoqData(@"[({(<(())[]>[[{[]{<()<>>
[(()[<>])]({[<{<<[]>>(
{([(<{}[<>[]}>{[]{[(<()>
(((({<>}<{<{<>}{[]{[]{}
[[<[([]))<([[{}[[()]]]
[{[{({}]{}}([{[{{{}}([]
{<[[]]>}<{[{[{[]{()[[[]
[<(<(<(<{}))><([]([]()
<{([([[(<>()){}]>(<<{{
<{([{{}}[<[[[<>{}]]]>[]]", 288957)]
        public void CanCalcCompletion(
            string input,
            int expected)
        {
            var inputList = input.Split("\r\n").ToList();
            var sut = new SyntaxCompleter(new SyntaxChecker());
            var result = sut.CalcCompletionScore(inputList);
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
