namespace MovieManager
{
    using System.Collections.Generic;
    using System.Windows.Media;

    public static class Core
    {
        public static string DateTagRegex => @"\s\[[0-9]{4}\]|\[[0-9]{4}\]|\s\([0-9]{4}\)|\([0-9]{4}\)|\s[0-9]{4}";

        public static string PunctuationRegex => @"[ \f\n\r\t\v.,;:']";

        public static List<string> MovieFileTypes => new List<string> { ".mp4", ".avi", ".mov" };

        public static Color TransparentColor => Color.FromArgb(0, 0, 0, 0);
    }
}
