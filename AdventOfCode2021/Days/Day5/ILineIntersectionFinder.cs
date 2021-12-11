using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Days.Day2;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day5
{
    public interface ILineIntersectionFinder
    {
        List<Coordinate> FindIntersections(List<Line> lines);
    }

    public class LineIntersectionFinder : ILineIntersectionFinder
    {
        public List<Coordinate> FindIntersections(List<Line> lines)
        {
            var paths = lines.SelectMany(x => x.Path);
            var counts = paths.GroupBy(x => x, (key, g) =>
                new
                {
                    Count = g.Count(),
                    Value = key
                });
            return counts.Where(x => x.Count > 1).Select(x => x.Value).ToList();
        }
    }
}