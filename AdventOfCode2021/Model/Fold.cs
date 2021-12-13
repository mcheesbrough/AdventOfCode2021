namespace AdventOfCode2021.Model
{
    public class Fold
    {
        public Fold(FoldAlong foldAlong, int line)
        {
            FoldAlong = foldAlong;
            Line = line;
        }

        public static Fold FromDescription(string description) 
        {
            var stripped = description.Replace("fold along", string.Empty).Trim();
            var parts = stripped.Split('=');
            var foldirection = parts[0] == "y" ? Model.FoldAlong.Horizontal : Model.FoldAlong.Vertical;
            return new Fold(foldirection, int.Parse(parts[1]));
        }

        public FoldAlong FoldAlong { get;  }
        public int Line { get;  }
    }

    public enum FoldAlong
    {
        Horizontal,
        Vertical
    }
}