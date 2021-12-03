using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace AdventOfCode2021.Days.Day3
{
    public interface IOxygenCo2Finder
    {
        void Find(List<string> binaries, out string oxygen, out string cO2);
    }

    public class OxygenCo2Finder : Finder, IOxygenCo2Finder
    {
        public void Find(List<string> binaries, out string oxygen, out string cO2)
        {
            var oxygenBinaries = new List<string>(binaries);
            var cO2Binaries = new List<string>(binaries);
            for (var i = 0; i < binaries[0].Length; i++)
            {
                if (oxygenBinaries.Count > 1)
                {
                    var oneCounts = CountOnes(oxygenBinaries);
                    var mostCommon = oneCounts[i] >= oxygenBinaries.Count / 2.0 ? '1' : '0';
                    oxygenBinaries = oxygenBinaries.Where(x => x[i] == mostCommon)
                        .ToList();
                }


                if (cO2Binaries.Count > 1)
                {
                    var oneCounts = CountOnes(cO2Binaries);
                    var leastCommon = oneCounts[i] < cO2Binaries.Count / 2.0 ? '1' : '0';
                    cO2Binaries = cO2Binaries.Where(x => x[i] == leastCommon)
                        .ToList();
                }

                if (oxygenBinaries.Count == 1 && cO2Binaries.Count == 1) break;

            }

            oxygen = oxygenBinaries[0];
            cO2 = cO2Binaries[0];

        }
    }
}