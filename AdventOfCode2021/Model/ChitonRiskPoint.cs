using System;

namespace AdventOfCode2021.Model
{
    public class ChitonRiskPoint: IMapPoint, IEquatable<ChitonRiskPoint>
    {
  

        public ChitonRiskPoint(Coordinate coordinate, int risk)
        {
            Coordinate = coordinate;
            Risk = risk;
            IsVisited = false;
            RiskToHere = long.MaxValue;
        }

        public Coordinate Coordinate { get; }
        public int Risk { get; }
        public long RiskToHere { get; set; }
        public bool IsVisited { get; set; }
        public object Clone()
        {
            return new ChitonRiskPoint(new Coordinate(Coordinate.X, Coordinate.Y), Risk);
        }

        public override string ToString()
        {
            return Risk.ToString();
        }

        public bool Equals(ChitonRiskPoint other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Coordinate, other.Coordinate);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ChitonRiskPoint)obj);
        }

        public override int GetHashCode()
        {
            return (Coordinate != null ? Coordinate.GetHashCode() : 0);
        }

        public static bool operator ==(ChitonRiskPoint left, ChitonRiskPoint right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ChitonRiskPoint left, ChitonRiskPoint right)
        {
            return !Equals(left, right);
        }
    }
}