using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day22
{
    public interface IReactorRebooter
    {
        long Reboot(List<ReactorRebootInstruction> instructions);
    }

    public class ReactorRebooterOptimised : IReactorRebooter
    {
        public long Reboot(List<ReactorRebootInstruction> instructions)
        {
            var cuboidsOn = new List<ReactorRebootCuboid>();
            foreach (var instruction in instructions)
            {
                var cuboids = cuboidsOn.ToList();
                foreach (var cuboid in cuboids)
                {
                    var splitCuboids = cuboid.Split(instruction.Cuboid);
                    if (cuboid.Overlaps(instruction.Cuboid)) cuboidsOn.Remove(cuboid);
                    if (splitCuboids.Any()) cuboidsOn.AddRange(splitCuboids);
                }
                if (instruction.TurnOn) cuboidsOn.Add(instruction.Cuboid);

            }
            return cuboidsOn.Sum(c => c.Volume);
        }
    }


    public class ReactorRebooter : IReactorRebooter
    {
        public long Reboot(List<ReactorRebootInstruction> instructions)
        {
            var cubesOn = new HashSet<Coordinate3D>();
            
            foreach (var instruction in instructions)
            {
                for (var x = instruction.Cuboid.TopLeft.X; x <= instruction.Cuboid.BottomRight.X; x++)
                {
                    for (var y = instruction.Cuboid.TopLeft.Y; y <= instruction.Cuboid.BottomRight.Y; y++)
                    {
                        for (var z = instruction.Cuboid.TopLeft.Z; z <= instruction.Cuboid.BottomRight.Z; z++)
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

            return cubesOn.Count;
        }
    }
}