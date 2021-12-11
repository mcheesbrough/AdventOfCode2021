using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day4
{
    public interface IBingoParser
    {
        List<BingoCard> Parse(string input, out List<int> numbers);
    }

    public class BingoParser : IBingoParser
    {
        public List<BingoCard> Parse(string input, out List<int> numbers)
        {
            var inputBlocks = input.Split("\r\n\r\n");
            numbers = inputBlocks.First().Split(',').Select(int.Parse).ToList();
            var cards = inputBlocks
                .Skip(1)
                .Select(x => new BingoCard(x.Replace("\r\n", " ")
                    .Replace("  ", " ")
                    .Trim()
                    .Split(' ')
                    .Select(y => int.Parse(y.Trim()))
                    .ToList()))
                .ToList();
            return cards;
        }
    }
}