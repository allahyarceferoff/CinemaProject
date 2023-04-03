using CinemaPlus.Commands;
using CinemaPlus.Helpers;
using CinemaPlus.Helpers.MovieCellUCHelpers;
using CinemaPlus.Models;
using CinemaPlus.ViewModels.WindowsViewModel;
using CinemaPlus.Views.UserControls.MovieUC;
using CinemaPlus.Views.Windows;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace CinemaPlus.ViewModels.MovieViewModels
{
    public class MovieCellUCViewModel : BaseViewModel
    {
        public List<MovieDetailUC> MovieFormats { get; set; }

        private Models.Movie movie;

        public Models.Movie Movie
        {
            get { return movie; }
            set { movie = value; OnPropertyChanged(); }
        }

        private ImageSource posterImageSource;

        public ImageSource PosterImageSource
        {
            get { return posterImageSource; }
            set { posterImageSource = value; OnPropertyChanged(); }
        }

        public RelayCommand MovieClickCommand { get; set; }
        public RelayCommand SessionsCommand { get; set; }

        public MovieCellUCViewModel()
        {
            MovieClickCommand = new RelayCommand((m) =>
            {
                if (App.IsInAdminSide)
                {
                    App.SelectedMovieForEdit = Movie;
                    if (App.AdminSideEditSide)
                    {

                    }
                    else
                    {

                    }
                }
                else
                {
                    MovieCellUCHelper.MovieClick(Movie, Helper.Enums.MovieTabs.DescriptionTab);
                }
            });

            SessionsCommand = new RelayCommand((s) =>
            {
                if (App.IsInAdminSide)
                {
                    App.SelectedMovieForEdit = Movie;
                }
                else
                {
                    MovieCellUCHelper.MovieClick(Movie, Helper.Enums.MovieTabs.SessionsTab);
                }
            });
        }

  
            
    
       
  
      

        private bool SetSelectedHallAndMovie(Cinema cinema)
        {
            foreach (var hall in cinema.Halls)
            {
                foreach (var _movie in hall.HallMovies)
                {
                    if (_movie.Title == movie.Title)
                    {
                        //App.SelectedHall = hall.HallName;
                        App.SelectedMovieForEdit = _movie;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}