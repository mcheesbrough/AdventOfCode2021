using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Days.Day14;
using AdventOfCode2021.Model;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;

namespace AdventOfCode2021Tests
{
    [TestFixture]
    public class Day14Fixture
    {
        [Test]
        [InlineAutoMoqData(@"NNCB

CH -> B
HH -> N
CB -> H
NH -> C
HB -> C
HC -> B
HN -> C
NN -> C
BH -> H
NC -> B
NB -> B
BN -> B
BB -> N
BC -> B
CC -> N
CN -> C", "NC,1;CN,1;NB,1;BC,1;CH,1;HB,1", 1)]
        [InlineAutoMoqData(@"NCNBCHB

CH -> B
HH -> N
CB -> H
NH -> C
HB -> C
HC -> B
HN -> C
NN -> C
BH -> H
NC -> B
NB -> B
BN -> B
BB -> N
BC -> B
CC -> N
CN -> C","NB,2;BC,2;CC,1;CN,1;BB,2;CB,2;BH,1;HC,1", 1)]
        public void CanInsertPolymers(
            string inputString,
            string expectedString,
            int iterations,
            PolymerInserter sut)
        {
            var input = inputString
                .Split("\r\n\r\n")
                .ToList();
            var polymerTemplate = input[0];
            var loader = new PairInsertionRuleLoader();
            var expectedList = expectedString.Split(';');
            var expected = expectedList
                .ToDictionary(y => y.Split(',')[0], y => int.Parse(y.Split(',')[1]));


            var rules  = loader.LoadPairInsertionRule(input[1].Split("\r\n").ToList());

            var result = sut.Insert(polymerTemplate, rules, iterations);
            CollectionAssert.AreEquivalent(result, expected);

        }

    }
}
