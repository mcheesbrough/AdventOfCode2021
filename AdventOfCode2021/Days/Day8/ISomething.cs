﻿namespace AdventOfCode2021.Days.Day8
{
    public interface ISomething
    {
        string Do(string a);
    }

    public class Something : ISomething
    {
        public string Do(string a)
        {
            throw new System.NotImplementedException();
        }
    }
}