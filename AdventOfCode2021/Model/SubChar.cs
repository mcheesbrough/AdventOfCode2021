using System;

namespace AdventOfCode2021.Model
{
    public class SubChar
    {
        public SubChar(char value)
        {
            Value = value;
        }

        public char Value { get; }

        public char ClosingChar
        {
            get
            {
                return Value switch
                {
                    '(' => ')',
                    '{' => '}',
                    '[' => ']',
                    '<' => '>',
                    _ => throw new Exception("No closing char defined")
                };
            }
        }

        public bool IsOpening => "({[<".Contains(Value);
        public bool IsClosing => ")]}>".Contains(Value);
        public int Score
        {
            get
            {
                return Value switch
                {
                    ')' => 3,
                    '}' => 1197,
                    ']' => 57,
                    '>' => 25137,
                    _ => throw new Exception("No closing char defined")
                };
            }
        }

        public int CompletionScore
        {
            get
            {
                return Value switch
                {
                    ')' => 1,
                    '}' => 3,
                    ']' => 2,
                    '>' => 4,
                    _ => throw new Exception("No closing char defined")
                };
            }
        }
    }
}