namespace AdventOfCode2021.Days.Day2
{
    public class MovementInstruction
    {
        public MovementInstruction(Direction direction, int amount)
        {
            Direction = direction;
            Amount = amount;
        }

        public Direction Direction { get; }
        public int Amount { get; }
    }
}