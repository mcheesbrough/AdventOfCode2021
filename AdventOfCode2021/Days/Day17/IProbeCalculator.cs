using System;
using System.Collections.Generic;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day17
{
    public interface IProbeCalculator
    {
        int FindHighestStart(Coordinate targetTL, Coordinate TargetBR);
        List<Velocity> FindAllVelocities(Coordinate targetTL, Coordinate targetBR);
    }

    public class ProbeCalculator : IProbeCalculator
    {
        public int FindHighestStart(Coordinate targetTL, Coordinate targetBR)
        {
            var highest = 0;
            for (int y = 5000; y >= -50; y--)
            {
                for (int x = 0; x < 100; x++)
                {
                    var velocityToTry = new Velocity(x, y);
                    if (HitsTarget(velocityToTry, targetTL, targetBR, out var finalPos, out var maxHeight))
                    {
                        if (maxHeight > highest)
                        {
                            highest = maxHeight;
                        }
                    }

                }
            }

            return highest;
        }

        public List<Velocity> FindAllVelocities(Coordinate targetTL, Coordinate targetBR)
        {
            var velocities = new List<Velocity>();
            for (int y = 200; y >= targetBR.Y; y--)
            {
                for (int x = targetBR.X; x > 0; x--)
                {
                    
                    var velocityToTry = new Velocity(x, y);
                    if (HitsTarget(velocityToTry, targetTL, targetBR, out var finalPos, out var maxHeight))
                    {
                        velocities.Add(velocityToTry);
                    }
                    else
                    {
                        if (finalPos.X < targetTL.X) break;
                    }

                }
            }

            return velocities;
        }

        public bool HitsTarget(Velocity velocityToTry, Coordinate targetTL, Coordinate targetBR, out Coordinate currentPos, out int highest)
        {
            var velocity = (Velocity)velocityToTry.Clone();
            currentPos = new Coordinate(0, 0);
            highest = 0;
            while (currentPos.X <= targetBR.X && currentPos.Y >= targetBR.Y)
            {
                highest = Math.Max(highest, currentPos.Y);
                currentPos = currentPos.Add(velocity);
                if (IsInArea(currentPos, targetTL, targetBR)) return true;

                velocity = new Velocity(Math.Max(0, velocity.X - 1), velocity.Y - 1);
                if (velocity.X == 0 && currentPos.X < targetTL.X) return false;
            }

            return false;
        }

        private bool IsInArea(Coordinate c, Coordinate targetTL, Coordinate targetBR)
        {
            return c.X >= targetTL.X
                   && c.X <= targetBR.X
                   && c.Y <= targetTL.Y
                   && c.Y >= targetBR.Y;
        }
    }
}