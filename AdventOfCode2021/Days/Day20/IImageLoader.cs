using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day20
{
    public interface IImageLoader
    {
        void Load(string input, out string enhancementAlgo, out Map<Pixel> image);
    }

    public class ImageLoader : IImageLoader
    {
        public void Load(string input, out string enhancementAlgo, out Map<Pixel> image)
        {
            var parts = input.Split("\r\n\r\n");
            enhancementAlgo = parts[0]
                .Replace('.', '0')
                .Replace('#', '1');

            var imageLines = parts[1]
                .Split("\r\n")
                .ToList();
            //imageLines.Insert(0,string.Empty.PadLeft(imageLines[0].Length, '.'));
            //imageLines.Add( string.Empty.PadLeft(imageLines[0].Length, '.'));
            var width = imageLines[0].Length;
            var height = imageLines.Count;
            var pixels = imageLines.SelectMany((x, i) =>
                x.ToCharArray().Select((y, i2) => new Pixel(new Coordinate(i2, i), y != '.')).ToList()).ToList();
            var littleMap = new Map<Pixel>(pixels, width, height);
            image = Expand(littleMap, 3);
        }

        public static Map<Pixel> Expand(Map<Pixel> map, int expandBy, bool isOn = false)
        {
            var newPixels = new List<Pixel>();
            for (var y = 0; y < expandBy; y++)
            {
                newPixels.AddRange(Enumerable.Repeat(isOn, map.Width + expandBy*2).Select((p, i) => new Pixel(new Coordinate(i, y), p)).ToList());

            }

            for (var y = 0; y < map.Height; y++)
            {
                var newRow = new List<Pixel>();
                for (var x = 0; x < expandBy; x++)
                {
                    newRow.Add(new Pixel(new Coordinate(x, y + expandBy), isOn));
                }

                newRow.AddRange(map.Points.Where(c => c.Coordinate.Y == y)
                    .Select(p => new Pixel(new Coordinate(p.Coordinate.X + expandBy, p.Coordinate.Y + expandBy), p.Value)));
                for (var x = map.Width + expandBy; x < map.Width + expandBy*2; x++)
                {
                    newRow.Add(new Pixel(new Coordinate(x, y + expandBy), isOn));
                }
                newPixels.AddRange(newRow);
            }

            for (var y = map.Height+ expandBy; y < map.Height + expandBy*2; y++)
            {
                newPixels.AddRange(Enumerable.Repeat(isOn, map.Width + expandBy*2)
                    .Select((p, i) => new Pixel(new Coordinate(i, y), p)).ToList());
            }

            return new Map<Pixel>(newPixels, map.Width+expandBy*2, map.Height+expandBy*2);
        }
    }
}