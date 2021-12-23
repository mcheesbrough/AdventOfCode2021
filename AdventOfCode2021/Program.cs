using Microsoft.Extensions.DependencyInjection;
using System;
using AdventOfCode2021.Days.Day1;
using AdventOfCode2021.Days.Day10;
using AdventOfCode2021.Days.Day11;
using AdventOfCode2021.Days.Day12;
using AdventOfCode2021.Days.Day13;
using AdventOfCode2021.Days.Day14;
using AdventOfCode2021.Days.Day15;
using AdventOfCode2021.Days.Day16;
using AdventOfCode2021.Days.Day17;
using AdventOfCode2021.Days.Day18;
using AdventOfCode2021.Days.Day19;
using AdventOfCode2021.Days.Day2;
using AdventOfCode2021.Days.Day20;
using AdventOfCode2021.Days.Day3;
using AdventOfCode2021.Days.Day4;
using AdventOfCode2021.Days.Day5;
using AdventOfCode2021.Days.Day6;
using AdventOfCode2021.Days.Day7;
using AdventOfCode2021.Days.Day8;
using AdventOfCode2021.Days.Day9;
using AdventOfCode2021.General;
using ChitonMapLoader = AdventOfCode2021.Days.Day15.ChitonMapLoader;

namespace AdventOfCode2021
{
    class Program
    {
        static void Main(string[] args)
        {
            const string day = "20";
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
                .AddTransient<Day7Puzzle1>()
                .AddTransient<Day8Puzzle1>()
                .AddTransient<Day9Puzzle1>()
                .AddTransient<Day10Puzzle1>()
                .AddTransient<Day11Puzzle1>()
                .AddTransient<Day12Puzzle1>()
                .AddTransient<Day13Puzzle1>()
                .AddTransient<Day14Puzzle1>()
                .AddTransient<Day15Puzzle1>()
                .AddTransient<Day16Puzzle1>()
                .AddTransient<Day17Puzzle1>()
                .AddTransient<Day18Puzzle1>()
                .AddTransient<Day19Puzzle1>()
                .AddTransient<Day20Puzzle1>()
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
                .AddTransient<IBestCrabPositionFinder, BestCrabPositionFinderExponential>()
                .AddTransient<IOutputDigitCounter, OutputDigitCounter>()
                .AddTransient<IDigitFinder, DigitFinder>()
                .AddTransient<IHeatMapLoader, HeatMapLoader>()
                .AddTransient<ILowPointFinder, LowPointFinder>()
                .AddTransient<IBasinFinder, BasinFinder>()
                .AddTransient<ISyntaxChecker, SyntaxChecker>()
                .AddTransient<ISyntaxCompleter, SyntaxCompleter>()
                .AddTransient<IOctopusTurnProcessor, OctopusTurnProcessor>()
                .AddTransient<IOctopusTurnRunner, OctopusTurnRunner>()
                .AddTransient<ICaveLoader, CaveLoader>()
                .AddTransient<ICavePathFinder, CavePathFinder>()
                .AddTransient<IFolder, Folder>()
                .AddTransient<IInstructionPaperLoader, InstructionPaperLoader>()
                .AddTransient<IPairInsertionRuleLoader, PairInsertionRuleLoader>()
                .AddTransient<IPolymerInserter, PolymerInserter>()
                .AddTransient<IChitonPathFinder, ChitonPathFinder>()
                .AddTransient<IChitonMapLoader, ChitonMapLoader>()
                .AddTransient<IPacketParser, PacketParser>()
                .AddTransient<IProbeInputReader, ProbeInputReader>()
                .AddTransient<IProbeCalculator, ProbeCalculator>()
                .AddTransient<IScannerLoader, ScannerLoader>()
                .AddTransient<IScannerComparer, ScannerComparer>()
                .AddTransient<IImageLoader, ImageLoader>()
                .AddTransient<IImageProcessor, ImageProcessor>()
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
