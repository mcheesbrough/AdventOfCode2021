using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day13
{
    public interface IInstructionPaperLoader
    {
        List<Coordinate> LoadCoordinates(string input);
        List<Fold> LoadFolds(string input);
    }

    public class InstructionPaperLoader : IInstructionPaperLoader
    {
        public List<Coordinate> LoadCoordinates(string input)
        {
            return input
                .Split("\r\n")
                .Select(Coordinate.FromDescription)
                .ToList();
        }

        public List<Fold> LoadFolds(string input)
        {
            return input
                .Split("\r\n")
                .Select(Fold.FromDescription)
                .ToList();
        }
    }
}