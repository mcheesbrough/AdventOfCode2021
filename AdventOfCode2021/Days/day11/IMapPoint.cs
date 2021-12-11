using System;
using AdventOfCode2021.Days.Day2;

namespace AdventOfCode2021.Days.Day11
{
    public interface IMapPoint: ICloneable
    {
        Coordinate Coordinate { get; }
    }
}