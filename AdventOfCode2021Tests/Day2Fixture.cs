using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Days.Day1;
using AdventOfCode2021.Days.Day2;
using AdventOfCode2021.Model;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AdventOfCode2021Tests
{
    [TestFixture]
    public class Day2Fixture
    {
        [Test]
        [InlineAutoMoqData("2,3", 2,3)]
        public void CanCreateCoordinateFromDescription(
            string description, 
            int expectedX,
            int expectedY)
        {
            var coordinate = Coordinate.FromDescription(description);
            Assert.That(coordinate.X, Is.EqualTo(expectedX));
            Assert.That(coordinate.Y, Is.EqualTo(expectedY));
        }

        [Test]
        [InlineAutoMoqData("0,0", Direction.Forward, 5, 5, 0)]
        [InlineAutoMoqData("0,0", Direction.Down, 5, 0, 5)]
        [InlineAutoMoqData("10,11", Direction.Up, 5, 10, 6)]
        public void CanCalculateNewPosition(
            string startPos,
            Direction direction,
            int amount,
            int expectedX,
            int expectedY,
            MoverSimple sut)
        {
            var start = new SubState(Coordinate.FromDescription(startPos), 0);
            var newState = sut.Move(start, new MovementInstruction(direction, amount));
            Assert.That(newState.Position.X, Is.EqualTo(expectedX));
            Assert.That(newState.Position.Y, Is.EqualTo(expectedY));
        }

        [Test]
        [InlineAutoMoqData("0,0", Direction.Forward, 5, Direction.Down, 3, 5, 3)]
        public void CanExecuteInstructions(
            string startPos,
            Direction direction1,
            int amount1,
            Direction direction2,
            int amount2,
            int expectedX,
            int expectedY,
            MoverSimple sut)
        {
            var start = new SubState(Coordinate.FromDescription(startPos), 0);
            var moves = new List<MovementInstruction>
            {
                new MovementInstruction(direction1, amount1),
                new MovementInstruction(direction2, amount2)
            };
            var newState = sut.Move(start, moves);
            Assert.That(newState.Position.X, Is.EqualTo(expectedX));
            Assert.That(newState.Position.Y, Is.EqualTo(expectedY));
        }

        [Test]
        [InlineAutoMoqData("forward 4,down 2", Direction.Forward, 4, Direction.Down, 2)]
        public void CanParseInstructions(
            string instructionsAsString,
            Direction expectedDir1,
            int expectedAmount1,
            Direction expectedDir2,
            int expectedAmount2,
            MovementInstructionsParser sut)
        {
            var instructions = instructionsAsString.Split(',');
            var parsedInstructions = sut.Parse(instructions);
            Assert.That(parsedInstructions[0].Direction, Is.EqualTo(expectedDir1));
            Assert.That(parsedInstructions[0].Amount, Is.EqualTo(expectedAmount1));
            Assert.That(parsedInstructions[1].Direction, Is.EqualTo(expectedDir2));
            Assert.That(parsedInstructions[1].Amount, Is.EqualTo(expectedAmount2));
        }

        [Test]
        [InlineAutoMoqData("0,0",0, Direction.Forward, 5, Direction.Down, 5, Direction.Forward, 8, 13, 40)]
        [InlineAutoMoqData("13,40", 5, Direction.Up, 3, Direction.Down, 8, Direction.Forward, 2, 15, 60)]
        public void CanExecuteInstructionsAdvanced(
            string startPos,
            int startAim,
            Direction direction1,
            int amount1,
            Direction direction2,
            int amount2,
            Direction direction3,
            int amount3,
            int expectedX,
            int expectedY,
            MoverAdvanced sut)
        {
            var start = new SubState(Coordinate.FromDescription(startPos), startAim);
            var moves = new List<MovementInstruction>
            {
                new MovementInstruction(direction1, amount1),
                new MovementInstruction(direction2, amount2),
                new MovementInstruction(direction3, amount3)
            };
            var newState = sut.Move(start, moves);
            Assert.That(newState.Position.X, Is.EqualTo(expectedX));
            Assert.That(newState.Position.Y, Is.EqualTo(expectedY));
        }
    }
}
