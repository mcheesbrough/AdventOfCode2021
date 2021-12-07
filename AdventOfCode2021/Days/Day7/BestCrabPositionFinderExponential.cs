using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Days.Day7
{

    public class BestCrabPositionFinderExponential : IBestCrabPositionFinder
    {
        public int Find(List<int> positions)
        {
            var fuelCosts = new List<Tuple<int, int>>();
            for (var i = 0; i <= positions.Max(x => x); i++)
            {

                var fuelCost = positions.Sum(p => Enumerable.Range(1, Math.Abs(p - i)).Sum(y => y));
                fuelCosts.Add(new Tuple<int, int>(i, fuelCost));
            }

            return fuelCosts.Min(x => x.Item2);
        }

    }
}