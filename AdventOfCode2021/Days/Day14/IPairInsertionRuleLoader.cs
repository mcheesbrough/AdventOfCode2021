using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day14
{
    public interface IPairInsertionRuleLoader
    {
        string LoadPolymerTemplate(string input);
        List<PairInsertionRule> LoadPairInsertionRule(List<string> input);
    }

    public class PairInsertionRuleLoader : IPairInsertionRuleLoader
    {
        public string LoadPolymerTemplate(string input)
        {
            throw new System.NotImplementedException();
        }

        public List<PairInsertionRule> LoadPairInsertionRule(List<string> input)
        {
            return input
                .Select(x => PairInsertionRule.FromDescription(x))
                .ToList();
        }
    }
}