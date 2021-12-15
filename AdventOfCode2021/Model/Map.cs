using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace AdventOfCode2021.Model
{
    public class Map<T> where T: IMapPoint, ICloneable
    {
        public int Width { get; set; }
        public int Height { get; set;  }

        public Map(List<T> points, int width, int height)
        {
            Width = width;
            Height = height;
            PointsDictionary = points.ToDictionary(c => c.Coordinate, p => p);
        }

        public T Find(Coordinate c) => PointsDictionary[c];

        public List<T> AdjacentPoints(T point)
        {
            var adjacent = new List<T>();
            if (point.Coordinate.X - 1 >= 0) adjacent.Add(PointsDictionary[new Coordinate( point.Coordinate.X - 1, point.Coordinate.Y)]);
            if (point.Coordinate.X + 1 < Width) adjacent.Add(PointsDictionary[new Coordinate(point.Coordinate.X + 1, point.Coordinate.Y)]);
            if (point.Coordinate.Y - 1 >= 0) adjacent.Add(PointsDictionary[new Coordinate(point.Coordinate.X, point.Coordinate.Y - 1)]);
            if (point.Coordinate.Y + 1 < Height) adjacent.Add(PointsDictionary[new Coordinate(point.Coordinate.X, point.Coordinate.Y + 1)]);
            return adjacent;
        }

        public List<T> AdjacentIncludingDiagonalPoints(T point)
        {
            var adjacent = AdjacentPoints(point);
            if (point.Coordinate.X - 1 >= 0 && point.Coordinate.Y - 1 >= 0) 
                adjacent.Add(PointsDictionary[new Coordinate(point.Coordinate.X - 1, point.Coordinate.Y - 1)]);
            if (point.Coordinate.X + 1 < Width && point.Coordinate.Y + 1 < Height) 
                adjacent.Add(PointsDictionary[new Coordinate(point.Coordinate.X + 1, point.Coordinate.Y + 1)]);
            if (point.Coordinate.Y - 1 >= 0 && point.Coordinate.X + 1 < Width) 
                adjacent.Add(PointsDictionary[new Coordinate(point.Coordinate.X + 1, point.Coordinate.Y - 1)]);
            if (point.Coordinate.Y + 1 < Height && point.Coordinate.X - 1 >= 0) 
                adjacent.Add(PointsDictionary[new Coordinate(point.Coordinate.X - 1,  point.Coordinate.Y + 1)]);
            return adjacent;
        }

        public Map<T> Clone()
        {
            var clonedPoints = Points.ConvertAll(p => (T)p.Clone());
            return new Map<T>(clonedPoints, Width, Height);
        }

        public List<T> Points => PointsDictionary.Values.ToList();
        public Dictionary<Coordinate, T> PointsDictionary { get; set; }

        public string Print()
        {
            var readout = new StringBuilder();
            for (var y = 0; y < Height; y++)
            {
                var line = string.Empty;
                for (var x = 0; x < Width; x++)
                {
                    line += Find(new Coordinate(x, y)).ToString();
                }
                readout.AppendLine(line);
            }

            return readout.ToString().TrimEnd('\r', '\n');
        }
    }
}