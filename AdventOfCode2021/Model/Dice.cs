namespace AdventOfCode2021.Model
{
    public class Dice
    {
        public int NumRolls { get; private set; }

        public Dice()
        {
            NumRolls = 0;
        }

        public int Roll()
        {
            NumRolls++;
            var result = NumRolls % 100;
            return result == 0 ? 100 : result;
        }
    }
}