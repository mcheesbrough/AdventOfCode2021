namespace AdventOfCode2021.Model
{
    public class ReactorRebootInstruction
    {
        public ReactorRebootInstruction(ReactorRebootCuboid cuboid, bool turnOn)
        {
            Cuboid = cuboid;
            TurnOn = turnOn;
        }

        public ReactorRebootCuboid Cuboid { get; }
        public bool TurnOn { get; }
    }
}