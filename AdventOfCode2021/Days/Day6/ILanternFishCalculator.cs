using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Days.Day6
{
    public interface ILanternFishCalculator
    {
        List<FishCount> Calculate(List<FishCount> fish);
    }

    public class LanternFishCalculator : ILanternFishCalculator
    {
        public List<FishCount> Calculate(List<FishCount> fishCounts)
        {
            var newFishCounts = new List<FishCount>{new FishCount{FishTimer = 8, Count = 0}};
            foreach (var fishCount in fishCounts)
            {
                if (fishCount.FishTimer == 0)
                {
                    newFishCounts.Add(new FishCount{FishTimer = 6, Count = fishCount.Count});
                    newFishCounts.First(x => x.FishTimer == 8).Count += fishCount.Count;
                    continue;
                }
                newFishCounts.Add(new FishCount{FishTimer = fishCount.FishTimer-1, Count = fishCount.Count});
            }

            return newFishCounts;
        }
    }
}