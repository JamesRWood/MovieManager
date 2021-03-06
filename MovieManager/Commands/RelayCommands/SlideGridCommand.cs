﻿namespace MovieManager.Commands.RelayCommands
{
    using System;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using Contracts.Commands.RelayCommands;
    using MovieManager.Contracts.ViewModels;

    public class SlideGridCommand : ISlideGridCommand
    {
        private readonly ICommonDataViewModel _commonData;
        private ICommand _command;

        public SlideGridCommand(ICommonDataViewModel commonData)
        {
            _commonData = commonData;
        }

        public ICommand Command => _command ?? (_command = new RelayCommand<object>(Execute, CanExecute));

        public void Execute(object parameter)
        {
            var val = (object[])parameter;
            if (!(val[0] is double width))
            {
                return;
            }

            if (!(val[1] is int position))
            {
                return;
            }

            if (!(val[2] is UIElement grid))
            {
                return;
            }

            if (position == 2)
            {
                _commonData.CommonDataPossibleMatches = null;
                _commonData.CommonDataSelectedPossibleMatch = null;
                _commonData.CommonDataSearchTerm = null;
                _commonData.CommonDataSearchTerm = string.Empty;
            }

            const int xPos1 = 0;
            var xPos2 = 0 - (width - 40);

            var animation = position == 1 ? GetAnimation(xPos2, xPos1) : GetAnimation(xPos1, xPos2);

            var transform = new TranslateTransform();
            transform.BeginAnimation(TranslateTransform.XProperty, animation);

            grid.RenderTransform = transform;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        private DoubleAnimation GetAnimation(double from, double to)
        {
            return new DoubleAnimation
            {
                From = from,
                To = to,
                Duration = TimeSpan.FromSeconds(0.5),
                AccelerationRatio = 0.6
            };
        }

        public event EventHandler CanExecuteChanged;
    }
}
