using System;
using System.Linq;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day25
{
    public interface ISeaCucumberMover
    {
        int MoveUntilStuck(Map<SeaCucumberPosition> map);
    }

    public class SeaCucumberMover : ISeaCucumberMover
    {
        public int MoveUntilStuck(Map<SeaCucumberPosition> map)
        {
            var somethingMoved = true;
            var moves = 0;
            var currentMap = map.Clone();
            while (somethingMoved)
            {
                var nextMap = currentMap.Clone();
                somethingMoved = false;
                foreach (var seaCucumberPosition in currentMap.Points.Where(s => s.LeftRight != 0))
                {
                    var newPos = new Coordinate(seaCucumberPosition.Coordinate.X + seaCucumberPosition.LeftRight,
                        seaCucumberPosition.Coordinate.Y);
                    if (newPos.X >= map.Width) newPos = new Coordinate(0, seaCucumberPosition.Coordinate.Y);
                    somethingMoved |= TryMove( currentMap, nextMap, seaCucumberPosition, newPos);
                }
                currentMap = nextMap.Clone();

                foreach (var seaCucumberPosition in currentMap.Points.Where(s => s.UpDown != 0))
                {
                    var newPos = new Coordinate(seaCucumberPosition.Coordinate.X,
                        seaCucumberPosition.Coordinate.Y + seaCucumberPosition.UpDown);
                    if (newPos.Y >= map.Height) newPos = new Coordinate(seaCucumberPosition.Coordinate.X, 0);
                    somethingMoved |= TryMove(currentMap, nextMap, seaCucumberPosition, newPos);
                }

                currentMap = nextMap.Clone();
                //Console.WriteLine(currentMap.Print());
                //Console.WriteLine();
                if (somethingMoved) moves++;
            }

            return moves+1;
        }

        private bool TryMove(Map<SeaCucumberPosition> currentMap, Map<SeaCucumberPosition> nextMap, SeaCucumberPosition sc, Coordinate newPos)
        {
            var scAtNewPos = currentMap.Find(newPos);
            if (scAtNewPos == null)
                throw new Exception("Something went wrong, could not find sea cucumber position");
            if (scAtNewPos.IsEmpty)
            {
                var scNextMap = nextMap.Find(sc.Coordinate);
                var newScNextMap = nextMap.Find(newPos);
                scNextMap.Swap(newScNextMap);
                return true;
            }

            return false;
        }
    }
}