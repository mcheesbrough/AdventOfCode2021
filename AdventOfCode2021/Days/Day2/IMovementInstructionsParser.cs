using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Days.Day2
{
    public interface IMovementInstructionsParser
    {
        List<MovementInstruction> Parse(IEnumerable<string> instructionsToParse);
    }

    public class MovementInstructionsParser : IMovementInstructionsParser
    {
        public List<MovementInstruction> Parse(IEnumerable<string> instructionsToParse)
        {
            return instructionsToParse.Select(x =>
            {
                var parts = x.Split(' ');
                if (parts.Length != 2) throw new Exception($"Instruction {x} does not have two parts");
                if (!Enum.TryParse(parts[0], true, out Direction dir)) throw new Exception($"Cannot parse direction {parts[0]}");
                if (!int.TryParse(parts[1], out var amount)) throw new Exception($"Cannot parse amount {parts[1]}");
                return new MovementInstruction(dir, amount);
            }).ToList();
        }
    }
}