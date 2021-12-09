using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Days.Day9
{
    public interface IBasinFinder
    {
        List<int> Find(Map map);
    }

    public class BasinFinder : IBasinFinder
    {
        private readonly ILowPointFinder _lowPointFinder;

        public BasinFinder(ILowPointFinder lowPointFinder)
        {
            _lowPointFinder = lowPointFinder;
        }

        public List<int> Find(Map map)
        {
            var lows = _lowPointFinder.Find(map);
            var basinSizes = new List<int>();
            foreach (var low in lows)
            {
                var basinPoints = FindAdjacentsInBasin(map, new List<HeatMapPoint>(), new List<HeatMapPoint>(), low);
                basinSizes.Add(basinPoints.Count);
            }

            return basinSizes;
        }

        private List<HeatMapPoint> FindAdjacentsInBasin(Map map, List<HeatMapPoint> pointsFound, List<HeatMapPoint> pointsChecked, HeatMapPoint pointToCheck)
        {
            if (pointsChecked.Select(x => x.Point).Contains(pointToCheck.Point)) return pointsFound;
            pointsChecked.Add(pointToCheck);
            pointsFound.Add(pointToCheck);
            var adjacentsToLow = map.AdjacentPoints(pointToCheck).Where(x => x.Height < 9);
            foreach (var point in adjacentsToLow)
            {
                if (!pointsFound.Select(x => x.Point).Contains(point.Point))
                {
                    pointsFound = FindAdjacentsInBasin(map, pointsFound, pointsChecked, point);
                }
            }

            return pointsFound;
        }
    }
}