using System;
using AdventOfCode2021.Days.Day5;

namespace AdventOfCode2021.Model
{
    public class LiteralPacket: Packet
    {
        public long Value { get; }

        public LiteralPacket(int version, PacketType type, int typeId, long value) : base(version, type, typeId)
        {
            Value = value;
        }


        public override long Execute()
        {
            return Value;
        }
    }
}