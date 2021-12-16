using System;
using System.Collections.Generic;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day16
{
    public interface IPacketParser
    {
        Packet Parse(string input);
    }

    public class PacketParser : IPacketParser
    {
        public Packet Parse(string input)
        {
            return Parse(ref input);
        }
        
        public Packet Parse(ref string input)
        {
            var version = ParseVersion(ReadBits(ref input, 3));
            var typeBits = ReadBits(ref input, 3);
            var typeId = Convert.ToInt32(typeBits, 2);
            var type = ParseType(typeBits);

            switch (type)
            {
                case PacketType.Literal:
                    return new LiteralPacket(version, type, typeId, ParseLiteral(ref input));
                case PacketType.Operator:
                    return new OperatorPacket(version, type, typeId, ParseOperator(ref input));
            }

            throw new Exception("Could not parse packet");
        }

        public int ParseVersion(string input)
        {
            return Convert.ToInt32(ReadBits(ref input, 3), 2);
        }


        private long ParseLiteral(ref string packetString)
        {
            var valueAsString = string.Empty;
            while (true)
            {
                var nextString = ReadBits(ref packetString, 5);
                valueAsString += nextString.Substring(1);
                if (nextString[0] == '0') break;
            }

            return Convert.ToInt64(valueAsString, 2);
        }

        private List<Packet> ParseOperator(ref string input)
        {
            var subPackets = new List<Packet>();
            var lengthType = ReadBits(ref input, 1);
            if (lengthType == "0")
            {
                var subPacketLength = Convert.ToInt32(ReadBits(ref input, 15), 2);
                var subPacketString = ReadBits(ref input, subPacketLength);
                while (!string.IsNullOrEmpty(subPacketString))
                {
                    subPackets.Add(Parse(ref subPacketString));
                }
                
            }
            else
            {
                var numSubPackets = Convert.ToInt32(ReadBits(ref input, 11), 2);
                for (var i = 0; i < numSubPackets; i++)
                {
                    subPackets.Add(Parse(ref input));
                }
            }

            return subPackets;
        }

        private static string ReadBits(ref string input, int bits)
        {
            if (input.Length < bits) throw new Exception("Not enough bits to read");
            var output = input[..bits];
            input = input.Substring(bits);
            return output;
        }

        public PacketType ParseType(string input)
        {
            switch (input)
            {
                case "100": return PacketType.Literal;
                default: return PacketType.Operator;
            }
        }
    }
}