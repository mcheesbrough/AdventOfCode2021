using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day14
{
    public interface IPolymerInserter
    {
        Dictionary<string, long> Insert(string polymerTemplate, List<PairInsertionRule> rule, int iterations);
        Dictionary<char, long> CountLetters(Dictionary<string, long> pairs, char first, char last);
    }


    public class PolymerInserter : IPolymerInserter
    {
        public Dictionary<string, long> Insert(string polymerTemplate, List<PairInsertionRule> rules, int iterations)
        {
            var pairs = Insert(polymerTemplate, rules);
            for (int i = 1; i < iterations; i++)
            {
                pairs = Insert(pairs, rules);
            }

            return pairs;
        }
        private Dictionary<string, long> Insert(string polymerTemplate, List<PairInsertionRule> rules)
        {
            
            var pairs = GetPairs(polymerTemplate);

            return Insert(pairs, rules);
        }

        private Dictionary<string, long> Insert(Dictionary<string, long> pairs, List<PairInsertionRule> rules)
        {
            var newPairs = new Dictionary<string, long>();
            foreach (var pair in pairs)
            {
                var matchingRule = rules.FirstOrDefault(r => r.Pair == pair.Key);
                if (matchingRule != null)
                {
                    AddPair(newPairs, matchingRule.Pair1, pair.Value);
                    AddPair(newPairs, matchingRule.Pair2, pair.Value);
                    continue;
                }
                newPairs.Add(pair.Key, pair.Value);
            }

            return newPairs;
        }

        private Dictionary<string, long> GetPairs(string template)

        {
             
            var pairs = new Dictionary<string, long>();
            for (int i = 0; i < template.Length -1; i++)
            {
                var pair = template.Substring(i, 2);
                AddPair(pairs, pair, 1);
            }

            return pairs;
        }

        private void AddPair(Dictionary<string, long> pairs, string pair, long count)
        {
            if (pairs.ContainsKey(pair))
            {
                pairs[pair] += count;
                return;
            }
            pairs.Add(pair, count);
        }

        public Dictionary<char, long> CountLetters(Dictionary<string, long> pairs, char first, char last)
        {
            var letterCounts = new Dictionary<char, long>();
            AddLetterCount(letterCounts, first, 1);
            AddLetterCount(letterCounts, last, 1);
            foreach (var pair in pairs)
            {
                AddLetterCount(letterCounts, pair.Key[0], pair.Value);
                AddLetterCount(letterCounts, pair.Key[1], pair.Value);
            }

            return letterCounts;
        }

        private void AddLetterCount(Dictionary<char, long> letterCounts, char letter, long count)
        {
            if (letterCounts.ContainsKey(letter))
            {
                letterCounts[letter] = letterCounts[letter] + count;
                return;
            }
            letterCounts.Add(letter, count);
        }
    }
}