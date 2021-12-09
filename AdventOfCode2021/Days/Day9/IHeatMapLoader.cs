using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Days.Day2;

namespace AdventOfCode2021.Days.Day9
{
    public interface IHeatMapLoader
    {
        Map Load(List<string> input);
    }

    public class HeatMapLoader : IHeatMapLoader
    {
        public Map Load(List<string> input)
        {
            var points = new List<HeatMapPoint>();
            for (var y = 0; y < input.Count; y++)
            {
                for (var x = 0; x < input[y].Length; x++)
                {
                    var point = new HeatMapPoint(new Coordinate(x, y), int.Parse(input[y][x].ToString()));
                    points.Add(point);
                }
            }

            return new Map(points, input[0].Length, input.Count);
        }
    }
}