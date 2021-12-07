using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Days.Day7
{
    public interface IBestCrabPositionFinder
    {
        int Find(List<int> positions);
    }

    public class BestCrabPositionFinder : IBestCrabPositionFinder
    {
        public int Find(List<int> positions)
        {
            var fuelCosts = new List<Tuple<int, int>>();
            for (var i = 0; i <= positions.Max(x => x); i++)
            {

                var fuelCost = positions.Sum(p => Math.Abs(p - i));
                fuelCosts.Add(new Tuple<int, int>(i, fuelCost));
            }

            return fuelCosts.Min(x => x.Item2);
        }
    }
}