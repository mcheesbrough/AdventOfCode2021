using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day24
{
    public interface IAlu
    {
        void Execute(string operationDescription);
        void Execute(List<string> operations);
    }

    public class Alu : IAlu
    {
        private readonly IAluInputReader _aluInputReader;
        private readonly Dictionary<string, Variable> _memory;

        public Alu(IAluInputReader aluInputReader, Dictionary<string, Variable> memory)
        {
            _aluInputReader = aluInputReader;
            _memory = memory;
        }

        private IAluOperation Create(string operationDescription)
        {
            var operationName = operationDescription.Substring(0, 3);
            return operationName switch
            {
                "inp" => new Inp(_aluInputReader),
                "mul" => new Mul(),
                "div" => new Div(),
                "mod" => new Mod(),
                "add" => new Add(),
                "eql" => new Eql(),
                _ => throw new Exception($"{operationName} not supported.")
            };
        }

        public void Execute(string operationDescription)
        {
            var operation = Create(operationDescription);
            var operands = operationDescription[3..].Trim().Split(' ').ToArray();
            if (!_memory.ContainsKey(operands[0])) throw new Exception($"{operands[0]} variable not supported.");
            var param1 = _memory[operands[0]];
            Operand param2 = null;
            if (operands.Length == 2)
            {
                if (int.TryParse(operands[1], out var intParam))
                {
                    param2 = new Literal(intParam);
                }
                else
                {
                    if (!_memory.ContainsKey(operands[1]))
                        throw new Exception($"{operands[1]} variable not supported.");
                    param2 = _memory[operands[1]];
                }
            }

            operation.Execute(param1, param2);
        }

        public void Execute(List<string> operations)
        {
            var alu = new Alu(_aluInputReader, _memory);
            foreach (var operation in operations)
            {
                alu.Execute(operation);
                Console.WriteLine($"{operation.PadRight(12)}{_memory["w"].Value} {_memory["x"].Value} {_memory["y"].Value} {_memory["z"].Value}");
            }
        }
    }

    public abstract class Operand
    {
        public long Value { get; protected set; }

        protected Operand(long value)
        {
            Value = value;
        }
    }

    public class Literal : Operand
    {
        public Literal(long value) : base(value)
        {
        }
    }

    public class Variable: Operand
    {
        public Variable(string name, long value): base(value)
        {
            Name = name;
        }

        public string Name { get; }

        public void SetValue(long value)
        {
            Value = value;
        }
    }
}