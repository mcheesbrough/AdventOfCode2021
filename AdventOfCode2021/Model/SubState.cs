namespace AdventOfCode2021.Model
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