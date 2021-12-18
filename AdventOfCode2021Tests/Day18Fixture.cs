using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Days.Day18;
using AdventOfCode2021.Model;
using NUnit.Framework;

namespace AdventOfCode2021Tests
{
    [TestFixture]
    public class Day18Fixture
    {
        [Test]
        [InlineAutoMoqData("[1,2]")]
        [InlineAutoMoqData("[[3,2],2]")]
        [InlineAutoMoqData("[1,[4,5]]")]
        [InlineAutoMoqData("[[3,2],[4,5]]")]
        [InlineAutoMoqData("[[3,2],[4,5]]")]
        [InlineAutoMoqData("[[3,[6,7]],[[7,9],5]]")]
        [InlineAutoMoqData("[10,2]")]
        [InlineAutoMoqData("[[3,2],20]")]

        public void CanLoad(
            string inputString)
        {
            var result = SnailFishNumber.FromDescription(inputString);
            Assert.That(result.ToString, Is.EquivalentTo(inputString));
        }

        [Test]
        [InlineAutoMoqData("[1,2]", "0")]
        [InlineAutoMoqData("[[3,2],2]", "0,1")]
        [InlineAutoMoqData("[1,[4,5]]", "0,1")]
        [InlineAutoMoqData("[[3,2],[4,5]]", "0,1,1")]
        [InlineAutoMoqData("[[3,[6,7]],[[7,9],5]]","0,1,2,1,2")]

        public void CenGetDepths(
            string inputString,
            string expected)
        {
            var result = SnailFishNumber.FromDescription(inputString).Depths();
            Assert.That(result.Select(x => x.Item1), Is.EquivalentTo(expected.Split(',').Select(int.Parse)));
        }

