using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Days.Day3
{
    public interface IGammaEpsilonFinder
    {
        void Find(List<string> binaries, out string mostCommonBinary, out string leastCommonBinary);
    }

    public abstract class Finder
    {
        protected int[] CountOnes(List<string> binaries)
        {
            var binariesLength = binaries.Max(x => x.Length);
            if (binaries.Any(x => x.Length != binariesLength)) throw new Exception("Not all binaries are same length");
            var oneCounts = new int[binariesLength];
            foreach (var binary in binaries)
            {
                for (var i = 0; i < binariesLength; i++)
                {
                    if (binary[i] == '1')
                    {
                        oneCounts[i]++;
                    }
                }
            }

            return oneCounts;
        }
    }

    public class GammaEpsilonFinder : Finder, IGammaEpsilonFinder
    {
        public void Find(List<string> binaries, out string mostCommonBinary, out string leastCommonBinary)
        {

            var oneCounts = CountOnes(binaries);

            mostCommonBinary = string.Empty;
            leastCommonBinary = string.Empty;
            foreach (var oneCount in oneCounts)
            {
                var mostCommonBit = oneCount > binaries.Count / 2.0 ? '1' : '0';
                var leastCommonBit = mostCommonBit == '0' ? '1' : '0';
                mostCommonBinary += mostCommonBit;
                leastCommonBinary += leastCommonBit;
            }
        }

    }
}