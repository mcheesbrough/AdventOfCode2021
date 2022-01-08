using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day25
{
    public interface ISeaCucumberLoader
    {
        Map<SeaCucumberPosition> Load(List<string> input);
    }

    public class SeaCucumberLoader : ISeaCucumberLoader
    {
        public Map<SeaCucumberPosition> Load(List<string> input)
        {
            var points = new List<SeaCucumberPosition>();
            for (var y = 0; y < input.Count; y++)
            {
                var scLine = input[y].ToCharArray().Select((p, x) => 
                    p switch
                    {
                        '>' => new SeaCucumberPosition(new Coordinate(x, y), 0, 1),
                        'v' => new SeaCucumberPosition(new Coordinate(x, y), 1, 0),
                        _ => new SeaCucumberPosition(new Coordinate(x, y), 0, 0)
                    }).ToList();
                points.AddRange(scLine);
            }

            return new Map<SeaCucumberPosition>(points, input[0].Length, input.Count);
        }
    }
}