using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Model
{
    public class BingoCard
    {
        private readonly List<Cell> _cells;
        private const int Width = 5;
        private const int Height = 5;

        public BingoCard(List<int> numbers)
        {
            if (numbers.Count != Width * Height) throw new Exception($"{numbers.Count} cells");
            _cells = numbers.Select(x => new Cell(x, false)).ToList();
        }

        public IEnumerable<IEnumerable<Cell>> Rows => _cells.Select((c, i) => new
            {
                C = c,
                R = i / 5
            })
            .GroupBy(x => x.R, x => x.C, (r, c) => new {R = r, C = c})
            .Select(x => x.C);

        public IEnumerable<IEnumerable<Cell>> Cols => _cells.Select((c, i) => new
            {
                C = c,
                R = i % 5
            })
            .GroupBy(x => x.R, x => x.C, (r, c) => new { R = r, C = c })
            .Select(x => x.C);

        public int SumUnchecked => _cells.Where(x => !x.Checked).Sum(x => x.Number);

        public void Check(int number)
        {
            var cells = _cells.Where(x => x.Number == number);
            foreach (var cell in cells)
            {
                cell.Checked = true;
            }
        }

        public bool IsBingo => Rows.Any(r => r.All(x => x.Checked)) || Cols.Any(c => c.All(x => x.Checked));


    }

    public class Cell
    {
        public Cell(int number, bool @checked)
        {
            Number = number;
            Checked = @checked;
        }

        public int Number { get; }
        public bool Checked { get; set; }
    }
}