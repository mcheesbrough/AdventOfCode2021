namespace AdventOfCode2021.Model
{
    public class ReactorRebootCube
    {
        public ReactorRebootCube(Coordinate3D start, Coordinate3D end)
        {
            Start = start;
            End = end;
        }

        public Coordinate3D Start { get; }
        public Coordinate3D End { get; }
    }
}