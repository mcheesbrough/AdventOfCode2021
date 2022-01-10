using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day21
{
    public interface IDiracDice
    {
        void Play(int player1Start, int player2Start, int winningScore, out long loserScore, out long winnerScore, out long numRolls);
        long PlayQuantum(int player1Start, int player2Start, int winningScore);
    }

    public class DiracDice : IDiracDice
    {
        private readonly Dice _dice;

        public DiracDice()
        {
            _dice = new Dice();
        }

        public long PlayQuantum(int player1Start, int player2Start, int winningScore)
        {

            var gameStates = new Dictionary<DiracDiceGameState, long>
            {
                {new DiracDiceGameState(player1Start, player2Start, winningScore), 1}
            };

            while (gameStates.Any(g => !g.Key.SomeoneWon))
            {
                gameStates = QuantumTurn(gameStates, true);
                Console.WriteLine($"Number in play after p1 {gameStates.Sum(g => g.Value)}");
                //foreach (var gameState in gameStates)
                //{
                //    Console.WriteLine($"{gameState.Key.Player1Score} {gameState.Key.Player2Score} {gameState.Value}");
                //}
                gameStates = QuantumTurn(gameStates, false);
                Console.WriteLine($"Number in play after p2 {gameStates.Sum(g => g.Value)}");
                //foreach (var gameState in gameStates)
                //{
                //    Console.WriteLine($"{gameState.Key.Player1Score} {gameState.Key.Player2Score} {gameState.Value}");
                //}
                var numberWon = gameStates.Where(g => g.Key.SomeoneWon)
                    .Sum(g => g.Value);
                var numberNotWon = gameStates.Where(g => !g.Key.SomeoneWon)
                    .Sum(g => g.Value);
                Console.WriteLine($"Num won {numberWon}");
                Console.WriteLine($"Num not won {numberNotWon}");
            }

            var player1Wins = gameStates
                .Where(g => g.Key.Player1Won)
                .Sum(g => g.Value);
            var player2Wins = gameStates
                .Where(g => g.Key.Player2Won)
                .Sum(g => g.Value);
            Console.WriteLine($"P1 wins {player1Wins}");
            Console.WriteLine($"P2 wins {player2Wins}");

            return player1Wins > player2Wins ? player1Wins : player2Wins;
        }

        private Dictionary<DiracDiceGameState, long> QuantumTurn(Dictionary<DiracDiceGameState, long> states, bool isPlayer1)
        {
            var newStates = states.Where(g => g.Key.SomeoneWon)
                .ToDictionary(p => p.Key, p=> p.Value);
            var gameStatesStillInPlay = states.Where(g => !g.Key.SomeoneWon).ToList();
            foreach (var state in gameStatesStillInPlay)
            {
                var oldState = state.Key.Clone();

                for (var i = 1; i <= 3; i++)
                {
                    for (var j = 1; j <= 3; j++)
                    {
                        for (var k = 1; k <= 3; k++)
                        {
                            var newState = oldState.Clone();

                            newState.Move(isPlayer1, i + j + k);
                            AddState(newStates, newState, states[oldState]);
                        }
                    }
                }
                
            }

            return newStates;
        }

        private void AddState(Dictionary<DiracDiceGameState, long> states, DiracDiceGameState state, long value)
        {
            if (states.ContainsKey(state))
            {
                states[state] += value;
            }
            else
            {
                states.Add(state, value);
            }

        }

        public void Play(int player1Start, int player2Start, int winningScore, out long loserScore, out long winnerScore, out long numRolls)
        {
            long player1Score = 0;
            long player2Score = 0;
            var player1Position = player1Start;
            var player2Position = player2Start;
            while (true)
            {
                player1Position = Roll(player1Position, 3);
                player1Score += player1Position;
                if (player1Score >= winningScore) break;
                player2Position = Roll(player2Position, 3);
                player2Score += player2Position;
                if (player2Score >= winningScore) break;
            }

            loserScore = player1Score >= 1000 ? player2Score : player1Score;
            winnerScore = player1Score >= 1000 ? player1Score : player2Score;
            numRolls = _dice.NumRolls;
        }

        private int Roll(int position, int rolls)
        {
            var moves = 0;
            for (var i = 0; i < rolls; i++)
            {
                moves += _dice.Roll();

            }
            var newPosition = (position + moves) % 10;
            if (newPosition == 0) newPosition = 10;

            return newPosition;
        }
    }

      
}