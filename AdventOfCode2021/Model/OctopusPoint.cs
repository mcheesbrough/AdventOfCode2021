using AdventOfCode2021.Days.Day2;

namespace AdventOfCode2021.Model
{
    public class OctopusPoint: IMapPoint
    {
        public OctopusPoint(Coordinate coordinate, int brightness)
        {
            Coordinate = coordinate;
            Brightness = brightness;
            NumberOfFlashes = 0;
            LastFlashTurn = 0;
        }

        public object Clone()
        {
            var newOctopusPoint = new OctopusPoint(new Coordinate(Coordinate.X, Coordinate.Y), Brightness);
            newOctopusPoint.NumberOfFlashes = NumberOfFlashes;
            newOctopusPoint.LastFlashTurn = LastFlashTurn;
            return newOctopusPoint;
        }

        public Coordinate Coordinate { get; }
        public int Brightness { get; set; }
        public int NumberOfFlashes { get; set; }
        public int LastFlashTurn { get; set; }
    }

}