using System;
using System.Collections.Generic;
using System.Linq;

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
            var strippedLine = StripFinalOpeningChars(chars);
            var result = FindPair(strippedLine);
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

        public List<List<SubChar>> FindSections(List<SubChar> toProcess)
        {
            var opens = 0;
            var sections = new List<List<SubChar>>();
            var startSection = 0;
            for (var i = 0; i < toProcess.Count; i++)
            {
                if (toProcess[i].IsOpening)
                {
                    opens++;
                    continue;
                }

                opens--;
                if (opens == 0)
                {
                    sections.Add(toProcess.Skip(startSection).Take(i - startSection + 1).ToList());
                    startSection = i + 1;
                }
            }

            return sections;
        }

        public List<SubChar> StripFinalOpeningChars(List<SubChar> chars)
        {
            for (var i = chars.Count-1; i >= 0; i--)
            {
                if (chars[i].IsClosing) return chars.Take(i + 1).ToList();
            }

            return new List<SubChar>();
        }

    }
}