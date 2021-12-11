using System;
using System.Linq;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day4
{
    public interface IBingoPlayer
    {
        int PlayToWin(string input);
        int PlayToLose(string input);
    }

    public class BingoPlayer : IBingoPlayer
    {
        private readonly IBingoParser _parser;

        public BingoPlayer(IBingoParser parser)
        {
            _parser = parser;
        }

        public int PlayToWin(string input)
        {
            var cards = _parser.Parse(input, out var numbers);
            BingoCard winningCard;
            foreach (var number in numbers)
            {
                foreach (var card in cards)
                {
                    card.Check(number);
                }

                winningCard = cards.FirstOrDefault(x => x.IsBingo);
                if (winningCard != null)
                {
                    return winningCard.SumUnchecked * number;
                }
            }

            throw new Exception("No winner");
        }

        public int PlayToLose(string input)
        {
            var cards = _parser.Parse(input, out var numbers);
            foreach (var number in numbers)
            {
                var losingCards = cards.Where(x => !x.IsBingo).ToList();
                foreach (var card in cards)
                {
                    card.Check(number);
                }

                if (losingCards.Count() == 1)
                {
                    var winningCards = cards.Where(x => x.IsBingo);
                    if (winningCards.Count() == cards.Count)
                    {
                        return losingCards.First().SumUnchecked * number;
                    }
                }
            }

            throw new Exception("No winner");
        }
    }
}