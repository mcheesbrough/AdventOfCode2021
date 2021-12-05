using System.Collections.Generic;

namespace AdventOfCode2021.Days.Day5
{
    public interface IIntersectionCounter
    {
        int Count(List<string> lineDefs);

    }

    public class IntersectionCounter : IIntersectionCounter
    {
        private readonly ILineParser _parser;
        private readonly ILineIntersectionFinder _intersectionFinder;

        public IntersectionCounter(ILineParser parser, ILineIntersectionFinder intersectionFinder)
        {
            _parser = parser;
            _intersectionFinder = intersectionFinder;
        }

        public int Count(List<string> lineDefs)
        {
            var lines = _parser.Parse(lineDefs);
            var intersections = _intersectionFinder.FindIntersections(lines);
            return intersections.Count;
        }
    }
}