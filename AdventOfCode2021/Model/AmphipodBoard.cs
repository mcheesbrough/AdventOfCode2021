using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace AdventOfCode2021.Model
{
    public class AmphipodBoard
    {
        public AmphipodBoard(List<Amphipod> amphipods)
        {
            EnergyUsed = 0;
            Amphipods = amphipods;
            Moves = new List<AmphipodMove>();
            SetStateAsString();
        }


        private AmphipodBoard(List<Amphipod> amphipods, int energy, List<AmphipodMove> moves)
        {
            EnergyUsed = energy;
            Amphipods = amphipods;
            Moves = moves;
            IsStuck = false;
            SetStateAsString();
        }

        public List<Amphipod> Amphipods { get; }
        public List<AmphipodMove> Moves { get; }
        public int EnergyUsed { get; private set; }
        public bool IsStuck { get; set; }
        public string StateAsString { get; private set; }
        public void Move(AmphipodMove move)
        {
            EnergyUsed += move.EnergyRequired;
            Amphipods.First( a => a == move.Amphipod).Move(move.Target);
            Moves.Add(move.Clone());
            SetStateAsString();
        }

        private void SetStateAsString()
        {
            StateAsString = string.Join(';', Amphipods
                .OrderBy(a => a.Position.X)
                .ThenBy(a => a.Position.Y)
                .ThenBy(a => a.Energy)
                .Select(a => a.ToString()));
        }
        public bool IsSpace(Coordinate c) => !_walls.Contains(c);

        public bool IsAvailable(Coordinate c) => IsSpace(c) && !Amphipods.Select(a => a.Position).Contains(c);

        public bool IsInRoom(Coordinate c) =>
            (c.X == 3 || c.X == 5 || c.X == 7 || c.X == 9)
            && (c.Y == 2 || c.Y == 3 || c.Y == 4 || c.Y == 5);

        public bool IsInHall(Coordinate c) => c.Y == 1;

        public bool RoomIsFree(Amphipod a)
        {
            return Amphipods.Where(a2 => a2.Position.X == RoomX(a)).All(a2 => a2.Energy == a.Energy);
        }

        public int RoomX(Amphipod a)
        {
            return a.Energy switch
            {
                1 => 3,
                10 => 5,
                100 => 7,
                1000 => 9,
                _ => throw new Exception()
            };
        }

        public bool InCorrectRoom(Amphipod a)
        {
            return a.Position.X == RoomX(a) && (a.Position.Y == 2 || a.Position.Y == 3 || a.Position.Y == 4 || a.Position.Y == 5);
        }

        public int LowestEmpty(int roomX)
        {
            var amphipodsInRoom = Amphipods.Where(a => a.Position.X == roomX);
            if (!amphipodsInRoom.Any()) return 5;
            return amphipodsInRoom.Min(a => a.Position.Y) - 1;
        }

        public List<Coordinate> Adjacent(Coordinate c)
        {
            var adjacent = new List<Coordinate>();
            if (c.X - 1 >= 0) adjacent.Add(new Coordinate(c.X - 1, c.Y));
            if (c.X + 1 < 13) adjacent.Add(new Coordinate(c.X + 1, c.Y));
            if (c.Y - 1 >= 0) adjacent.Add(new Coordinate(c.X, c.Y - 1));
            if (c.Y + 1 < 5) adjacent.Add(new Coordinate(c.X, c.Y + 1));
            return adjacent;
        }

        public bool IsRoomX(int x) => x == 3 || x == 5 || x == 7 || x == 9;

        public List<AmphipodMove> PossibleMoves
        {
            get
            {
                var possibleMoves = new List<AmphipodMove>();
                foreach (var amphipod in Amphipods)
                {
                    if (InCorrectRoom(amphipod) && amphipod.Position.Y == 5) continue;
                    if (InCorrectRoom(amphipod) && Amphipods
                        .Where(a => a.Position.X == amphipod.Position.X && a.Position.Y > amphipod.Position.Y)
                        .All(a => a.Energy == amphipod.Energy)) continue;

                    if (!Adjacent(amphipod.Position).Any(IsAvailable)) continue;
                    if (IsInHall(amphipod.Position) && !RoomIsFree(amphipod)) continue;
                    if (IsInRoom(amphipod.Position))
                    {
                        for (var x = amphipod.Position.X-1; x >= 1; x--)
                        {
                            if (IsRoomX(x)) continue;
                            if (!IsAvailable(new Coordinate(x, 1))) break;
                            possibleMoves.Add(new AmphipodMove(amphipod, new Coordinate(x, 1)));
                        }
                        for (var x = amphipod.Position.X + 1; x <= 11; x++)
                        {
                            if (IsRoomX(x)) continue;
                            if (!IsAvailable(new Coordinate(x, 1))) break;
                            possibleMoves.Add(new AmphipodMove(amphipod, new Coordinate(x, 1)));
                        }
                    }
                    if (IsInRoom(amphipod.Position) && !RoomIsFree(amphipod)) continue;
                    if (RoomIsFree(amphipod) && LowestEmpty(RoomX(amphipod)) + 1 == amphipod.Position.Y) continue;
                    var increment = amphipod.Position.X < RoomX(amphipod) ? 1 : -1;
                    var cannotReach = false;
                    for (var x = amphipod.Position.X+1*increment; x != RoomX(amphipod); x += 1*increment)
                    {
                        if (!IsAvailable(new Coordinate(x, 1)))
                        {
                            cannotReach = true;
                            break;
                        };
                    }
                    if (cannotReach) continue;
                    
                    var roomY = LowestEmpty(RoomX(amphipod));
                    possibleMoves.Add(new AmphipodMove(amphipod, new Coordinate(RoomX(amphipod), roomY)));
                }

                if (possibleMoves.Any(m => m.Target.X == 3 || m.Target.X == 5 || m.Target.X == 7 || m.Target.X == 9))
                {
                    possibleMoves = possibleMoves.Where(m =>
                        m.Target.X == 3 || m.Target.X == 5 || m.Target.X == 7 || m.Target.X == 9).ToList();
                }
                return possibleMoves.Distinct().ToList();
            }
        }


        public AmphipodBoard Clone()
        {
            return new AmphipodBoard(Amphipods.Select(a => (Amphipod) a.Clone()).ToList(), EnergyUsed, Moves.Select(m => m.Clone()).ToList());
        }

        public bool IsComplete => Amphipods.All(InCorrectRoom);

        private List<Coordinate> _walls = new List<Coordinate>
        {
            new Coordinate(0, 0),
            new Coordinate(1, 0),
            new Coordinate(2, 0),
            new Coordinate(3, 0),
            new Coordinate(4, 0),
            new Coordinate(5, 0),
            new Coordinate(6, 0),
            new Coordinate(7, 0),
            new Coordinate(8, 0),
            new Coordinate(9, 0),
            new Coordinate(10, 0),
            new Coordinate(11, 0),
            new Coordinate(12, 0),
            new Coordinate(0, 1),
            new Coordinate(12, 1),
            new Coordinate(0, 2),
            new Coordinate(1, 2),
            new Coordinate(2, 2),
            new Coordinate(4, 2),
            new Coordinate(6, 2),
            new Coordinate(8, 2),
            new Coordinate(10, 2),
            new Coordinate(11, 2),
            new Coordinate(12, 2),
            new Coordinate(2, 3),
            new Coordinate(4, 3),
            new Coordinate(6, 3),
            new Coordinate(8, 3),
            new Coordinate(10, 3),
            new Coordinate(2, 4),
            new Coordinate(4, 4),
            new Coordinate(6, 4),
            new Coordinate(8, 4),
            new Coordinate(10, 4),
            new Coordinate(2, 5),
            new Coordinate(4, 5),
            new Coordinate(6, 5),
            new Coordinate(8, 5),
            new Coordinate(10, 5),
            new Coordinate(2, 6),
            new Coordinate(3, 6),
            new Coordinate(4, 6),
            new Coordinate(5, 6),
            new Coordinate(6, 6),
            new Coordinate(7, 6),
            new Coordinate(8, 6),
            new Coordinate(9, 6),
            new Coordinate(10, 6)

        };
    }

    public class Amphipod : IEquatable<Amphipod>
    {


        public Amphipod(int energy, Coordinate position)
        {
            Energy = energy;
            Position = position;
        }

        public Coordinate Position { get; private set; }
        public int Energy { get; }
        public override string ToString()
        {
            return Energy switch
            {
                1 => $"Amber {Position}",
                10 => $"Bronze {Position}",
                100 => $"Copper {Position}",
                1000 => $"Desert {Position}",
            };
        }

        public void Move(Coordinate c)
        {
            Position = c;
        }
        public Amphipod Clone()
        {
            return new Amphipod(Energy, (Coordinate)Position.Clone());
        }

        public static Amphipod FromDescription(string description)
        {
            var parts = description.Split(',').Select(int.Parse).ToArray();
            if (parts.Length != 3) throw new Exception("Not three parts in description");
            return new Amphipod(parts[2], new Coordinate(parts[0], parts[1]));
        }



        public bool Equals(Amphipod other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Position, other.Position) && Energy == other.Energy;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Amphipod)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Position, Energy);
        }

        public static bool operator ==(Amphipod left, Amphipod right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Amphipod left, Amphipod right)
        {
            return !Equals(left, right);
        }
    }

    public class AmphipodMove : IEquatable<AmphipodMove>
    {
 

        public AmphipodMove(Amphipod amphipod, Coordinate target)
        {
            Amphipod = amphipod;
            Target = target;
            EnergyRequired = CalcEnergy(amphipod, target);
        }
        private int CalcEnergy(Amphipod a, Coordinate c)
        {
            var steps = Math.Abs(a.Position.X - c.X);
            steps += a.Position.Y - 1 + c.Y - 1;
            return steps * a.Energy;
        }

        public Amphipod Amphipod { get; }
        public Coordinate Target { get; }
        public int EnergyRequired { get; }

        public override string ToString()
        {
            return $"{Amphipod} to {Target}";
        }

        public AmphipodMove Clone()
        {
            return new AmphipodMove(Amphipod.Clone(), (Coordinate)Target.Clone());
        }
        public bool Equals(AmphipodMove other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Amphipod, other.Amphipod) && Equals(Target, other.Target);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AmphipodMove)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Amphipod, Target);
        }

        public static bool operator ==(AmphipodMove left, AmphipodMove right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(AmphipodMove left, AmphipodMove right)
        {
            return !Equals(left, right);
        }
    }
}