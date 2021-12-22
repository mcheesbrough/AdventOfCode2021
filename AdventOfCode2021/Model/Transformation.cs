namespace AdventOfCode2021.Model
{
    public class Transformation
    {
        public Transformation(int xShift, int yShift, int zShift, int reverse)
        {
            XShift = xShift;
            YShift = yShift;
            ZShift = zShift;
            Reverse = reverse;
        }

        public int XShift { get; }
        public int YShift { get; }
        public int ZShift { get; }
        public int Reverse { get; }

        public override string ToString()
        {
            return $"[{XShift},{YShift},{ZShift},{Reverse}]";
        }

        public Transformation Combine(Transformation t)
        {
            return new Transformation(
                XShift * t.XShift,
                YShift * t.YShift,
                ZShift * t.ZShift,
                t.Reverse
            );
        }
    }
}