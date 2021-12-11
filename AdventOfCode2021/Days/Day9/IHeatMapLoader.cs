using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Days.Day2;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day9
{
    public interface IHeatMapLoader
    {
        Map<HeatMapPoint> Load(List<string> input);
    }

    public class HeatMapLoader : IHeatMapLoader
    {
        public Map<HeatMapPoint> Load(List<string> input)
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

            return new Map<HeatMapPoint>(points, input[0].Length, input.Count);
        }
    }
}