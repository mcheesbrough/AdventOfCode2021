using System;
using System.Collections.Generic;

namespace AdventOfCode2021.Model
{
    public class Cave : IEquatable<Cave>
    {
        

        public Cave(string name)
        {
            Name = name;
            ConnectedCaves = new List<Cave>();
            Visits = 0;
        }

        public string Name { get; }
        public int Visits { get; set; }
        public bool IsBig => Name.ToUpper() == Name;
        public bool IsStart => Name == "start";
        public bool IsEnd => Name == "end";
        public List<Cave> ConnectedCaves { get; set; }
        public void AddConnection(Cave cave)
        {
            if (!ConnectedCaves.Contains(cave)) ConnectedCaves.Add(cave);
        }


        public bool Equals(Cave other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Cave)obj);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }

        public static bool operator ==(Cave left, Cave right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Cave left, Cave right)
        {
            return !Equals(left, right);
        }


    }
}