using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day15
{
    public interface IChitonPathFinder
    {
        long Find(Map<ChitonRiskPoint> map, Coordinate start);
    }

    public class ChitonPathFinder : IChitonPathFinder
    {
        public long Find(Map<ChitonRiskPoint> map, Coordinate startCoords)
        {
            var startPoint = map.Find(startCoords);
            var unvisited = new List<ChitonRiskPoint>{startPoint};
            startPoint.RiskToHere = 0;
            var endPoint = map.Find(new Coordinate(map.Width - 1, map.Height - 1));
            while (true)
            {
                var currentPoint = unvisited
                    .OrderBy(x => x.RiskToHere)
                    .First();

                var adjacentAny = map
                    .AdjacentPoints(currentPoint)
                    .ToList();
                var adjacent    = adjacentAny.Where(x => !x.IsVisited).ToList();

                foreach (var point in adjacent)
                {
                    var distToPoint = currentPoint.RiskToHere + point.Risk;
                    if (point.RiskToHere > distToPoint) point.RiskToHere = distToPoint;
                    if (!unvisited.Contains(point)) unvisited.Add(point);
                }

                currentPoint.IsVisited = true;
                unvisited.Remove(currentPoint);
                
                if (currentPoint == endPoint) break;
            }

            return endPoint.RiskToHere;

        }
        
    }
}