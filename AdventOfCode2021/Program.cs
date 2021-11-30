using Microsoft.Extensions.DependencyInjection;
using System;
using AdventOfCode2021.Days.Day1;
using AdventOfCode2021.General;

namespace AdventOfCode2021
{
    class Program
    {
        static void Main(string[] args)
        {
            const string day = "1";
            const string puzzle = "1";

            var serviceProvider = SetUpServices(day, puzzle);
            var puzzleSolver = GetPuzzleSolver(serviceProvider, day, puzzle);
            var result = puzzleSolver.Run();
            Console.WriteLine($"Answer for puzzle number {puzzle} on day {day} is {result}");

        }

        private static ServiceProvider SetUpServices(string day, string puzzle)
        {
            return new ServiceCollection()
                .AddTransient<Day1Puzzle1>()
                .BuildServiceProvider();
        }

        private static IPuzzleSolver GetPuzzleSolver(ServiceProvider serviceProvider, string day, string puzzle)
        {
            var puzzleSolverType = GetPuzzleTypeFromDayAndPuzzleNumber(day, puzzle);
            if (puzzleSolverType == null) throw new Exception($"No solver type for puzzle on day {day}, number {puzzle}");
            var puzzleSolver = (IPuzzleSolver)serviceProvider.GetService(puzzleSolverType);
            if (puzzleSolver == null) throw new Exception($"No solver registered for puzzle on day {day}, number {puzzle}");
            return puzzleSolver;
        }

        private static Type GetPuzzleTypeFromDayAndPuzzleNumber(string day, string puzzle)
        {
            var assembly = typeof(Program).Assembly;
            var type = assembly.GetType($"AdventOfCode2021.Days.Day{day}.Day{day}Puzzle{puzzle}");
            return type;
        }
    }


}
