using System;
using System.Collections.Generic;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day2
{
    public interface IMover
    {
        SubState Move(SubState startState, MovementInstruction instruction);
        SubState Move(SubState startState, List<MovementInstruction> instructions);

    }

    public class MoverSimple : IMover
    {
        public virtual SubState Move(SubState startState, MovementInstruction instruction)
        {
            switch (instruction.Direction)
            {
                case Direction.Forward:
                {
                    return new SubState(new Coordinate(startState.Position.X + instruction.Amount, startState.Position.Y), 0);
                }
                case Direction.Down:
                {
                    return new SubState(new Coordinate(startState.Position.X, startState.Position.Y + instruction.Amount), 0);
                }
                case Direction.Up:
                {
                    return new SubState(new Coordinate(startState.Position.X, startState.Position.Y - instruction.Amount), 0);
                }
                default:
                {
                    throw new Exception($"Direction {instruction.Direction.ToString()} not recognised");
                }
            }
        }

        public SubState Move(SubState startPosition, List<MovementInstruction> instructions)
        {
            var currentPos = startPosition;
            foreach (var instruction in instructions)
            {
                currentPos = Move(currentPos, instruction);
            }

            return currentPos;
        }
    }

    public class MoverAdvanced : MoverSimple
    {
        public override SubState Move(SubState startState, MovementInstruction instruction)
        {
            switch (instruction.Direction)
            {
                case Direction.Forward:
                {
                    return new SubState(new Coordinate(startState.Position.X + instruction.Amount, startState.Position.Y + startState.Aim * instruction.Amount), startState.Aim);
                }
                case Direction.Down:
                {
                    return new SubState(startState.Position, startState.Aim + instruction.Amount);
                }
                case Direction.Up:
                {
                    return new SubState(startState.Position, startState.Aim - instruction.Amount);
                }
                default:
                {
                    throw new Exception($"Direction {instruction.Direction.ToString()} not recognised");
                }
            }
        }
    }
}