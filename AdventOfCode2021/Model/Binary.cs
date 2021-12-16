using System;
using System.Linq;

namespace AdventOfCode2021.Model
{
    public class Binary
    {

        public static string BinaryStringFromHex(string hex)
        {
            return string.Join(String.Empty,
                hex.Select(
                    c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
                )
            );
        }


    }
}