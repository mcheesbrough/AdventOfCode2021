using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day8
{
    public interface IDigitFinder
    {
        List<string> GetMappings(List<Digit> digits);
        int FindDigit(List<string> mappings, string input);
    }

    public class DigitFinder : IDigitFinder
    {

        public List<string> GetMappings(List<Digit> digits)
        {
            var resultArray = new string[10];
            resultArray[1] = digits.First(x => x.EasyValue == 1).DigitString;
            resultArray[4] = digits.First(x => x.EasyValue == 4).DigitString;
            resultArray[7] = digits.First(x => x.EasyValue == 7).DigitString;
            resultArray[8] = digits.First(x => x.EasyValue == 8).DigitString;

            var rightSide = resultArray[1].ToCharArray();
            var top = resultArray[7].ToCharArray().Except(rightSide).ToArray();
            var leftTopAndMiddle = resultArray[4].ToCharArray().Except(rightSide).ToArray();

            resultArray[9] = digits.First(x => x.DigitString.Length == 6
                                                        && x.Contains(rightSide)
                                                        && x.Contains(leftTopAndMiddle)
                                                        && x.Contains(top)).DigitString;
            var leftBottom = resultArray[8].ToCharArray().Except(resultArray[9].ToCharArray()).ToArray();
            var bottom = resultArray[9].Except(rightSide).Except(top).Except(leftTopAndMiddle).ToArray();


            resultArray[0] = digits.First(x => x.DigitString.Length == 6
                                               && !x.Equals(new Digit(resultArray[9]))
                                               && x.Contains(rightSide)).DigitString;

            var middle = resultArray[8].ToCharArray().Except(resultArray[0].ToCharArray()).ToArray();
            var leftTop = leftTopAndMiddle.Except(middle).ToArray();

            resultArray[6] = digits.First(x => x.DigitString.Length == 6
                                               && !x.Equals(new Digit(resultArray[9]))
                                               && !x.Equals(new Digit(resultArray[0]))).DigitString;

            resultArray[3] = digits.First(x => x.DigitString.Length == 5
                                               && x.Contains(rightSide)
                                               && x.Contains(top)
                                               && x.Contains(middle)
                                               && x.Contains(bottom)).DigitString;

            resultArray[2] = digits.First(x => x.DigitString.Length == 5
                                               && x.Contains(leftBottom)).DigitString;

            resultArray[5] = digits.First(x => x.DigitString.Length == 5
                                               && x.Contains(leftTop)).DigitString;

            return resultArray.ToList();
        }

        public int FindDigit(List<string> mappings, string input)
        {
            var mappingsWithIndex = mappings
                .Select((x, index) => new {digit = new Digit(x), index = index});
            var match = mappingsWithIndex.First(x => x.digit.Equals(new Digit(input)));
            return match.index;
        }
    }
}