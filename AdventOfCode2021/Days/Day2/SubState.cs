namespace AdventOfCode2021.Days.Day2
{
    public class SubState
    {
        public SubState(Coordinate position, int aim)
        {
            Position = position;
            Aim = aim;
        }

        public Coordinate Position { get; }
        public int Aim { get; }
    }
}