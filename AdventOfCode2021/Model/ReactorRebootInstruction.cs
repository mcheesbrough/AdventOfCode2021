namespace AdventOfCode2021.Model
{
    public class ReactorRebootInstruction
    {
        public ReactorRebootInstruction(ReactorRebootCube cube, bool turnOn)
        {
            Cube = cube;
            TurnOn = turnOn;
        }

        public ReactorRebootCube Cube { get; }
        public bool TurnOn { get; }
    }
}