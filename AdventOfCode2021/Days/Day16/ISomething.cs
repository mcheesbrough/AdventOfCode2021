using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day16
{
    public interface ISomething
    {
        string Load(List<string> input);
    }

    public class Something : ISomething
    {
        public string Load(List<string> input)
        {
            throw new NotImplementedException();
        }
    }
}