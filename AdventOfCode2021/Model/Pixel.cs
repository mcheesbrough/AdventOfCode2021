namespace AdventOfCode2021.Model
{
    public class Pixel: IMapPoint
    {
        public Pixel(Coordinate coordinate, bool value)
        {
            Coordinate = coordinate;
            Value = value;
        }

        public override string ToString()
        {
            return Value ? "#" : ".";
        }

        public Coordinate Coordinate { get; }
        public bool Value { get; }
        public object Clone()
        {
            return new Pixel(new Coordinate(Coordinate.X, Coordinate.Y), Value);
        }
    }
}