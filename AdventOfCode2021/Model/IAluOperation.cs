using System;
using System.Threading;
using AdventOfCode2021.Days.Day24;

namespace AdventOfCode2021.Model
{
    public interface IAluOperation
    {
        void Execute(params Operand[] param);
    }

    public class Inp: IAluOperation
    {
        private readonly IAluInputReader _aluInputReader;

        public Inp(IAluInputReader aluInputReader)
        {
            _aluInputReader = aluInputReader;
        }

        public void Execute(params Operand[] param)
        {
            ((Variable)param[0]).SetValue(_aluInputReader.Read());
        }
    }

    public class Add : IAluOperation
    {
        public void Execute(params Operand[] param)
        {
            ((Variable)param[0]).SetValue(param[0].Value + param[1].Value);
        }
    }

    public class Mul : IAluOperation
    {
        public void Execute(params Operand[] param)
        {
            ((Variable)param[0]).SetValue(param[0].Value * param[1].Value);
        }
    }

    public class Div : IAluOperation
    {
        public void Execute(params Operand[] param)
        {
            ((Variable)param[0]).SetValue(Convert.ToInt32(Math.Floor(Convert.ToDouble(param[0].Value) / param[1].Value)));
        }
    }

    public class Mod : IAluOperation
    {
        public void Execute(params Operand[] param)
        {
            ((Variable)param[0]).SetValue(param[0].Value % param[1].Value);
        }
    }

    public class Eql : IAluOperation
    {
        public void Execute(params Operand[] param)
        {
            ((Variable)param[0]).SetValue(param[0].Value == param[1].Value ? 1 : 0);
        }
    }
}