using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Days.Day9
{
    public interface ILowPointFinder
    {
        List<HeatMapPoint> Find(Map<HeatMapPoint> map);
    }

    public class LowPointFinder : ILowPointFinder
    {
        public List<HeatMapPoint> Find(Map<HeatMapPoint> map)
        {
            var lows = map.Points
                .Where(p => p.Height < map.AdjacentPoints(p).Min(p2 => p2.Height))
                .ToList();
            return lows;
        }
    }
}