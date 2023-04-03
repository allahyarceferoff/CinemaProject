using CinemaPlus.Commands;
using CinemaPlus.Helpers;
using CinemaPlus.Models;
using CinemaPlus.Views.UserControls.AdminSide;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CinemaPlus.ViewModels.AdminSideViewModels
{
    public class AddMovieSessionUCViewModel : BaseViewModel
    {
        public RelayCommand BackCommand { get; set; }
        public RelayCommand DateChangedCommand { get; set; }
        public RelayCommand TimeChangedCommand { get; set; }
        public RelayCommand PriceChangedCommand { get; set; }
        public RelayCommand AddSessionCommand { get; set; }

        public List<string> AllDates { get; set; } = new List<string>();
        public List<string> AllTimes { get; set; } = new List<string>();

        private string price = string.Empty;

        public string Price
        {
            get { return price; }
            set { price = value; OnPropertyChanged(); PriceChangedCommand.Execute(null); }
        }

        public MovieSessionUC MovieSessionView { get; set; } = new MovieSessionUC();
        public MovieSessionUCViewModel MovieSessionViewModel { get; set; } = new MovieSessionUCViewModel();

        EditMovieSessionsTabUCViewModel SessionsTabViewModel { get; set; }

        public AddMovieSessionUCViewModel(EditMovieSessionsTabUCViewModel _SessionsTabViewModel)
        {
            SessionsTabViewModel = _SessionsTabViewModel;
            MovieSessionViewModel.Date = "Select Date";
            MovieSessionViewModel.Time = "Select Time";
            MovieSessionViewModel.Price = "Type Price";

            AllDates = Helper.GetDates();
            AllTimes = Helper.GetAllTimes();

            BackCommand = new RelayCommand((b) =>
            {
                App.EditMoviePageStackPanel.Children.RemoveAt(0);
                App.EditMoviePageStackPanel.Children.Add(App.AdminSidePreviousPage);
                SessionsTabViewModel.UpdateSessions();
                App.AdminSidePreviousPage = null;
            });

            DateChangedCommand = new RelayCommand((selectedDate) =>
            {
                MovieSessionViewModel.Date = selectedDate.ToString();
            });

            TimeChangedCommand = new RelayCommand((selectedTime) =>
            {
                MovieSessionViewModel.Time = selectedTime.ToString();
            });

            PriceChangedCommand = new RelayCommand((p) =>
            {
                if (Price.StartsWith("0"))
                {
                    do
                    {
                        Price = Price.Remove(0, 1);
                    } while (Price.StartsWith("0"));
                }

                MovieSessionViewModel.Price = Price.Trim() + ".00 ₼";
            });

            AddSessionCommand = new RelayCommand((a) =>
            {

            
            var stringPrice = MovieSessionViewModel.Price.Replace("₼", String.Empty).Trim();
            var _price = double.Parse(stringPrice);

            var session = new Session()
            {

                Date = MovieSessionViewModel.Date.Replace("/", "."),
                Time = DateTime.Parse(MovieSessionViewModel.Time).ToLongTimeString().Replace(":00 ", " ").Trim(),
                Price = _price.ToString() + ".00 ₼",
            };

            var date = DateTime.Parse(session.Date + " " + session.Time);
            var detailedHall = new DetailedHall()
            {
                BusySeats = new List<int>(),
                Cinema = session.Cinema,
                Date = date.ToShortDateString().Replace("/", ".") + ", " + date.ToLongTimeString().Replace(":00 ", " ").Trim(),
                Hallname = session.Hall
            };

            App.BusySeatsOfMovieInDifferentHalls.Add(new List<int>());
            SessionsTabViewModel.Sessions.Add(session);
            App.EditMovieWindowViewModel.SeatsTabViewModel.HallsMovieExists.Add(detailedHall);
            App.SeatsTabViewModel.PlacesMovieExists.Add($"{detailedHall.Cinema}, {detailedHall.Hallname}");
            App.SeatsTabViewModel.RefreshPlacesComboBox();
            App.SeatsTabViewModel.HallChangedCommand.Execute(App.SeatsTabViewModel.SelectedIndex);
            if (App.EditMovieWindowViewModel.SeatsTabViewModel.PlacesMovieExists.Count == 1)
            {
                App.EditMovieWindowViewModel.SeatsTabViewModel.SelectedIndex = 0;
            }
            App.HasChanges = true;
            BackCommand.Execute(null);
        });
        }

    }
}
