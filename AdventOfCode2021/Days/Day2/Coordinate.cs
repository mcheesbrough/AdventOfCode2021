using System;
using System.Linq;

namespace AdventOfCode2021.Days.Day2
{
    public class Coordinate
    {
        public int X { get; }
        public int Y { get; }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Coordinate FromDescription(string description)
        {
            var parts = description.Split(',').Select(x => int.Parse(x)).ToArray();
            if (parts.Length != 2) throw new Exception($"Coordinate description {description} does not have two values");
            return new Coordinate(parts[0], parts[1]);
        }
    }
}