        [Test]
        [InlineAutoMoqData(@"[1,2]
[2,3]", "[[1,2],[2,3]]")]
        [InlineAutoMoqData(@"[1,1]
[2,2]
[3,3]
[4,4]", "[[[[1,1],[2,2]],[3,3]],[4,4]]")]
        [InlineAutoMoqData(@"[1,1]
[2,2]
[3,3]
[4,4]
[5,5]", "[[[[3,0],[5,3]],[4,4]],[5,5]]")]
        [InlineAutoMoqData(@"[1,1]
[2,2]
[3,3]
[4,4]
[5,5]
[6,6]", "[[[[5,0],[7,4]],[5,5]],[6,6]]")]
        [InlineAutoMoqData(@"[[[0,[4,5]],[0,0]],[[[4,5],[2,6]],[9,5]]]
[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]
[[2,[[0,8],[3,4]]],[[[6,7],1],[7,[1,6]]]]
[[[[2,4],7],[6,[0,5]]],[[[6,8],[2,8]],[[2,1],[4,5]]]]
[7,[5,[[3,8],[1,4]]]]
[[2,[2,2]],[8,[8,1]]]
[2,9]
[1,[[[9,3],9],[[9,0],[0,7]]]]
[[[5,[7,4]],7],1]
[[[[4,2],2],6],[8,7]]", "[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]")]

        public void CanAdd(
            string inputString,
            string expected)
        {
            var numbers = inputString.Split("\r\n")
                .Select(x => SnailFishNumber.FromDescription(x))
                .ToList();
            var result = numbers[0].Add(numbers[1]);
            for (int i = 2; i < numbers.Count; i++)
            {
                result = result.Add(numbers[i]);
            }
            Assert.That(result.ToString, Is.EquivalentTo(expected));
        }

        [Test]
        [InlineAutoMoqData("[[[[[9,8],1],2],3],4]", "[[[[0,9],2],3],4]")]
        [InlineAutoMoqData("[7,[6,[5,[4,[3,2]]]]]", "[7,[6,[5,[7,0]]]]")]
        [InlineAutoMoqData("[[6,[5,[4,[3,2]]]],1]", "[[6,[5,[7,0]]],3]")]
        [InlineAutoMoqData("[[3,[2,[1,[7,3]]]],[6,[5,[4,[3,2]]]]]", "[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]")]
        [InlineAutoMoqData("[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]", "[[3,[2,[8,0]]],[9,[5,[7,0]]]]")]

        public void CanExplode(
            string inputString,
            string expected)
        {
            var input = SnailFishNumber.FromDescription(inputString);
            var result = input.Explode(out var newNumber);
            Assert.That(newNumber.ToString, Is.EquivalentTo(expected));
        }

        [Test]
        [InlineAutoMoqData("[[[[0,7],4],[15,[0,13]]],[1,1]]", "[[[[0,7],4],[[7,8],[0,13]]],[1,1]]")]
        [InlineAutoMoqData("[[[[0,7],4],[[7,8],[0,13]]],[1,1]]", "[[[[0,7],4],[[7,8],[0,[6,7]]]],[1,1]]")]

        public void CanSplit(
            string inputString,
            string expected)
        {
            var input = SnailFishNumber.FromDescription(inputString);
            var result = input.Split(out var newNumber);
            Assert.That(newNumber.ToString, Is.EquivalentTo(expected));
        }

        [Test]
        [InlineAutoMoqData("[1,[2,[3,[4,[5,6]]]]]", "[4,[5,6]]")]
        [InlineAutoMoqData("[1,[2,[3,[[5,6],4]]]]", "[3,[[5,6],4]]")]
        [InlineAutoMoqData("[[[[[5,6],4],3],2],1]", null)]
        [InlineAutoMoqData("[[8,9],[[[[5,6],4],3],2]]", "[8,9]")]

        public void CanFindFirstToLeft(
            string inputString,
            string expected)
        {
            var input = SnailFishNumber.FromDescription(inputString);
            var firstDepthFour = input.Depths().First(x => x.Item1 == 4).Item2;
            var result = firstDepthFour.FindFirstRegularToLeft();
            if (expected == null)
                Assert.IsNull(result);
            else
                Assert.That(result.Item1.ToString(), Is.EqualTo(expected));
        }

        [Test]
        [InlineAutoMoqData("[1,[2,[3,[4,[5,6]]]]]", null)]
        [InlineAutoMoqData("[1,[2,[3,[[5,6],4]]]]", "[[5,6],4]")]
        [InlineAutoMoqData("[[[[[5,6],4],3],2],1]", "[[5,6],4]")]
        [InlineAutoMoqData("[[2,[3,[4,[5,6]]]],[8,9]]", "[8,9]")]

        public void CanFindFirstToRight(
            string inputString,
            string expected)
        {
            var input = SnailFishNumber.FromDescription(inputString);
            var firstDepthFour = input.Depths().First(x => x.Item1 == 4).Item2;
            var result = firstDepthFour.FindFirstRegularToRight();
            if (expected == null)
                Assert.IsNull(result);
            else
                Assert.That(result.Item1.ToString(), Is.EqualTo(expected));
        }

        [Test]
        [InlineAutoMoqData("[[[[9,8],10],12],13]", "[[9,8],10]")]
        [InlineAutoMoqData("[[[[9,8],7],12],13]", "[[[9,8],7],12]")]
        [InlineAutoMoqData("[[1,2],[9,10]]", "[9,10]")]
        [InlineAutoMoqData("[[1,20],[9,10]]", "[1,20]")]
        [InlineAutoMoqData("[[21,1],[9,10]]", "[21,1]")]
        [InlineAutoMoqData("[[[[0,7],4],[[7,8],[[0,0],13]]],[1,1]]", "[[0,0],13]")]

        public void CanFindFirstOverNine(
            string inputString,
            string expected)
        {
            var input = SnailFishNumber.FromDescription(inputString);
            var result = input.FindFirstOverNine();
            if (expected == null)
                Assert.IsNull(result);
            else
                Assert.That(result.ToString, Is.EquivalentTo(expected));
        }

        [Test]
        [InlineAutoMoqData("[[1,2],[[3,4],5]]", 143)]
        [InlineAutoMoqData("[[[[0,7],4],[[7,8],[6,0]]],[8,1]]", 1384)]
        [InlineAutoMoqData("[[[[1,1],[2,2]],[3,3]],[4,4]]", 445)]
        [InlineAutoMoqData("[[[[3,0],[5,3]],[4,4]],[5,5]]", 791)]
        [InlineAutoMoqData("[[[[5,0],[7,4]],[5,5]],[6,6]]", 1137)]
        [InlineAutoMoqData("[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]", 3488)]

        public void CanCalcMag(
            string inputString,
            int expected)
        {
            var input = SnailFishNumber.FromDescription(inputString);
            var result = input.Magnitude;
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
