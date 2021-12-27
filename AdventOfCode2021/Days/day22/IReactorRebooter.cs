using System;
using System.Collections.Generic;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day22
{
    public interface IReactorRebooter
    {
        HashSet<Coordinate3D> Reboot(List<ReactorRebootInstruction> instructions);
    }

    public class ReactorRebooter : IReactorRebooter
    {
        public HashSet<Coordinate3D> Reboot(List<ReactorRebootInstruction> instructions)
        {
            var cubesOn = new HashSet<Coordinate3D>();
            
            foreach (var instruction in instructions)
            {
                for (var x = instruction.Cube.Start.X; x <= instruction.Cube.End.X; x++)
                {
                    for (var y = instruction.Cube.Start.Y; y <= instruction.Cube.End.Y; y++)
                    {
                        for (var z = instruction.Cube.Start.Z; z <= instruction.Cube.End.Z; z++)
                        {
                            var c = new Coordinate3D(x, y, z);
                            if (instruction.TurnOn)
                            {
                                if (!cubesOn.Contains(c))
                                    cubesOn.Add(c);
                            }
                            else
                            {
                                if (cubesOn.Contains(c))
                                    cubesOn.Remove(c);
                            }
                        }
                    }
                }
            }

            return cubesOn;
        }
    }
}