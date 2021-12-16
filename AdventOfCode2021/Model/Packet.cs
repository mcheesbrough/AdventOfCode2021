using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Model
{
    public abstract class Packet
    {
        public int Version { get; }
        public PacketType Type { get; }
        public int TypeId { get; }

        public List<Packet> SubPackets { get; protected set; }

        protected Packet(int version, PacketType type, int typeId)
        {
            Version = version;
            Type = type;
            TypeId = typeId;
            SubPackets = new List<Packet>();
        }

        public List<int> Versions
        {
            get
            {
                var versions = new List<int>{Version};
                versions.AddRange(SubPackets.SelectMany(x => x.Versions));
                return versions;
            }
        }

        public abstract long Execute();

    }
}