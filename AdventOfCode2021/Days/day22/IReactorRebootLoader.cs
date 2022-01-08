using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day22
{
    public interface IReactorRebootLoader
    {
        List<ReactorRebootInstruction> Load(List<string> input, bool discardOutside50 = true);
    }

    public class ReactorRebootLoader : IReactorRebootLoader
    {
        public List<ReactorRebootInstruction> Load(List<string> input, bool discardOutside50 = true)
        {
            var instructions = new List<ReactorRebootInstruction>();
            foreach (var line in input)
            {
                var parts = line.Split(' ');
                var ranges = parts[1].Split(',');
                var xAndYs = new List<int[]>();
                foreach (var range in ranges)
                {
                    var parsedRange = range.Replace("..", ".")[2..];
                    var xAndY = parsedRange.Split('.').Select(int.Parse).ToArray();
                    xAndYs.Add(xAndY);
                }

                var start = new Coordinate3D(xAndYs[0][0], xAndYs[1][0], xAndYs[2][0]);
                var end = new Coordinate3D(xAndYs[0][1], xAndYs[1][1], xAndYs[2][1]);
                var instruction = new ReactorRebootInstruction(
                    new ReactorRebootCuboid(start, end),
                    parts[0] == "on");
                //if (discardOutside50 && ((start.X < -50 || start.X > 50))) continue;
                instructions.Add(instruction);
            }

            return instructions;
        }
    }
}