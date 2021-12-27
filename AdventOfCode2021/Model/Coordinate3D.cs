using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.XPath;

namespace AdventOfCode2021.Model
{
    public class Coordinate3D : IEquatable<Coordinate3D>, ICloneable
    {
 

        public int X { get; }
        public int Y { get; }
        public int Z { get; }

        public Coordinate3D(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Coordinate3D FromDescription(string description)
        {
            var parts = description.Split(',').Select(x => int.Parse((string) x)).ToArray();
            if (parts.Length != 3) throw new Exception($"Coordinate description {description} does not have three values");
            return new Coordinate3D(parts[0], parts[1], parts[2]);
        }

        public int DistanceFromOrigin => Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);

        public int DistanceFrom(Coordinate3D c) => Math.Abs(X-c.X) + Math.Abs(Y-c.Y) + Math.Abs(Z-c.Z);

        public Coordinate3D Add(Coordinate3D c)
        {
            return new Coordinate3D(X + c.X, Y + c.Y, Z + c.Z);
        }

        public Coordinate3D Subtract(Coordinate3D c)
        {
            return new Coordinate3D(X - c.X, Y - c.Y, Z - c.Z);
        }

        public Coordinate3D Invert()
        {
            return new Coordinate3D(X *-1, Y *-1, Z *-1);
        }

        public override string ToString()
        {
            return $"({X},{Y},{Z})";
        }

        public Coordinate3D Rotate(Rotation r)
        {
            var result = Rotate(0, r.AboutX);
            result = result.Rotate(1, r.AboutY);
            result = result.Rotate(2, r.AboutZ);
            return result;

        }

        public Coordinate3D RotateReverse(Rotation r)
        {
            var result = Rotate(2, 4-r.AboutZ);
            result = result.Rotate(1, 4-r.AboutY);
            result = result.Rotate(0, 4-r.AboutX);
            return result;

        }

        public Coordinate3D Rotate(int axis, int numRotations)
        {
            var result = (Coordinate3D) Clone();
            for (int i = 0; i < numRotations; i++)
            {
                switch (axis)
                {
                    case 0:
                    {
                        result = new Coordinate3D(result.X, result.Z, -result.Y);
                        break;
                    }
                    case 1:
                    {
                        result = new Coordinate3D(result.Z, result.Y, -result.X);
                        break;
                    }
                    case 2:
                    {
                        result = new Coordinate3D(result.Y, -result.X, result.Z);
                        break;
                    }
                    default:
                    {
                        result = new Coordinate3D(result.X, result.Y, result.Z);
                        break;
                    }
                }
            }

            return result;

        }

        public Coordinate3D Transform(Transformation t)
        {
            
            switch (t.Reverse)
            {
                case 1:
                {
                    return new Coordinate3D(X * t.XShift, Z * t.ZShift, Y * t.YShift);

                }
                case 2: 
                {
                    return new Coordinate3D(Y * t.YShift, X * t.XShift, Z * t.ZShift);
                    }
                case 3:
                {
                    return new Coordinate3D(Y * t.YShift, Z * t.ZShift, X * t.XShift);
                }
                case 4: // z
                {
                    return new Coordinate3D(Z * t.ZShift, X * t.XShift, Y * t.YShift);
                }
                case 5: // z
                {
                    return new Coordinate3D(Z * t.ZShift, Y * t.YShift, X * t.XShift);
                }
                default: return new Coordinate3D(X * t.XShift, Y*t.YShift, Z*t.ZShift);
            }
        }

        public object Clone()
        {
            return new Coordinate3D(X, Y, Z);
        }

        public bool Equals(Coordinate3D other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Coordinate3D) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }

        public static bool operator ==(Coordinate3D left, Coordinate3D right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Coordinate3D left, Coordinate3D right)
        {
            return !Equals(left, right);
        }
    }
}