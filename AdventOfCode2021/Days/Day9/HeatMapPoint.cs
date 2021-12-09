using AdventOfCode2021.Days.Day2;

namespace AdventOfCode2021.Days.Day9
{
    public class HeatMapPoint
    {
        public HeatMapPoint(Coordinate point, int height)
        {
            Point = point;
            Height = height;
        }

        public Coordinate Point { get; }
        public int Height { get; }
    }
}