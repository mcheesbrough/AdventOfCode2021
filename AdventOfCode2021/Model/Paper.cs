using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021.Model
{
    public class Paper
    {
        public Paper(List<Coordinate> dots, int width, int height)
        {
            Dots = dots;
            Width = width;
            Height = height;
        }
        
        public List<Coordinate> Dots { get; }
        public int Width { get;  }
        public int Height { get;  }

        public string Print()
        {
            var readout = new StringBuilder();
            for (var y = 0; y < Height; y++)
            {
                var line = string.Empty;
                for (var x = 0; x < Width; x++)
                {
                    if (Dots.Contains(new Coordinate(x, y)))
                    {
                        line += "#";
                    }
                    else
                    {
                        line += ".";
                    }
                }
                readout.AppendLine(line);
            }

            return readout.ToString().TrimEnd('\r','\n');
        }
    }
}