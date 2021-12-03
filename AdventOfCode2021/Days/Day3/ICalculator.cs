using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Days.Day3
{
    public interface ICalculator
    {
        int Calculate(List<string> binaries);
    }

    public abstract class Calculator : ICalculator
    {
        public abstract int Calculate(List<string> binaries);
        protected int MultiplyBinaries(params string[] binaries)
        {
            return binaries.Aggregate(1, (acc, x) => Convert.ToInt32(x, 2) * acc);
        }
    }

    public class PowerCalculator : Calculator
    {
        private readonly IGammaEpsilonFinder _gammaEpsilonFinder;

        public PowerCalculator(IGammaEpsilonFinder gammaEpsilonFinder)
        {
            _gammaEpsilonFinder = gammaEpsilonFinder;
        }

        public override int Calculate(List<string> binaries)
        {
            _gammaEpsilonFinder.Find(binaries, out var gamma, out var epsilon);
            return MultiplyBinaries(gamma, epsilon);
        }
    }

    public class LifeSupportCalculator : Calculator
    {
        private readonly IOxygenCo2Finder _oxygenCo2Finder;

        public LifeSupportCalculator(IOxygenCo2Finder oxygenCo2Finder)
        {
            _oxygenCo2Finder = oxygenCo2Finder;
        }

        public override int Calculate(List<string> binaries)
        {
            _oxygenCo2Finder.Find(binaries, out var gamma, out var epsilon);
            return MultiplyBinaries(gamma, epsilon);
        }
    }
}