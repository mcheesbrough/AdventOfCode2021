using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace AdventOfCode2021.Model
{
    public class SnailFishNumber
    {

        public SnailFishNumber(SnailFishNumber left, SnailFishNumber right)
        {
            Left = left;
            Right = right;
            Depth = 0;
        }

        public SnailFishNumber(SnailFishNumber left, SnailFishNumber right, int depth)
        {
            Left = left;
            Right = right;
            Depth = depth;
        }

        public SnailFishNumber(int regular)
        {
            Regular = regular;
        }
        public int Depth { get; }
        public int? Regular { get; private set; } 
        public SnailFishNumber Left { get; private set; }
        public SnailFishNumber Right { get; private set; }
        public SnailFishNumber Parent { get; private set; }

        public static SnailFishNumber FromDescription(string description)
        {
            return FromDescription(description, 0);
        }

        public static SnailFishNumber FromDescription(string description, int depth)
        {
            var inner = FindInner(description);
            SnailFishNumber left;
            SnailFishNumber right;
            if (inner.Substring(0, 1) != "[")
            {
                left = new SnailFishNumber(GetInteger(inner));
            }
            else
            {
                left = SnailFishNumber.FromDescription(inner, depth + 1);
            }

            if (inner.Substring(left.ToString().Length,1) != ",") throw new Exception("Did not find comma");
            var rightString = inner[(left.ToString().Length + 1)..];

            if (rightString.Substring(0, 1) != "[")
            {
                right = new SnailFishNumber(GetInteger(rightString));
            }
            else
            {
                right = SnailFishNumber.FromDescription(rightString, depth + 1);
            }

            var newNumber = new SnailFishNumber(left, right, depth);
            left.Parent = newNumber;
            right.Parent = newNumber;
            return newNumber;
        }

        private static int GetInteger(string input)
        {
            var intString = string.Empty;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == ',' || input[i] == ']') return int.Parse(intString);
                intString += input[i];
            }

            return int.Parse(intString);
        }

        public SnailFishNumber Add(SnailFishNumber numberToAdd)
        {
            var provisional =  FromDescription(new SnailFishNumber(this, numberToAdd).ToString());
            if (provisional.Depths().Max( x => x.Item1) < 4) return provisional;

            provisional = provisional.FullyExplode();
            var isSplit = provisional.Split(out provisional);
            while (isSplit)
            {
                provisional = provisional.FullyExplode();
                isSplit = provisional.Split(out provisional);
            }

            return provisional;
        }

        private SnailFishNumber FullyExplode()
        {
            var exploded = Explode(out var explodedNumber);
            while (exploded)
            {
                exploded = explodedNumber.Explode(out explodedNumber);
            }

            return explodedNumber;
        }

        public bool Split(out SnailFishNumber result)
        {
            result = FromDescription(ToString());
            var firstLargerThanNine = result.FindFirstOverNine();
            if (firstLargerThanNine == null) return false;

            if (firstLargerThanNine.Left.Regular.HasValue && firstLargerThanNine.Left.Regular.Value > 9)
            {
                var val = firstLargerThanNine.Left.Regular.Value;
                firstLargerThanNine.Left = CreateSplitPair(val, firstLargerThanNine.Depth);
            }
            else
            {
                var val = firstLargerThanNine.Right.Regular.Value;
                firstLargerThanNine.Right = CreateSplitPair(val, firstLargerThanNine.Depth);
            }
            return true;

        }

        private SnailFishNumber CreateSplitPair(int val, int parentDepth)
        {
            return new SnailFishNumber(new SnailFishNumber(val / 2),
                new SnailFishNumber(Convert.ToInt32(Math.Ceiling(val / 2.0))), parentDepth + 1);
        }

        public SnailFishNumber FindFirstOverNine()
        {
            if (Left.Regular.HasValue)
            {
                if (Left.Regular.Value > 9) return this;
            }
            else
            {
                var left = Left.FindFirstOverNine();
                if (left != null) return left;
            }
            if (Right.Regular.HasValue)
            {
                if (Right.Regular.Value > 9) return this;
                return null;
            }

            return Right.FindFirstOverNine();
        }

        public bool Explode(out SnailFishNumber exploded)
        {
            exploded = FromDescription(ToString());
            var firstDepthFour = exploded.Depths().FirstOrDefault(x => x.Item1 == 4)?.Item2;
            if (firstDepthFour == null) return false;

            var left = firstDepthFour.FindFirstRegularToLeft();
            var right = firstDepthFour.FindFirstRegularToRight();
            if (left != null)
            {
                if (left.Item2) left.Item1.Left.Regular += firstDepthFour.Left.Regular;
                else left.Item1.Right.Regular += firstDepthFour.Left.Regular;
            }
            if (right != null)
            {
                if (right.Item2) right.Item1.Left.Regular += firstDepthFour.Right.Regular;
                else right.Item1.Right.Regular += firstDepthFour.Right.Regular;
            }
            var parent = firstDepthFour.Parent;
            if (parent.Left == firstDepthFour) parent.Left = new SnailFishNumber(0);
            if (parent.Right == firstDepthFour) parent.Right = new SnailFishNumber(0);

            return true;
        }

        public List<Tuple<int,SnailFishNumber>> Depths()
        {
            var depths = new List<Tuple<int, SnailFishNumber>>();
            if (Regular.HasValue) return depths;
            depths.Add(new Tuple<int, SnailFishNumber>(Depth, this));
            depths.AddRange(Left.Depths());
            depths.AddRange(Right.Depths());
            return depths;
        }


        public Tuple<SnailFishNumber, bool> FindFirstRegularToLeft()
        {
            var currentNumber = this;
            for (var i = Depth; i > 0; i--)
            {
                var nextNumber = currentNumber.Parent;
                if (nextNumber.Left == currentNumber)
                {
                    currentNumber = nextNumber;
                    continue;
                }

                if (nextNumber.Left.Regular.HasValue) return new Tuple<SnailFishNumber, bool>(nextNumber, true);
                return new Tuple<SnailFishNumber, bool>(FindFirstRegularRightGoingDown(nextNumber.Left), false);
            }

            return null;
        }

        public Tuple<SnailFishNumber, bool> FindFirstRegularToRight()
        {
            var currentNumber = this;
            for (var i = Depth; i > 0; i--)
            {
                var nextNumber = currentNumber.Parent;
                if (nextNumber.Right == currentNumber)
                {
                    currentNumber = nextNumber;
                    continue;
                }

                if (nextNumber.Right.Regular.HasValue) return new Tuple<SnailFishNumber, bool>(nextNumber, false); ;
                return new Tuple<SnailFishNumber, bool>(FindFirstRegularLeftGoingDown(nextNumber.Right), true);
            }

            return null;
        }

        public SnailFishNumber FindFirstRegularRightGoingDown(SnailFishNumber number)
        {
            if (number.Right.Regular.HasValue) return number;
            return FindFirstRegularRightGoingDown(number.Right);
        }

        public SnailFishNumber FindFirstRegularLeftGoingDown(SnailFishNumber number)
        {
            if (number.Left.Regular.HasValue) return number;
            return FindFirstRegularLeftGoingDown(number.Left);
        }


        private static string FindInner(string description)
        {
            if (description[0] != '[') throw new Exception("First char not [");

            var stack = new Stack<int>();
            for (int i = 0; i < description.Length; i++)
            {
                if (description[i] == '[') stack.Push(i);
                if (description[i] == ']') stack.Pop();
                if (stack.Count == 0)
                {
                    return description.Substring(1, i-1);
                }
            }

            throw new Exception("Did not find closing ]");
        }

        public override string ToString()
        {
            return Regular.HasValue ? Regular.Value.ToString() : $"[{Left},{Right}]";
        }

        public long Magnitude
        {
            get
            {
                var mag = (long)0;
                if (Left.Regular.HasValue)
                {
                    mag += Left.Regular.Value * 3;
                }
                else
                {
                    mag += Left.Magnitude * 3;
                }
                if (Right.Regular.HasValue)
                {
                    mag += Right.Regular.Value * 2;
                }
                else
                {
                    mag += Right.Magnitude * 2;
                }

                return mag;
            }
        }
    }
}