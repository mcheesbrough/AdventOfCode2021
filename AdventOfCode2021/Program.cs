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
            var puzzleSolver = serviceProvider.GetService<IPuzzleSolver>();
            var result = puzzleSolver.Run();
            Console.WriteLine($"Answer for puzzle number {puzzle} on day {day} is {result}");

        }

        private static ServiceProvider SetUpServices(string day, string puzzle)
        {
            var puzzleSolverType = GetPuzzleTypeFromDayAndPuzzleNumber(day, puzzle);
            if (puzzleSolverType == null) throw new Exception($"No solver found for puzzle on day {day}, number {puzzle}");
            return new ServiceCollection()
                .AddTransient<IPuzzleSolver, Day1Puzzle1>()
                .BuildServiceProvider();
        }

        private static Type GetPuzzleTypeFromDayAndPuzzleNumber(string day, string puzzle)
        {
            var assembly = typeof(Program).Assembly;
            var type = assembly.GetType($"AdventOfCode2021.Days.Day{day}.Day{day}Puzzle{puzzle}");
            var types = assembly.GetTypes();
            return type != null ? type : null;
        }
    }


}
