using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day13
{
    public interface IFolder
    {
        Paper Fold(Paper paper, Fold fold);
    }

    public class Folder : IFolder
    {
        public Paper Fold(Paper paper, Fold fold)
        {
            var newDots = new List<Coordinate>();

            if (fold.FoldAlong == FoldAlong.Vertical)
            {
                foreach (var dot in paper.Dots)
                {
                    var newPos = new Coordinate(dot.X, dot.Y);
                    if (dot.X > fold.Line)
                    {
                        newPos = new Coordinate(paper.Width - dot.X - 1, dot.Y);
                    }
                    if (!newDots.Contains(newPos)) newDots.Add(newPos);

                }

                return new Paper(newDots, paper.Width / 2, paper.Height);

            }

            foreach (var dot in paper.Dots)
            {
                var newPos = new Coordinate(dot.X, dot.Y);
                if (dot.Y > fold.Line)
                {
                    newPos = new Coordinate(dot.X, paper.Height - dot.Y - 1);
                }
                if (!newDots.Contains(newPos)) newDots.Add(newPos);

            }
            return new Paper(newDots, paper.Width, paper.Height / 2);

        }


        private int CalcExcess(int max, int foldLine)
        {
            return (max - foldLine * 2 - 1) * -1;
        }
         
    }
}