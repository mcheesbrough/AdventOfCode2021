using AdventOfCode2021.Days.Day11;
using AdventOfCode2021.Days.Day2;

namespace AdventOfCode2021.Days.Day9
{
    public class HeatMapPoint: IMapPoint
    {
        public HeatMapPoint(Coordinate coordinate, int height)
        {
            Coordinate = coordinate;
            Height = height;
        }

        public Coordinate Coordinate { get; }
        public int Height { get; }
        public object Clone()
        {
            return new HeatMapPoint(new Coordinate(Coordinate.X, Coordinate.Y), Height);
        }
    }
}