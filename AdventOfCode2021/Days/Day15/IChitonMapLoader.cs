using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Days.Day2;
using AdventOfCode2021.Days.Day9;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day15
{
    public interface IChitonMapLoader
    {
        Map<ChitonRiskPoint> Load(List<string> input);
        Map<ChitonRiskPoint> CombineMaps(Map<ChitonRiskPoint> map, int repeats);
    }

    public class ChitonMapLoader : IChitonMapLoader
    {
        public Map<ChitonRiskPoint> Load(List<string> input)
        {
            var points = new List<ChitonRiskPoint>();
            for (var y = 0; y < input.Count; y++)
            {
                for (var x = 0; x < input[y].Length; x++)
                {
                    var point = new ChitonRiskPoint(new Coordinate(x, y), int.Parse(input[y][x].ToString()));
                    points.Add(point);
                }
            }

            return new Map<ChitonRiskPoint>(points, input[0].Length, input.Count);
        }

        public Map<ChitonRiskPoint> CombineMaps(Map<ChitonRiskPoint> map, int repeats)
        {
            var newPoints = new List<ChitonRiskPoint>();
            foreach (var point in map.Points)
            {
                for (var x = 0; x < repeats; x++)
                {
                    for (var y = 0; y < repeats; y++)
                    {
                        var newRisk = (point.Risk + x + y) % 9;
                        if (newRisk == 0) newRisk = 9; 
                        var newPoint =
                            new ChitonRiskPoint(
                                new Coordinate(point.Coordinate.X + map.Width * x, point.Coordinate.Y + map.Height * y),
                                newRisk);
                        newPoints.Add(newPoint);
                    }
                }
            }

            return new Map<ChitonRiskPoint>(newPoints, map.Width * repeats, map.Height * repeats);
        }
    }
}