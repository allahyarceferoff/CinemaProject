using CinemaPlus.Commands;
using CinemaPlus.Views.UserControls.MovieUC;
using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace CinemaPlus.ViewModels.MovieViewModels
{
    public class MovieDetailUCViewModel : BaseViewModel
    {
        public RelayCommand IsMouseOverCommand { get; set; }
        public RelayCommand MouseLeaveCommand { get; set; }

        private ImageSource imageSource = null;

        public ImageSource ImageSource
        {
            get { return imageSource; }
            set { imageSource = value; OnPropertyChanged(); }
        }

        private string toolTipText = String.Empty;

        public string ToolTipText
        {
            get { return toolTipText; }
            set { toolTipText = value; OnPropertyChanged(); }
        }


        public MovieDetailUCViewModel()
        {
            IsMouseOverCommand = new RelayCommand((_popup) =>
            {
                var popup = _popup as Popup;
                popup.PlacementTarget = popup;
                popup.Placement = PlacementMode.Bottom;
                popup.IsOpen = true;
            });

            MouseLeaveCommand = new RelayCommand((_popup) =>
            {
                var popup = _popup as Popup;
                popup.Visibility = Visibility.Hidden;
                popup.IsOpen = false;
            });
        }
    }
}
