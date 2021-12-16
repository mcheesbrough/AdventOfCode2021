using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using AdventOfCode2021.Days.Day16;
using AdventOfCode2021.Model;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;

namespace AdventOfCode2021Tests
{
    [TestFixture]
    public class Day16Fixture
    {
        [Test]
        [InlineAutoMoqData("EE00D40C823060", "11101110000000001101010000001100100000100011000001100000")]
        public void CanConvertHexToBinString(
            string inputString,
            string expectedString)
        {
            Assert.That(Binary.BinaryStringFromHex(inputString), Is.EqualTo(expectedString));
        }

        [Test]
        [InlineAutoMoqData("0101001111111", 2)]
        [InlineAutoMoqData("1101001111111", 6)]
        public void GetsVersion(
            string inputString,
            int expectedVersion,
            PacketParser sut)
        {
            Assert.That(sut.ParseVersion(inputString), Is.EqualTo(expectedVersion));
        }

        [Test]
        [InlineAutoMoqData("100", PacketType.Literal)]
        [InlineAutoMoqData("001", PacketType.Operator)]
        [InlineAutoMoqData("010", PacketType.Operator)]
        [InlineAutoMoqData("011", PacketType.Operator)]
        [InlineAutoMoqData("000", PacketType.Operator)]
        [InlineAutoMoqData("101", PacketType.Operator)]
        [InlineAutoMoqData("110", PacketType.Operator)]
        [InlineAutoMoqData("111", PacketType.Operator)]
        public void GetsType(
            string inputString,
            PacketType expectedType,
            PacketParser sut)
        {
            Assert.That(sut.ParseType(inputString), Is.EqualTo(expectedType));
        }

        [Test]
        [InlineAutoMoqData("1111001000100010", 18)]
        [InlineAutoMoqData("11110000001", 1)]
        public void GetsLiteralValue(
            string inputString,
            int expectedLiteral,
            PacketParser sut)
        {
            Assert.That(((LiteralPacket)sut.Parse(inputString)).Value, Is.EqualTo(expectedLiteral));
        }

        [Test]
        public void ParsePacketsWithLieralsAndSubPacketLength()
        {
            var sut = new PacketParser();
            var input = "00111000000000000110111101000101001010010001001000000000";
            var result = sut.Parse(input);
            Assert.That(result.Version, Is.EqualTo(1));
            Assert.That(result.Type, Is.EqualTo(PacketType.Operator));
            Assert.That(result, Is.TypeOf(typeof(OperatorPacket)));
            Assert.That(((OperatorPacket)result).SubPackets[0], Is.TypeOf(typeof(LiteralPacket)));
            var subPacket1 = (LiteralPacket) ((OperatorPacket) result).SubPackets[0];
            Assert.That(subPacket1.Value, Is.EqualTo(10));
            var subPacket2 = (LiteralPacket)((OperatorPacket)result).SubPackets[1];
            Assert.That(subPacket2.Value, Is.EqualTo(20));
        }

        [Test]
        public void ParsePacketsWithLieralsAndSubPacketNumber()
        {
            var sut = new PacketParser();
            var input = "11101110000000001101010000001100100000100011000001100000";
            var result = sut.Parse(input);
            Assert.That(result.Version, Is.EqualTo(7));
            Assert.That(result.Type, Is.EqualTo(PacketType.Operator));
            Assert.That(result, Is.TypeOf(typeof(OperatorPacket)));
            Assert.That(((OperatorPacket)result).SubPackets[0], Is.TypeOf(typeof(LiteralPacket)));
            var subPacket1 = (LiteralPacket)((OperatorPacket)result).SubPackets[0];
            Assert.That(subPacket1.Value, Is.EqualTo(1));
            var subPacket2 = (LiteralPacket)((OperatorPacket)result).SubPackets[1];
            Assert.That(subPacket2.Value, Is.EqualTo(2));
            var subPacket3 = (LiteralPacket)((OperatorPacket)result).SubPackets[2];
            Assert.That(subPacket3.Value, Is.EqualTo(3));
        }

        [Test]
        public void ParseExample1()
        {
            var sut = new PacketParser();
            var input = "8A004A801A8002F478";
            var result = sut.Parse(Binary.BinaryStringFromHex(input));
            Assert.That(result.Version, Is.EqualTo(4));
            Assert.That(result, Is.TypeOf(typeof(OperatorPacket)));
            var subPacket1 = (OperatorPacket)((OperatorPacket)result).SubPackets[0];
            Assert.That(subPacket1.Version, Is.EqualTo(1));
            Assert.That(subPacket1, Is.TypeOf(typeof(OperatorPacket)));
            var subPacket2 = (OperatorPacket)subPacket1.SubPackets[0];
            Assert.That(subPacket2.Version, Is.EqualTo(5));
            Assert.That(subPacket2, Is.TypeOf(typeof(OperatorPacket)));
            var subPacket3 = (LiteralPacket)subPacket2.SubPackets[0];
            Assert.That(subPacket3.Version, Is.EqualTo(6));
        }

        [Test]
        [InlineAutoMoqData("8A004A801A8002F478", 16)]
        [InlineAutoMoqData("620080001611562C8802118E34", 12)]
        [InlineAutoMoqData("C0015000016115A2E0802F182340", 23)]
        [InlineAutoMoqData("A0016C880162017C3686B18A3D4780", 31)]

        public void CanSumVersions(
            string input,
            int expected,
            PacketParser sut)
        {
            var result = sut.Parse(Binary.BinaryStringFromHex(input)).Versions;
            var sumVersions = result.Sum();
            Assert.That(sumVersions, Is.EqualTo(expected));

        }

        [Test]
        [InlineAutoMoqData("C200B40A82", 3)]
        [InlineAutoMoqData("04005AC33890", 54)]
        [InlineAutoMoqData("880086C3E88112", 7)]
        [InlineAutoMoqData("CE00C43D881120", 9)]
        [InlineAutoMoqData("D8005AC2A8F0", 1)]
        [InlineAutoMoqData("F600BC2D8F", 0)]
        [InlineAutoMoqData("9C005AC2F8F0", 0)]
        [InlineAutoMoqData("9C0141080250320F1802104A08", 1)]

        public void CanExecute(
            string input,
            int expected,
            PacketParser sut)
        {
            var packet = sut.Parse(Binary.BinaryStringFromHex(input));
            var result = packet.Execute();
            Assert.That(result, Is.EqualTo(expected));

        }

    }
}
