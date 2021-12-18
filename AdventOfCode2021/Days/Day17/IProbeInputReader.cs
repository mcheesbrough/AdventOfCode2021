using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day17
{
    public interface IProbeInputReader
    {
        List<Coordinate> Read(string input);
    }

    public class ProbeInputReader : IProbeInputReader
    {
        public List<Coordinate> Read(string input)
        {
            var parts = input
                .Replace("target area: x=", String.Empty)
                .Replace("y=", String.Empty)
                .Split(',')
                .Select(x => x.Trim())
                .SelectMany(x => x.Split(".."))
                .Select(int.Parse)
                .ToList();
            return new List<Coordinate>
            {
                new Coordinate(parts[0], parts[3]),
                new Coordinate(parts[1], parts[2])
            };
        }
    }
}