using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Days.Day19;
using AdventOfCode2021.Days.Day20;
using AdventOfCode2021.Days.Day21;
using AdventOfCode2021.Days.Day22;
using AdventOfCode2021.Days.Day23;
using AdventOfCode2021.Days.Day24;
using AdventOfCode2021.Model;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AdventOfCode2021Tests
{
    [TestFixture]
    public class Day24Fixture
    {
        [Test]
        [InlineAutoMoqData("1234", 1, 2)]

        public void CanReadInput(
            string inputString,
            int expected1,
            int expected2)
        {
            var reader = new AluInputReader(inputString);
            Assert.That(expected1, Is.EqualTo(reader.Read()));
            Assert.That(expected2, Is.EqualTo(reader.Read()));
        }

        [Test]
        [InlineAutoMoqData("1234", 1)]

        public void CanInp(
            string inputString,
            int expected)
        {
            var reader = new AluInputReader(inputString);

            var inp = new Inp(reader);
            Variable result = new Variable("x", 0);
            inp.Execute(result);
            Assert.That(result.Value, Is.EqualTo(expected));
        }

        [Test]
        [InlineAutoMoqData(1, 2, 3)]

        public void CanAdd(
            int param1,
            int param2,
            int expected)
        {
            var add = new Add();
            var result = new Variable("x", param1);
            var literal = new Literal(param2);
            add.Execute(result, literal);
            Assert.That(result.Value, Is.EqualTo(expected));
        }

        [Test]
        [InlineAutoMoqData(3, 2, 6)]

        public void CanMul(
            int param1,
            int param2,
            int expected)
        {
            var mul = new Mul();
            var result = new Variable("x", param1);
            var literal = new Literal(param2);
            mul.Execute( result, literal);
            Assert.That(result.Value, Is.EqualTo(expected));
        }

        [Test]
        [InlineAutoMoqData(5, 2, 2)]

        public void CanDiv(
            int param1,
            int param2,
            int expected)
        {
            var div = new Div();
            var result = new Variable("x", param1);
            var literal = new Literal(param2);
            div.Execute( result, literal);
            Assert.That(result.Value, Is.EqualTo(expected));
        }

        [Test]
        [InlineAutoMoqData(14, 10, 4)]

        public void CanMod(
            int param1,
            int param2,
            int expected)
        {
            var mod = new Mod();
            var result = new Variable("x", param1);
            var literal = new Literal(param2);
            mod.Execute( result, literal);
            Assert.That(result.Value, Is.EqualTo(expected));
        }

        [Test]
        [InlineAutoMoqData(7, 10, 0)]
        [InlineAutoMoqData(7, 7, 1)]

        public void CanEql(
            int param1,
            int param2,
            int expected)
        {
            var eql = new Eql();
            var result = new Variable("x", param1);
            var literal = new Literal(param2);
            eql.Execute( result, literal);
            Assert.That(result.Value, Is.EqualTo(expected));
        }

        [Test]
        [InlineAutoMoqData("1234", @"inp x
add x 1", "x2")]
        [InlineAutoMoqData("1234", @"inp x
inp y
add x y", "x3")]
        [InlineAutoMoqData("26", @"inp z
inp x
mul z 3
eql z x", "z1")]

        public void Can(
            string inputString,
            string operationsString,
            string expected)
        {
            var reader = new AluInputReader(inputString);
            var mem = new Dictionary<string, Variable>
            {
                {"w", new Variable("w", 0)}, 
                {"x", new Variable("x", 0)},
                {"y", new Variable("y", 0)},
                {"z", new Variable("z", 0)}
            };
            var alu = new Alu(reader, mem);
            var operations = operationsString.Split("\r\n");
            foreach (var operation in operations)
            {
                alu.Execute(operation);
            }
            Assert.That(mem[expected[0].ToString()].Value, Is.EqualTo(int.Parse(expected[1..])));
        }
    }
}
