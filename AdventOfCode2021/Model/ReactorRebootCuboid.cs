using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021.Model
{
    public class ReactorRebootCuboid : IEquatable<ReactorRebootCuboid>
    {
        public ReactorRebootCuboid(Coordinate3D topLeft, Coordinate3D bottomRight)
        {
            TopLeft = topLeft;
            BottomRight = bottomRight;
        }

        public Coordinate3D TopLeft { get; }
        public Coordinate3D BottomRight { get; }

        public long Volume => (long)(Math.Abs(BottomRight.X - TopLeft.X) + 1)
                                            * (long)(Math.Abs(BottomRight.Y - TopLeft.Y) + 1)
                              * (long)(Math.Abs(BottomRight.Z - TopLeft.Z) + 1);
        public bool Overlaps(ReactorRebootCuboid cuboid)
        {
            if (TopLeft.X > cuboid.BottomRight.X) return false;
            if (BottomRight.X < cuboid.TopLeft.X) return false;
            if (TopLeft.Y > cuboid.BottomRight.Y) return false;
            if (BottomRight.Y < cuboid.TopLeft.Y) return false;
            if (TopLeft.Z > cuboid.BottomRight.Z) return false;
            if (BottomRight.Z < cuboid.TopLeft.Z) return false;

            return true;
        }

        public List<ReactorRebootCuboid> Split(ReactorRebootCuboid cuboid)
        {
            var newCubes = new List<ReactorRebootCuboid>();
            if (!Overlaps(cuboid)) return newCubes;

            var xs = new List<int>
            {
                TopLeft.X, BottomRight.X+1, cuboid.TopLeft.X, cuboid.BottomRight.X+1
            }.Distinct().OrderBy(x => x).ToList();
            var ys = new List<int>
            {
                TopLeft.Y, BottomRight.Y+1, cuboid.TopLeft.Y, cuboid.BottomRight.Y+1
            }.Distinct().OrderBy(y => y).ToList();
            var zs = new List<int>
            {
                TopLeft.Z, BottomRight.Z+1, cuboid.TopLeft.Z, cuboid.BottomRight.Z+1
            }.Distinct().OrderBy(z => z).ToList();
            for (var x = 0; x < xs.Count-1; x++)
            {
                for (var y = 0; y < ys.Count - 1; y++)
                {
                    for (var z = 0; z < zs.Count - 1; z++)
                    {
                        var (x1, x2) = GetMinAndMax(xs, x);
                        var (y1, y2) = GetMinAndMax(ys, y);
                        var (z1, z2) = GetMinAndMax(zs, z);

                        newCubes.Add(new ReactorRebootCuboid(
                            new Coordinate3D(x1, y1, z1),
                            new Coordinate3D(x2, y2, z2)));
                    }
                }
            }

            var filteredCubes = newCubes
                .Where(c => !c.Overlaps(cuboid) && Overlaps(c)).ToList();

            return filteredCubes;
        }

        private (int, int) GetMinAndMax(List<int> values, int index)
        {
            var min = values[index];
            var max = values[index + 1] - 1;
            return (min, max);
        }
        public override string ToString()
        {
            return $"{TopLeft.ToString()} -> {BottomRight.ToString()}";
        }

        public string PrintContainedCoords()
        {
            var output = new StringBuilder();
            for (var x = TopLeft.X; x <= BottomRight.X; x++)
            {
                for (var y = TopLeft.Y; y <= BottomRight.Y; y++)
                {
                    for (var z = TopLeft.Z; z <= BottomRight.Z; z++)
                    {
                        output.AppendLine(new Coordinate3D(x, y, z).ToString());
                    }
                }
            }

            return output.ToString();
        }

        public bool Equals(ReactorRebootCuboid other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(TopLeft, other.TopLeft) && Equals(BottomRight, other.BottomRight);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ReactorRebootCuboid) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(TopLeft, BottomRight);
        }

        public static bool operator ==(ReactorRebootCuboid left, ReactorRebootCuboid right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ReactorRebootCuboid left, ReactorRebootCuboid right)
        {
            return !Equals(left, right);
        }
    }
}