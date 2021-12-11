using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day10
{
    public interface ISyntaxChecker
    {
        SubChar FindInvalidCharacter(string line);
    }

    public class SyntaxChecker : ISyntaxChecker
    {
        public SubChar FindInvalidCharacter(string line)
        {
            var chars = line.Select(x => new SubChar(x)).ToList();
            var result = FindPair(chars);
            return result;
        }

        private SubChar FindPair(List<SubChar> toProcess)
        {
            if (toProcess.Count == 2) return toProcess[0].ClosingChar == toProcess[1].Value ? null : toProcess[1]; 
            var pairs = new Stack<SubChar>();
            for (var i = 0; i < toProcess.Count; i++)
            {
                if (toProcess[i].IsOpening)
                {
                    pairs.Push(toProcess[i]);
                    continue;
                }

                var toCheck = pairs.Pop();
                if (toCheck.ClosingChar != toProcess[i].Value) return toProcess[i];
            }

            return null;
        }
    }
}