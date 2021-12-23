using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day20
{
    public interface IImageProcessor
    {
        Map<Pixel> Process(Map<Pixel> input, string enhancementAlgo, int numTimes);
        Map<Pixel> Process(Map<Pixel> input, string enhancementAlgo);
    }



    public class ImageProcessor : IImageProcessor
    {
        public Map<Pixel> Process(Map<Pixel> input, string enhancementAlgo, int numTimes)
        {
            var output = Process(input, enhancementAlgo);

            for (var i = 1; i <= 49; i++)
            {
                Console.Write(i);
                output = Process(output, enhancementAlgo);
            }

            return output;
        }

        public Map<Pixel> Process(Map<Pixel> input, string enhancementAlgo)
        {
            var outputPixels = new List<Pixel>();
            for (var x = 0; x < input.Width; x++)
            {
                for (var y = 0; y < input.Height; y++)
                {
                    var adjacents = input
                        .AdjacentIncludingDiagonalPoints(new Pixel(new Coordinate(x, y), false));
                    var centre = input.Find(new Coordinate(x, y));
                    adjacents.Add(centre);

                    var fullSet = CompleteEdge(adjacents, centre)
                        .OrderBy(p => p.Coordinate.Y)
                        .ThenBy(p => p.Coordinate.X);
                    var binary = new string(fullSet.Select(p => p.Value ? '1' : '0').ToArray());
                    var index = Convert.ToInt32(binary, 2);
                    var decode = enhancementAlgo[index];
                    outputPixels.Add(new Pixel(new Coordinate(x, y), decode == '1'));
                }
            }

            var map = new Map<Pixel>(outputPixels, input.Width, input.Height);
            var output = ImageLoader.Expand(map, 2, IsEdgeOnOrOff(map));
            return output;
        }

        private List<Pixel> CompleteEdge(List<Pixel> start, Pixel centre)
        {
            var result = start.ToList();
            for (int x = centre.Coordinate.X - 1; x <= centre.Coordinate.X+1; x++)
            {
                for (int y = centre.Coordinate.Y - 1; y <= centre.Coordinate.Y + 1; y++)
                {
                    if (start.Select(p => p.Coordinate).Contains(new Coordinate(x, y))) continue;
                    var newPixel = new Pixel(new Coordinate(x, y), centre.Value);
                    result.Add(newPixel);
                }
            }

            return result;
        }

        private bool IsEdgeOnOrOff(Map<Pixel> map)
        {
            return map.Find(new Coordinate(0, 0)).Value;
        }
    }
}