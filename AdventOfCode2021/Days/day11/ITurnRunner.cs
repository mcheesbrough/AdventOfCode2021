﻿using System;
using System.Linq;
using AdventOfCode2021.Days.Day9;

namespace AdventOfCode2021.Days.Day11
{
    public interface ITurnRunner
    {
        long Run(Map<OctopusPoint> map, int turns);
    }

    public class TurnRunner : ITurnRunner
    {
        private readonly IOctopusTurnProcessor _octopusTurnProcessor;

        public TurnRunner(IOctopusTurnProcessor octopusTurnProcessor)
        {
            _octopusTurnProcessor = octopusTurnProcessor;
        }

        public long Run(Map<OctopusPoint> map, int turns)
        {
            var nextMap = map.Clone();
            var previousFlashes = 0;
            for (int t = 1; t <= turns; t++)
            {
                nextMap = _octopusTurnProcessor.Process(nextMap, t);
                var flashesThisTurn = nextMap.Points.Count(p => p.LastFlashTurn == t);
                if (flashesThisTurn == map.Points.Count)
                {
                    Console.WriteLine(t.ToString());
                    break;
                }
            }

            return nextMap.Points.Sum(p => Convert.ToInt64(p.NumberOfFlashes));
        }
    }
}