using System;

namespace AdventOfCode2021.Model
{
    public class Rotation : IEquatable<Rotation>
    {
 

        public Rotation(int aboutX, int aboutY, int aboutZ)
        {
            AboutX = aboutX;
            AboutY = aboutY;
            AboutZ = aboutZ;
        }

        public int AboutX { get; }
        public int AboutY { get; }
        public int AboutZ { get; }

        public override string ToString()
        {
            return $"[{AboutX},{AboutY},{AboutZ}]";
        }

        public bool Equals(Rotation other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return AboutX == other.AboutX && AboutY == other.AboutY && AboutZ == other.AboutZ;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Rotation)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(AboutX, AboutY, AboutZ);
        }

        public static bool operator ==(Rotation left, Rotation right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Rotation left, Rotation right)
        {
            return !Equals(left, right);
        }
    }
}