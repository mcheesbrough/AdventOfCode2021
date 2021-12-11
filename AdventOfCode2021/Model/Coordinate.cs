using System;
using System.Linq;

namespace AdventOfCode2021.Model
{
    public class Coordinate : IEquatable<Coordinate>, ICloneable
    {
 

        public int X { get; }
        public int Y { get; }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Coordinate FromDescription(string description)
        {
            var parts = description.Split(',').Select(x => int.Parse((string) x)).ToArray();
            if (parts.Length != 2) throw new Exception($"Coordinate description {description} does not have two values");
            return new Coordinate(parts[0], parts[1]);
        }

        public bool Equals(Coordinate other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Coordinate)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public object Clone()
        {
            return new Coordinate(X, Y);
        }

        public static bool operator ==(Coordinate left, Coordinate right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Coordinate left, Coordinate right)
        {
            return !Equals(left, right);
        }
    }
}