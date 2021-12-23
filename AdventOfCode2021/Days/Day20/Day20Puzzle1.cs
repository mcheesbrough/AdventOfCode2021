using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2021.Days.Day19;
using AdventOfCode2021.General;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day20
{
    public class Day20Puzzle1: IPuzzleSolver
    {
        private readonly IImageLoader _imageLoader;
        private readonly IImageProcessor _imageProcessor;

        public Day20Puzzle1(IImageLoader imageLoader, IImageProcessor imageProcessor)
        {
            _imageLoader = imageLoader;
            _imageProcessor = imageProcessor;
        }

        public string Run()
        {
            var input = File
                .ReadAllText(@"C:\\aoc\day20\20_1.txt");

            _imageLoader.Load(input, out var enhancementAlgo, out var image);
            Console.Write(image.Print());

            var output = _imageProcessor.Process(image, enhancementAlgo);

            for (var i = 1; i <= 49; i++)
            {
                Console.Write(i);
                output = _imageProcessor.Process(output, enhancementAlgo);
            }
            Console.Write(output.Print());

            var count = output.Points.Count(p => p.Value);
            return count.ToString();
        }

    }
}