using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Model
{
    public class OperatorPacket: Packet
    {

        public OperatorPacket(int version, PacketType type, int typeId, List<Packet> packets) : base(version, type, typeId)
        {
            SubPackets = packets;
        }

        public override long Execute()
        {
            switch (TypeId)
            {
                case 0:
                {
                    return SubPackets.Sum(x => x.Execute());
                }
                case 1:
                {
                    return SubPackets.Aggregate((long)1, (prod, x) => x.Execute() * prod);
                }
                case 2:
                {
                    return SubPackets.Min(x =>  x.Execute());
                }
                case 3:
                {
                    return SubPackets.Max(x => x.Execute());
                }
                case 5:
                {
                    return Convert.ToInt64(SubPackets[0].Execute() > SubPackets[1].Execute());
                }
                case 6:
                {
                    return Convert.ToInt64(SubPackets[0].Execute() < SubPackets[1].Execute());
                }
                case 7:
                {
                    return Convert.ToInt64(SubPackets[0].Execute() == SubPackets[1].Execute());
                }
                default: throw new Exception("TypeId not recognised");
            }
        }
    }
}