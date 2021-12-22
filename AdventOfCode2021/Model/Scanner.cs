using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Model
{
    public class Scanner
    {
        public Scanner(List<Coordinate3D> beaconCoordinates)
        {
            BeaconCoordinates = beaconCoordinates;
            var closest = BeaconCoordinates.OrderBy(b => b.DistanceFromOrigin).First();
            Position = null;
        }

        public List<Coordinate3D> BeaconCoordinates { get; private set; }
        public Coordinate3D Position { get; set; }
    }
}