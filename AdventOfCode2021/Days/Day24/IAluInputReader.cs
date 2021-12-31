using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Days.Day24
{
    public interface IAluInputReader
    {
        int Read();
    }

    public class AluInputReader : IAluInputReader
    {
        private readonly Queue<int> _stream;

        public AluInputReader(string streamString)
        {
            var stream = streamString.ToCharArray().Select(n => int.Parse(n.ToString()));
            _stream = new Queue<int>(stream);
        }

        public int Read()
        {
            return _stream.Dequeue();
        }
    }
}