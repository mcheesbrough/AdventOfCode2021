using System;
using System.Linq;

namespace AdventOfCode2021.Model
{
    public class PairInsertionRule
    {
        public PairInsertionRule(string pair, string insert)
        {
            if (pair.Length != 2) throw new Exception($"Pair {pair} does not have 2 chars");
            Pair = pair;
            Replacement = pair[0] + insert + pair[1];
            Pair1 = pair[0] + insert;
            Pair2 = insert + pair[1];
        }

        public string Pair { get; }
        public string Replacement { get; }
        public string Pair1 { get; }
        public string Pair2 { get; }

        public static PairInsertionRule FromDescription(string description)
        {
            var parts = description
                .Trim()
                .Replace(">", string.Empty)
                .Split('-')
                .Select(x => x.Trim())
                .ToList();
            return new PairInsertionRule(parts[0], parts[1]);
        }
    }
}