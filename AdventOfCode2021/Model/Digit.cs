using System;
using System.Linq;

namespace AdventOfCode2021.Model
{
    public class Digit : IEquatable<Digit>
    {
        public Digit(string digitString)
        {
            DigitString = digitString.Trim();
        }

        public string DigitString { get; }

        public bool IsEasyDigit => DigitString.Length == 2 || DigitString.Length == 7 || DigitString.Length == 3 ||
                                   DigitString.Length == 4;

        public int EasyValue
        {
            get
            {
                switch (DigitString.Length)
                {
                    case 2: return 1;
                    case 3: return 7;
                    case 4: return 4;
                    case 7: return 8;
                    default: return -1;
                }
            }
        }

        public bool Contains(char[] chars)
        {
            foreach (var character in chars)
            {
                if (!DigitString.Contains(character)) return false;
            }

            return true;
        }

        public bool Equals(Digit other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            var string1 = new string(DigitString.ToCharArray().OrderBy(x => x).ToArray());
            var string2 = new string(other.DigitString.ToCharArray().OrderBy(x => x).ToArray());
            return string1 == string2;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Digit) obj);
        }

        public override int GetHashCode()
        {
            return (DigitString != null ? DigitString.GetHashCode() : 0);
        }

        public static bool operator ==(Digit left, Digit right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Digit left, Digit right)
        {
            return !Equals(left, right);
        }
    }
}