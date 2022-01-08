using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2021.General;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day24
{
    public class Day24Puzzle1: IPuzzleSolver
    {
        public Day24Puzzle1()
        {
            
        }

        public string Run()
        {
            var operations = File
                .ReadAllLines(@"C:\\aoc\day24\24_1.txt")
                .ToList();

            var result = Execute(operations, "91811241911641");

            return result["z"].Value.ToString();
        }

        private Dictionary<string, Variable> Execute(List<string> operations, string input)
        {
            var reader = new AluInputReader(input);
            var mem = ZeroMem();
            var alu = new Alu(reader, mem);
            alu.Execute(operations);
            return mem;
        }

        private Dictionary<string, Variable> ZeroMem()
        {
            return new Dictionary<string, Variable>
            {
                {"w", new Variable("w", 0)},
                {"x", new Variable("x", 0)},
                {"y", new Variable("y", 0)},
                {"z", new Variable("z", 0)}
            };
        }
    }
}