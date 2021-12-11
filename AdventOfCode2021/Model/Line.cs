using System;
using System.Collections.Generic;

namespace AdventOfCode2021.Model
{
    public class Line
    {
        public Line(Coordinate start, Coordinate end)
        {
            Start = start;
            End = end;
        }

        public Coordinate Start { get; }
        public Coordinate End { get; }

        public bool IsHorizontal => Start.Y == End.Y;
        public bool IsVertical => Start.X == End.X;

        public List<Coordinate> Path
        {
            get
            {
                var path = new List<Coordinate>();

                var xInc = Math.Sign(End.X - Start.X);
                var yInc = Math.Sign(End.Y - Start.Y);
                var lineLength = xInc != 0 ? Math.Abs(End.X - Start.X) : Math.Abs(End.Y - Start.Y);
                for (var i = 0; i <= lineLength; i++)
                {
                    path.Add(new Coordinate(Start.X + xInc * i, Start.Y + yInc * i));
                }


                return path;
            }
        }
    }
}