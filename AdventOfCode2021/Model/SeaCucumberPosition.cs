namespace AdventOfCode2021.Model
{
    public class SeaCucumberPosition: IMapPoint
    {
        public int UpDown { get; private set; }
        public int LeftRight { get; private set; }
        public SeaCucumberPosition(Coordinate c, int upDown, int leftRight)
        {
            Coordinate = c;
            UpDown = upDown;
            LeftRight = leftRight;
        }

        public bool IsEmpty => UpDown == 0 && LeftRight == 0;
        public object Clone()
        {
            return new SeaCucumberPosition((Coordinate) Coordinate.Clone(), UpDown, LeftRight);
        }

        public Coordinate Coordinate { get; }

        public override string ToString()
        {
            if (LeftRight != 0) return ">";
            if (UpDown != 0) return "v";
            return ".";
        }

        public void Swap(SeaCucumberPosition sc)
        {
            var lr = LeftRight;
            var ud = UpDown;
            LeftRight = sc.LeftRight;
            UpDown = sc.UpDown;
            sc.LeftRight = lr;
            sc.UpDown = ud;
        }
    }
}