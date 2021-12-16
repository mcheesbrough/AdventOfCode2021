using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2021.General;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day16
{
    public class Day16Puzzle1: IPuzzleSolver
    {
        private readonly IPacketParser _packetParser;
        public Day16Puzzle1(IPacketParser packetParser)
        {
            _packetParser = packetParser;
        }

        public string Run()
        {
            var input = File
                .ReadAllText(@"C:\\aoc\day16\16_1.txt");
            var packets = _packetParser.Parse(Binary.BinaryStringFromHex(input));

            return packets.Execute().ToString();
        }

    }
}