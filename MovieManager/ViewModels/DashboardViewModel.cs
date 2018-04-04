namespace MovieManager.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Linq;
    using System.Net;
    using System.Windows.Input;
    using System.Windows.Media;
    using Autofac;
    using Contracts.Commands.RelayCommands;
    using Contracts.Controllers;
    using Contracts.ViewModels;
    using MovieManager.Helpers;
    using MovieManager.Models;
    using Color = System.Windows.Media.Color;
    using PixelFormat = System.Drawing.Imaging.PixelFormat;

    public class DashboardViewModel : IDashboardViewModel
    {
        private readonly ICommonDataViewModel _commonData;
        private readonly IScanForLocalMovieFilesCommand _scanForLocalMovieFilesCommand;
        private readonly IShowEditMovieSettingsViewCommand _showEditMovieSettingsViewCommand;
        private readonly IPlayMovieCommand _playMovieCommand;
        private ObservableCollection<Movie> _movies;
        private Movie _selectedMovie;
        private Color _backgroundColor;

        public DashboardViewModel()
        {
            _commonData = AutofacInstaller.Container.Resolve<ICommonDataViewModel>();
            _scanForLocalMovieFilesCommand = AutofacInstaller.Container.Resolve<IScanForLocalMovieFilesCommand>();
            _showEditMovieSettingsViewCommand = AutofacInstaller.Container.Resolve<IShowEditMovieSettingsViewCommand>();
            _playMovieCommand = AutofacInstaller.Container.Resolve<IPlayMovieCommand>();

            PropertyChanged += PropertyChangedHandler;
            _commonData.PropertyChanged += CommonDataPropertyChanged;

            var fileController = AutofacInstaller.Container.Resolve<IFileController>();
            _commonData.CommonDataMovies = fileController.GetMovieDataFromLocalLibraryFile();
            
            _backgroundColor = Core.TransparentColor;
        }

        public ObservableCollection<Movie> Movies
        {
            get => _movies;
            set { PropertyChanged.ChangeAndNotify(ref _movies, value, () => Movies); }
        }

        public Movie SelectedMovie
        {
            get => _selectedMovie;
            set { PropertyChanged.ChangeAndNotify(ref _selectedMovie, value, () => SelectedMovie); }
        }

        public Color BackgroundColor
        {
            get => _backgroundColor;
            set { PropertyChanged.ChangeAndNotify(ref _backgroundColor, value, () => BackgroundColor); }
        }

        public Color BaseColor => Core.TransparentColor;

        public ICommand ScanForLocalMovieFilesCommand => _scanForLocalMovieFilesCommand.Command;

        public ICommand ShowEditMovieSettingsViewCommand => _showEditMovieSettingsViewCommand.Command;

        public ICommand PlayMovieCommand => _playMovieCommand.Command;

        public event PropertyChangedEventHandler PropertyChanged;

        private void PropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "SelectedMovie":
                    BackgroundColor = GetColor(SelectedMovie.ImagePath);
                    break;
                default:
                    break;
            }
        }

        private void CommonDataPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "CommonDataMovies":
                    Movies = _commonData.CommonDataMovies.OrderBy(x => x.Title).ToObservableCollection();
                    break;
                case "CommonDataSelectedMovie":
                    SelectedMovie = _commonData.CommonDataSelectedMovie;
                    break;
                default:
                    break;
            }
        }

        private static unsafe Color GetColor(string filePath)
        {
            filePath = filePath.StartsWith("\\") ? filePath.Substring(1) : filePath;
            var req = WebRequest.Create(new Uri("https://image.tmdb.org/t/p/w780" + filePath));
            var responseStream = req.GetResponse().GetResponseStream();

            if (responseStream == null)
            {
                return Core.TransparentColor;
            }

            using (var image = (Bitmap)Image.FromStream(responseStream))
            {
                if (image.PixelFormat != PixelFormat.Format24bppRgb)
                {
                    throw new NotSupportedException($"Unsupported pixel format: {image.PixelFormat}");
                }

                var bounds = new Rectangle(0, 0, image.Width, image.Height);
                var data = image.LockBits(bounds, ImageLockMode.ReadOnly, image.PixelFormat);

                long r = 0;
                long g = 0;
                long b = 0;

                const int pixelSize = 3;
                for (int y = 0; y < data.Height; ++y)
                {
                    byte* row = (byte*)data.Scan0 + (y * data.Stride);
                    for (int x = 0; x < data.Width; ++x)
                    {
                        var pos = x * pixelSize;
                        b += row[pos];
                        g += row[pos + 1];
                        r += row[pos + 2];
                    }
                }

                r = r / (data.Width * data.Height);
                g = g / (data.Width * data.Height);
                b = b / (data.Width * data.Height);
                image.UnlockBits(data);

                System.Drawing.Color colour;
                if (r >= g && r >= b)
                {
                    colour = System.Drawing.Color.FromArgb((int)r, 0, 0);
                }
                else if (g >= r && g >= b)
                {
                    colour = System.Drawing.Color.FromArgb(0, (int)g, 0);
                }
                else
                {
                    colour = System.Drawing.Color.FromArgb(0, 0, (int)b);
                }

                //var colour = Color.FromArgb((int)r, (int)g, (int)b);
                return Color.FromArgb(colour.A, colour.R, colour.G, colour.B);
            }
        }
    }
}
