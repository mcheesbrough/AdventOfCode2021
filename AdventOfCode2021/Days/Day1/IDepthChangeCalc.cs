using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Days.Day1
{
    public interface IDepthChangeCalc
    {
        int NumberOfIncreases(List<int> depthMeasurements);

    }

    public class DepthChangeCalc : IDepthChangeCalc
    {
        public int NumberOfIncreases(List<int> depthMeasurements)
        {
            var numIncreases = 0;
            var lastMeasurement = int.MaxValue;
            foreach (var depthMeasurement in depthMeasurements)
            {
                numIncreases += depthMeasurement > lastMeasurement ? 1 : 0;
                lastMeasurement = depthMeasurement;
            }

            return numIncreases;
        }
    }

    public class ThreeMeasurementDepthChangeCalc : IDepthChangeCalc
    {
        public int NumberOfIncreases(List<int> depthMeasurements)
        {
            var numIncreases = 0;
            var lastMeasurement = int.MaxValue;

            for (var i = 0; i < depthMeasurements.Count-2; i++)
            {
                var sum = depthMeasurements.Skip(i).Take(3).Sum();
                numIncreases += sum > lastMeasurement ? 1 : 0;
                lastMeasurement = sum;
            }
            return numIncreases;

        }
    }
}