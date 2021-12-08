using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Days.Day8
{
    public interface IOutputDigitCounter
    {
        int Count(List<Digit> outputValues);
    }

    public class OutputDigitCounter : IOutputDigitCounter
    {
        public int Count(List<Digit> outputValues)
        {
            return outputValues.Count(x => x.IsEasyDigit);
        }

    }
}