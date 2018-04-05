namespace MovieManager.Models
{
    using System;
    using System.Windows.Media;

    public class Movie
    {
        public Movie()
        {
            BackdropColor = Core.TransparentColor;
        }

        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string FileLocation { get; set; }

        public int? MovieId { get; set; }

        public double? Rating { get; set; }

        public string Synopsis { get; set; }

        public string ImagePath { get; set; }

        public string TagLine { get; set; }

        public int? RunTime { get; set; }

        public Color BackdropColor { get; set; }
    }
}
