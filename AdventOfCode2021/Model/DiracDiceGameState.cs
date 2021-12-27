using System;
using AdventOfCode2021.Days.Day21;

namespace AdventOfCode2021.Model
{
    public class DiracDiceGameState : IEquatable<DiracDiceGameState>
    {
 

        private readonly int _toWin;

        public DiracDiceGameState(int player1Position, int player2Position, int toWin)
        {
            _toWin = toWin;
            Player1Position = player1Position;
            Player2Position = player2Position;
            Player1Score = 0;
            Player2Score = 0;
        }
        public void Move(bool isPlayer1, int moves)
        {
            if (isPlayer1)
            {
                Player1Position = Move(Player1Position, moves);
                Player1Score += Player1Position;
            }
            else
            {
                Player2Position = Move(Player2Position, moves);
                Player2Score += Player2Position;
            }
        }

        public bool Player1Won => Player1Score >= _toWin;
        public bool Player2Won => Player2Score >= _toWin;
        public bool SomeoneWon => Player1Won || Player2Won;

        public DiracDiceGameState Clone()
        {
            var newState = new DiracDiceGameState(Player1Position, Player2Position, _toWin)
            {
                Player1Score = Player1Score, Player2Score = Player2Score
            };
            return newState;
        }

        private int Move(int position, int moves)
        {
            var newPosition = (position + moves) % 10;
            if (newPosition == 0) newPosition = 10;
            return newPosition;
        }

        public int Player1Score { get; private set; }
        public int Player2Score { get; private set; }
        public int Player1Position { get; private set; }
        public int Player2Position { get; private set; }


        public bool Equals(DiracDiceGameState other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Player1Score == other.Player1Score && Player2Score == other.Player2Score && Player1Position == other.Player1Position && Player2Position == other.Player2Position;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DiracDiceGameState)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Player1Score, Player2Score, Player1Position, Player2Position);
        }

        public static bool operator ==(DiracDiceGameState left, DiracDiceGameState right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(DiracDiceGameState left, DiracDiceGameState right)
        {
            return !Equals(left, right);
        }
    }
}