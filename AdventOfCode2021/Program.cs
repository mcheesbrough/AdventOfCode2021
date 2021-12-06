using Microsoft.Extensions.DependencyInjection;
using System;
using AdventOfCode2021.Days.Day1;
using AdventOfCode2021.Days.Day2;
using AdventOfCode2021.Days.Day3;
using AdventOfCode2021.Days.Day4;
using AdventOfCode2021.Days.Day5;
using AdventOfCode2021.Days.Day6;
using AdventOfCode2021.General;

namespace AdventOfCode2021
{
    class Program
    {
        static void Main(string[] args)
        {
            const string day = "6";
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
                .AddTransient<Day2Puzzle1>()
                .AddTransient<Day3Puzzle1>()
                .AddTransient<Day4Puzzle1>()
                .AddTransient<Day5Puzzle1>()
                .AddTransient<Day6Puzzle1>()
                .AddTransient<IDepthChangeCalc, ThreeMeasurementDepthChangeCalc>()
                .AddTransient<IMover, MoverAdvanced>()
                .AddTransient<IMovementInstructionsParser, MovementInstructionsParser>()
                .AddTransient<IGammaEpsilonFinder, GammaEpsilonFinder>()
                .AddTransient<IOxygenCo2Finder, OxygenCo2Finder>()
                .AddTransient<ICalculator, LifeSupportCalculator>()
                .AddTransient<IBingoParser, BingoParser>()
                .AddTransient<IBingoPlayer, BingoPlayer>()
                .AddTransient<ILineParser, LineParser>()
                .AddTransient<ILineIntersectionFinder, LineIntersectionFinder>()
                .AddTransient<IIntersectionCounter, IntersectionCounter>()
                .AddTransient<ILanternFishCalculator, LanternFishCalculator>()
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
