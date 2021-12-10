using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Days.Day10
{
    public interface ISyntaxCompleter
    {
        List<SubChar> Complete(string line);
        long CalcCompletionScore(List<string> input);
    }

    public class SyntaxCompleter : ISyntaxCompleter
    {
        private readonly ISyntaxChecker _syntaxChecker;

        public SyntaxCompleter(ISyntaxChecker syntaxChecker)
        {
            _syntaxChecker = syntaxChecker;
        }

        public List<SubChar> Complete(string line)
        {
            var toProcess = line.Select(x => new SubChar(x)).ToList();
            var newChars = new List<SubChar>();
            var stack = new Stack<SubChar>();
            for (var i = 0; i < toProcess.Count; i++)
            {
                if (toProcess[i].IsOpening)
                {
                    stack.Push(toProcess[i]);
                    continue;
                }

                stack.Pop();
            }

            while (stack.Count > 0)
            {
                newChars.Add(new SubChar(stack.Pop().ClosingChar));
            }

            return newChars;
        }

        public long CalcCompletionScore(List<string> input)
        {
            var validIncomplete = input
                .Where(x => _syntaxChecker.FindInvalidCharacter(x) == null).ToList();
            var completions = validIncomplete
                .Select(x => Complete(x));

            var scores = completions.Select(CalculateScore).ToList();
            var orderedScores = scores.OrderBy(x => x);
            var middleScore = orderedScores.ElementAt(scores.Count() / 2);
            return middleScore;

        }

        public long CalculateScore(List<SubChar> subChars)
        {
            long score = 0;
            foreach (var subChar in subChars)
            {
                score = score * 5 + subChar.CompletionScore;
            }

            return score;
        }
    }
}