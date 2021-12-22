using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day19
{
    public interface IScannerLoader
    {
        List<Scanner> Load(string input);
    }

    public class ScannerLoader : IScannerLoader
    {
        public List<Scanner> Load(string input)
        {
            var scanners = new List<Scanner>();
            var scannerBlocks = input.Split("\r\n\r\n");
            foreach (var scannerBlock in scannerBlocks)
            {
                scanners.Add(new Scanner(scannerBlock
                    .Split("\r\n")
                    .Skip(1)
                    .Select(Coordinate3D.FromDescription)
                    .ToList()));
            }

            return scanners;

        }
    }
}