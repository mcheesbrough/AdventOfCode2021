using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Days.Day19;
using AdventOfCode2021.Days.Day20;
using AdventOfCode2021.Model;
using NUnit.Framework;

namespace AdventOfCode2021Tests
{
    [TestFixture]
    public class Day20Fixture
    {
        [Test]
        [InlineAutoMoqData(@"..#.#..#####.#.#.#.###.##.....###.##.#..###.####..#####..#....#..#..##..###..######.###...####..#..#####..##..#.#####...##.#.#..#.##..#.#......#.###.######.###.####...#.##.##..#..#..#####.....#.#....###..#.##......#.....#..#..#..##..#...##.######.####.####.#.#...#.......#..#.#.#...####.##.#......#..#...##.#.##..#...##.#.##..###.#......#.#.......#.#.#.####.###.##...#.....####.#..#..#.##.#....##..#.####....##...##..#...#......#.#.......#.......##..####..#...#.#.#...##..#.#..###..#####........#..####......#..#

#..#.
#....
##..#
..#..
..###", @".............
.............
.............
....##.##....
...#..#.#....
...##.#..#...
...####..#...
....#..##....
.....##..#...
......#.#....
.............
.............
.............")]

        public void CanLoad(
            string inputString,
            string expectedString,
            ImageProcessor sut)
        {
            var loader = new ImageLoader();
            loader.Load(inputString, out var algo, out var startImage);

            var result = sut.Process(startImage, algo);
            Assert.That(expectedString, Is.EqualTo(result.Print()));

        }

        [Test]
        [InlineAutoMoqData(@"..#.#..#####.#.#.#.###.##.....###.##.#..###.####..#####..#....#..#..##..###..######.###...####..#..#####..##..#.#####...##.#.#..#.##..#.#......#.###.######.###.####...#.##.##..#..#..#####.....#.#....###..#.##......#.....#..#..#..##..#...##.######.####.####.#.#...#.......#..#.#.#...####.##.#......#..#...##.#.##..#...##.#.##..###.#......#.#.......#.#.#.####.###.##...#.....####.#..#..#.##.#....##..#.####....##...##..#...#......#.#.......#.......##..####..#...#.#.#...##..#.#..###..#####........#..####......#..#

#..#.
#....
##..#
..#..
..###", 50, 3351)]

        public void CanApply50Times(
            string inputString,
            int times,
            int expected,
            ImageProcessor sut)
        {
            var loader = new ImageLoader();
            loader.Load(inputString, out var algo, out var startImage);

            var result = sut.Process(startImage, algo, times);
            Assert.That(expected, Is.EqualTo(result.Points.Count(p => p.Value)));

        }

    }
}
