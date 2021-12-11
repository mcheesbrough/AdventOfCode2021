using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Days.Day2;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day5
{
    public interface ILineParser
    {
        List<Line> Parse(List<string> lineDefs);
    }

    public class LineParser : ILineParser
    {
        public List<Line> Parse(List<string> lineDefs)
        {
            return lineDefs
                .Select(x =>
                {
                    var coords = x.Replace(">", string.Empty)
                        .Split('-')
                        .Select(x => x.Trim())
                        .ToList();
                    return new Line(Coordinate.FromDescription(coords[0]), Coordinate.FromDescription(coords[1]));
                })
                .ToList();
        }
    }
}