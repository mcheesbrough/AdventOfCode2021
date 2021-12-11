using System;
using AdventOfCode2021.Days.Day2;

namespace AdventOfCode2021.Model
{
    public interface IMapPoint: ICloneable
    {
        Coordinate Coordinate { get; }
    }
}