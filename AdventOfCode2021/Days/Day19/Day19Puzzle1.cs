using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2021.General;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day19
{
    public class Day19Puzzle1: IPuzzleSolver
    {
        private readonly IScannerLoader _scannerLoader;
        private readonly IScannerComparer _scannerComparer;

        public Day19Puzzle1(IScannerLoader scannerLoader, IScannerComparer scannerComparer)
        {
            _scannerLoader = scannerLoader;
            _scannerComparer = scannerComparer;
        }

        public string Run()
        {
            var input = File
                .ReadAllText(@"C:\\aoc\day19\19_1.txt");

            var scanners = _scannerLoader.Load(input);
            var result = _scannerComparer.FindAllBeacons(scanners, 12);
            scanners.ForEach(x => Console.WriteLine(x.Position));
            var maxDist = 0;
            for (int i = 0; i < scanners.Count; i++)
            {
                for (int j = 0; j < scanners.Count; j++)
                {
                    var dist = scanners[i].Position.DistanceFrom(scanners[j].Position);
                    if (dist > maxDist) maxDist = dist;
                }
            }
            //var totalBeacons = result.Count;
            return maxDist.ToString();
        }

    }
}