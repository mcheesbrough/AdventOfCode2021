using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day23
{
    public interface IAmphipodArranger
    {
        int Arrange(List<Amphipod> amphipods);
    }

    public class AmphipodArranger : IAmphipodArranger
    {
        public int Arrange(List<Amphipod> amphipods)
        {
            var boards = new List<AmphipodBoard>
            {
                new AmphipodBoard(amphipods)
            };
            while (!boards.All(a => a.IsComplete || a.IsStuck))
            {
                var updatedBoards = new List<AmphipodBoard>();
                Console.WriteLine($"Complete {boards.Count(b => b.IsComplete)}");
                Console.WriteLine($"Stuck {boards.Count(b => b.IsStuck)}");
                Console.WriteLine($"In play {boards.Count(b => !b.IsComplete && !b.IsStuck)}");
                foreach (var board in boards.Where(b => !b.IsStuck))
                {
                    if (board.IsComplete)
                    {
                        updatedBoards.Add(board);
                        continue;
                    }
                    var possibleMoves = board.PossibleMoves;
                    if (!possibleMoves.Any())
                    {
                        board.IsStuck = true;
                        continue;
                    }
                    foreach (var possibleMove in possibleMoves)
                    {
                        var boardToMove = board.Clone();
                        boardToMove.Move(possibleMove);
                        updatedBoards.Add(boardToMove);
                    }
                }

                var dedupedBoards = new Dictionary<string, AmphipodBoard>();
                Console.WriteLine($"Starting dedupe of {updatedBoards.Count}");
                foreach (var updatedBoard in updatedBoards)
                {
                    if (dedupedBoards.ContainsKey(updatedBoard.StateAsString)) continue;
                    var samePositions = updatedBoards
                        .Where(b => b.StateAsString == updatedBoard.StateAsString).ToList();
                    dedupedBoards.Add(samePositions[0].StateAsString, samePositions.OrderBy(b => b.EnergyUsed).First()); 
                }
                Console.WriteLine($"Ending dedupe with {dedupedBoards.Count}");
                boards = dedupedBoards.Values.ToList();
            }

            var leastEnergy = boards.Where(b => b.IsComplete).OrderBy(b => b.EnergyUsed).First();
            leastEnergy.Moves.ForEach(m => Console.WriteLine(m));
            return leastEnergy.EnergyUsed;
        }
    }
}