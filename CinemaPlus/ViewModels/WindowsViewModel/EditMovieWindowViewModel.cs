using CinemaPlus.Commands;
using CinemaPlus.Helpers;
using CinemaPlus.Helpers.MovieCellUCHelpers;
using CinemaPlus.Models;
using CinemaPlus.ViewModels.AdminSideViewModels;
using CinemaPlus.ViewModels.EndingViewModels;
using CinemaPlus.Views.UserControls.AdminSide;
using CinemaPlus.Views.UserControls.EndOfPage;
using CinemaPlus.Views.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CinemaPlus.ViewModels.WindowsViewModel
{
    public class EditMovieWindowViewModel : BaseViewModel
    {
        public RelayCommand CloseCommand { get; set; }
        public RelayCommand SaveChangesCommand { get; set; }
        public RelayCommand ResetChangesCommand { get; set; }
        public RelayCommand DeleteMovieCommand { get; set; }
        public RelayCommand MainTabCheckedCommand { get; set; }
        public RelayCommand MainTabUncheckedCommand { get; set; }
        public RelayCommand PlotTabCheckedCommand { get; set; }
        public RelayCommand PlotTabUncheckedCommand { get; set; }
        public RelayCommand PosterTabCheckedCommand { get; set; }
        public RelayCommand PosterTabUncheckedCommand { get; set; }
        public RelayCommand SeatsTabCheckedCommand { get; set; }
        public RelayCommand SeatsTabUncheckedCommand { get; set; }
        public RelayCommand SessionsTabCheckedCommand { get; set; }
        public RelayCommand SessionsTabUncheckedCommand { get; set; }
        public RelayCommand MainTabCommand { get; set; }
        public RelayCommand PlotTabCommand { get; set; }
        public RelayCommand PosterTabCommand { get; set; }
        public RelayCommand SeatsTabCommand { get; set; }
        public RelayCommand SessionsTabCommand { get; set; }

        public Models.Movie SelectedMovie { get; set; }

        private string headline;
        public string Headline
        {
            get { return headline; }
            set { headline = value; OnPropertyChanged(); }
        }

        public EditMovieMainTabUC MainTabView { get; set; } = new EditMovieMainTabUC();
        public EditMovieMainTabUCViewModel MainTabViewModel { get; set; } = new EditMovieMainTabUCViewModel();

        public EditMoviePlotTabUC PlotTabView { get; set; } = new EditMoviePlotTabUC();
        public EditMoviePlotTabUCViewModel PlotTabViewModel { get; set; } = new EditMoviePlotTabUCViewModel();

        public EditMoviePosterTabUC PosterTabView { get; set; } = new EditMoviePosterTabUC();
        public EditMoviePosterTabUCViewModel PosterTabViewModel { get; set; } = new EditMoviePosterTabUCViewModel();

        public EditMovieSeatsTabUC SeatsTabView { get; set; } = new EditMovieSeatsTabUC();
        public EditMovieSeatsTabUCViewModel SeatsTabViewModel { get; set; } = new EditMovieSeatsTabUCViewModel();

        public EditMovieSessionsTabUC SessionsTabView { get; set; } = new EditMovieSessionsTabUC();
        public EditMovieSessionsTabUCViewModel SessionsTabViewModel { get; set; } = new EditMovieSessionsTabUCViewModel();

        public EditMovieWindowViewModel()
        {
            Headline = $"Edit • {App.SelectedMovieForEdit.Title}";

            MainTabView.DataContext = MainTabViewModel;

            PlotTabView.DataContext = PlotTabViewModel;

            PosterTabView.DataContext = PosterTabViewModel;

            SeatsTabView.DataContext = SeatsTabViewModel;
            SeatsTabViewModel.SeatsWrapPanel = SeatsTabView.SeatsWrapPanel;
            SeatsTabViewModel.UpdateBusySeatsOfMovieInDifferentHalls();
            SeatsTabViewModel.RefreshPlacesComboBox();
            App.SeatsTabViewModel = SeatsTabViewModel;

            SessionsTabView.DataContext = SessionsTabViewModel;
            App.SessionsTabViewModel = SessionsTabViewModel;

            CloseCommand = new RelayCommand((c) =>
            {
                App.Rectangle.Visibility = Visibility.Hidden;
                App.ChildWindow.Close();
                App.ChildWindow = null;
            });

            #region RadioButtonCommands
            //MainTabCheckedCommand = new RelayCommand((mainTabImage) =>
            //{
            //    ChangeImageSource(@"\Images\mainTabImageWhite.png", mainTabImage as Image);
            //});

            //MainTabUncheckedCommand = new RelayCommand((mainTabImage) =>
            //{
            //    ChangeImageSource(@"\Images\mainTabImage.png", mainTabImage as Image);
            //});

            //PlotTabCheckedCommand = new RelayCommand((plotTabImage) =>
            //{
            //    ChangeImageSource(@"\Images\plotTabImageWhite.png", plotTabImage as Image);
            //});

            //PlotTabUncheckedCommand = new RelayCommand((plotTabImage) =>
            //{
            //    ChangeImageSource(@"\Images\plotTabImage.png", plotTabImage as Image);
            //});

            //PosterTabCheckedCommand = new RelayCommand((posterTabImage) =>
            //{
            //    ChangeImageSource(@"\Images\posterTabImageWhite.png", posterTabImage as Image);
            //});

            //PosterTabUncheckedCommand = new RelayCommand((posterTabImage) =>
            //{
            //    ChangeImageSource(@"\Images\posterTabImage.png", posterTabImage as Image);
            //});

            //SeatsTabCheckedCommand = new RelayCommand((plotTabImage) =>
            //{
            //    ChangeImageSource(@"\Images\seatsTabImageWhite.png", plotTabImage as Image);
            //});

            //SeatsTabUncheckedCommand = new RelayCommand((plotTabImage) =>
            //{
            //    ChangeImageSource(@"\Images\seatsTabImage.png", plotTabImage as Image);
            //});

            //SessionsTabCheckedCommand = new RelayCommand((sessionsTabImage) =>
            //{
            //    ChangeImageSource(@"\Images\sessionsTabImageWhite.png", sessionsTabImage as Image);
            //});

            //SessionsTabUncheckedCommand = new RelayCommand((sessionsTabImage) =>
            //{
            //    ChangeImageSource(@"\Images\sessionsTabImage.png", sessionsTabImage as Image);
            //});
            #endregion

            MainTabCommand = new RelayCommand((m) =>
            {
                App.EditMoviePageStackPanel.Children.RemoveAt(0);
                App.EditMoviePageStackPanel.Children.Add(MainTabView);
            });

            PlotTabCommand = new RelayCommand((p) =>
            {
                App.EditMoviePageStackPanel.Children.RemoveAt(0);
                App.EditMoviePageStackPanel.Children.Add(PlotTabView);
            });

            PosterTabCommand = new RelayCommand((p) =>
            {
                App.EditMoviePageStackPanel.Children.RemoveAt(0);
                App.EditMoviePageStackPanel.Children.Add(PosterTabView);
            });

            SeatsTabCommand = new RelayCommand((p) =>
            {
                App.EditMoviePageStackPanel.Children.RemoveAt(0);
                if (SessionsTabViewModel.Sessions.Count == 0)
                {
                    var noResultUC = new NoResultUC();
                    var noResultViewModel = new NoResultUCViewModel("There is no session yet . . . ");
                    noResultUC.DataContext = noResultViewModel;
                    noResultUC.Width = 984;
                    App.EditMoviePageStackPanel.Children.Add(noResultUC);
                    return;
                }
                App.EditMoviePageStackPanel.Children.Add(SeatsTabView);
            });

            SessionsTabCommand = new RelayCommand((s) =>
            {
                App.EditMoviePageStackPanel.Children.RemoveAt(0);
                App.EditMoviePageStackPanel.Children.Add(SessionsTabView);
            });

            ResetChangesCommand = new RelayCommand((r) =>
            {
                if (PlotTabViewModel.Plot != App.SelectedMovieForEdit.Plot)
                {
                    App.HasChanges = true;
                }

                if (App.HasChanges)
                {
                    MainTabViewModel.UpdateProperties();
                    PlotTabViewModel.Plot = App.SelectedMovieForEdit.Plot;
                    PosterTabViewModel.ImageSource = Helper.StringToImageSource($@"{App.SelectedMovieForEdit.Poster}");
                    SeatsTabViewModel.HallsMovieExists = Helper.GetHallsMovieExists(App.SelectedMovieForEdit.Title);
                    SeatsTabViewModel.PlacesMovieExists.Clear();
                    foreach (var dH in SeatsTabViewModel.HallsMovieExists)
                    {
                        SeatsTabViewModel.PlacesMovieExists.Add($"{dH.Cinema}, {dH.Hallname}");
                    }
                    if (SeatsTabViewModel.HallsMovieExists.Count > 0)
                    {
                        var detailedHall = SeatsTabViewModel.HallsMovieExists[0];
                        SeatsTabViewModel.SelectedItem = $"{detailedHall.Cinema}, {detailedHall.Hallname}, {detailedHall.Date}";
                    }
                    SeatsTabViewModel.UpdateBusySeatsOfMovieInDifferentHalls();
                    SeatsTabViewModel.RefreshPlacesComboBox();
                    SessionsTabViewModel.InitializeSessions();
                    SessionsTabViewModel.UpdateSessions();
                    if (SessionsTabViewModel.Sessions.Count == 0)
                    {
                        var noResultUC = new NoResultUC();
                        var noResultViewModel = new NoResultUCViewModel("There is no session yet . . . ");
                        noResultUC.DataContext = noResultViewModel;
                        noResultUC.Width = 984;
                        App.EditMoviePageStackPanel.Children.Add(noResultUC);
                    }
                    else
                    {
                        SeatsTabViewModel.RefreshPlacesComboBox();
                    }
                    string caption2 = "Warning";
                    string message2 = "All changes were reset successfully";
                    MessageBoxButton button2 = MessageBoxButton.OK;
                    MessageBoxImage icon2 = MessageBoxImage.Warning;
                    MessageBox.Show(message2, caption2, button2, icon2);
                    App.HasChanges = false;
                }
                else
                {
                    string caption2 = "Warning";
                    string message2 = "There is no change to reset!!!";
                    MessageBoxButton button2 = MessageBoxButton.OK;
                    MessageBoxImage icon2 = MessageBoxImage.Warning;
                    MessageBox.Show(message2, caption2, button2, icon2);
                }
            });

            SaveChangesCommand = new RelayCommand((s) =>
            {
                if (PlotTabViewModel.Plot != App.SelectedMovieForEdit.Plot)
                {
                    App.HasChanges = true;
                }

                if (App.HasChanges)
                {
                    Helper.RemoveMoviesFromFiles(App.SelectedMovieForEdit.Title);

                    List<string> hallsDone = new List<string>();
                    int count = SessionsTabViewModel.Sessions.Count;
                    List<Models.Movie> defaultMovies = JsonSerialization<Models.Movie>.Deserialize(@"~/../../../Files/Defaults\defaultMovies.json");
                    bool didOneTime = false;
                    for (int x = 0; x < count; x++)
                    {
                        #region Movie Initializing
                        var _changed_movie = new Models.Movie()
                        {
                            Id = App.SelectedMovieForEdit.Id,
                            Title = MainTabViewModel.Title,
                            Year = MainTabViewModel.Year,
                            Released = MainTabViewModel.Released,
                            Runtime = MainTabViewModel.Runtime + " min",
                            Genre = MainTabViewModel.Genre,
                            Director = MainTabViewModel.Director,
                            Writer = MainTabViewModel.Writer,
                            Actors = MainTabViewModel.Actors,
                            Plot = PlotTabViewModel.Plot,
                            Awards = MainTabViewModel.Awards,
                            //Poster = "~" + PosterTabViewModel.ImageSource.ToString(),
                            //Poster = App.SelectedMovieForEdit.Poster,
                            ImdbRating = MainTabViewModel.ImdbRating,
                            Subtitle = MainTabViewModel.SelectedSubtitle,
                        };

                        if (!string.IsNullOrEmpty(MainTabViewModel.Languages[MainTabViewModel.LanguagesCBSelectedIndex].Trim()))
                            _changed_movie.Language = MainTabViewModel.Languages[MainTabViewModel.LanguagesCBSelectedIndex];
                        else
                            _changed_movie.Language = "N/A";

                        if (!string.IsNullOrEmpty(MainTabViewModel.Countries[MainTabViewModel.CountriesCBSelectedIndex].Trim()))
                            _changed_movie.Country = MainTabViewModel.Countries[MainTabViewModel.CountriesCBSelectedIndex];
                        else
                            _changed_movie.Country = "N/A";

                        #endregion

                        #region Price
                        _changed_movie.Price = SessionsTabViewModel.Sessions[x].Price.Replace("₼", " ").Trim();
                        #endregion

                        #region Poster
                        string source;
                        source = PosterTabViewModel.ImageSource.ToString();
                        //if (source.Contains("Images/MoviePosters"))
                        //{
                        //    source = source.Replace("")
                        //}
                        //try
                        //{
                        //    source = new BitmapImage(new Uri(PosterTabViewModel.ImageSource.ToString())).ToString();
                        //}
                        //catch (Exception)
                        //{
                        //    source = $@"..\..\Images\noImage.jpg";
                        //}
                        _changed_movie.Poster = source;

                        #endregion

                        #region Seats
                        //int indexOfHallInBusySeatsList = -1;
                        //int length = SeatsTabViewModel.HallsMovieExists.Count;
                        //for (int z = 0; z < length; z++)
                        //{
                        //    if (SeatsTabViewModel.HallsMovieExists[z].Hallname == hall_name)
                        //    {
                        //        indexOfHallInBusySeatsList = z;
                        //        break;
                        //    }
                        //}
                        _changed_movie.BusySeats = SeatsTabViewModel.BusySeatsOfMovieInDifferentHalls[x];
                        #endregion

                        var _session = SessionsTabViewModel.Sessions[x];
                        string filename = @"~/../../../Files/Halls\" + _session.Date.Replace(" ", string.Empty) + ".json";

                        #region Session

                        _changed_movie.Session = _session;
                        List<Models.Movie> hall_movies = new List<Models.Movie>();
                        if (File.Exists(filename))
                        {
                            hall_movies = JsonSerialization<Models.Movie>.Deserialize(filename);
                        }
                        else
                        {
                            var _cinema = App.Cinemas.Find((c) => c.Name == _session.Cinema);
                            var hall = new Hall()
                            {
                                HallName = _session.Hall
                            };
                            JsonSerialization<Cinema>.Serialize(App.Cinemas, @"~/../../../Files/Defaults\defaultCinemas.json");
                        }


                        hall_movies.Add(_changed_movie);
                        JsonSerialization<Models.Movie>.Serialize(hall_movies, filename);
                        #endregion

                        #region OneTime
                        if (!didOneTime)
                        {
                            int i = defaultMovies.FindIndex((m) => m.Title == App.SelectedMovieForEdit.Title);
                            defaultMovies[i] = _changed_movie;
                            App.DefaultMovies = defaultMovies;
                            JsonSerialization<Models.Movie>.Serialize(defaultMovies, @"~/../../../Files/Defaults\defaultMovies.json");

                            //int i2 = App.Movies.FindIndex((m) => m.Title == App.SelectedMovieForEdit.Title);
                            //App.Movies[i2] = _changed_movie;
                            didOneTime = true;

                            // +    
                        }
                        #endregion

                    }
                    App.ChildWindow.Close();
                    App.Rectangle.Visibility = Visibility.Hidden;

                    string caption2 = "Warning";
                    string message2 = "Changes were successfully saved!";
                    MessageBoxButton button2 = MessageBoxButton.OK;
                    MessageBoxImage icon2 = MessageBoxImage.Warning;
                    MessageBox.Show(message2, caption2, button2, icon2);

                    ((App.PageWrapPanel.Children[0] as AdminHomePageUC).DataContext as AdminHomePageUCViewModel).EditMovieTabViewModel.FilterMovies();
                    App.IsInAdminSide = !App.IsInAdminSide;
                    Helper.InitializeCinemas();
                    App.HomePageViewModel.ScheduleViewModel.FilterAllSchedules();
                    App.HomePageViewModel.SearchViewModel.AddRandomMoviesToSearch();
                    MovieCellUCHelper.AddMoviesToView(App.HomePageViewModel.TodayView);
                    App.IsInAdminSide = !App.IsInAdminSide;
                }
                else
                {
                    string caption2 = "Warning";
                    string message2 = "Not found change to save!";
                    MessageBoxButton button2 = MessageBoxButton.OK;
                    MessageBoxImage icon2 = MessageBoxImage.Warning;
                    MessageBox.Show(message2, caption2, button2, icon2);
                }
            });

            DeleteMovieCommand = new RelayCommand((d) =>
            {
                var areYourSureWindow = new AreYouSureWindow();
                var areYouSureWindowViewModel = new AreYouSureWindowViewModel();
                areYourSureWindow.DataContext = areYouSureWindowViewModel;

                areYourSureWindow.Owner = App.ChildWindow;
                areYourSureWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
                App.ChildWindow2 = areYourSureWindow;
                App.ChildWindowRectangle.Visibility = Visibility.Visible;
                areYourSureWindow.ShowDialog();
                var delete = App.ChildWindow2.DialogResult;
                App.ChildWindow2 = null;

                if (delete is true)
                {
                    CloseCommand.Execute(null);
                    App.Movies.RemoveAll((m) => m.Title == App.SelectedMovieForEdit.Title);
                    App.DefaultMovies.RemoveAll((m) => m.Title == App.SelectedMovieForEdit.Title);
                    {
                        List<Models.Movie> defaultMovies = JsonSerialization<Models.Movie>.Deserialize(@"~/../../../Files/Defaults\defaultMovies.json");
                        int index = defaultMovies.FindIndex((m) => m.Title == App.SelectedMovieForEdit.Title);
                        defaultMovies.RemoveAt(index);
                        JsonSerialization<Models.Movie>.Serialize(defaultMovies, @"~/../../../Files/Defaults\defaultMovies.json");
                    }

                    foreach (var cinema in App.Cinemas)
                    {
                        foreach (var hall in cinema.Halls)
                        {
                            foreach (var m in hall.HallMovies)
                            {
                                if (m.Title == App.SelectedMovieForEdit.Title)
                                {
                                    string filename = @"~/../../../Files/Halls\" + cinema.Name.Replace(" ", string.Empty) + "+" + hall.HallName.Replace(" ", string.Empty) + ".json";
                                    var movies = JsonSerialization<Models.Movie>.Deserialize(filename);
                                    int count = movies.RemoveAll((_movie) => _movie.Title == App.SelectedMovieForEdit.Title);
                                    if (count > 0)
                                        JsonSerialization<Models.Movie>.Serialize(movies, filename);
                                }
                            }
                        }
                    }

                    ((App.PageWrapPanel.Children[0] as AdminHomePageUC).DataContext as AdminHomePageUCViewModel).EditMovieTabViewModel.FilterMovies();
                    App.IsInAdminSide = !App.IsInAdminSide;
                    Helper.InitializeCinemas();
                    MovieCellUCHelper.AddMoviesToView(App.HomePageViewModel.TodayView);
                    App.HomePageViewModel.ScheduleViewModel.FilterAllSchedules();
                    App.HomePageViewModel.SearchViewModel.AddRandomMoviesToSearch();
                    App.IsInAdminSide = !App.IsInAdminSide;

                    string caption2 = "Warning";
                    string message2 = "Movie deleted successfully!";
                    MessageBoxButton button2 = MessageBoxButton.OK;
                    MessageBoxImage icon2 = MessageBoxImage.Warning;
                    MessageBox.Show(message2, caption2, button2, icon2);
                }
            });
        }
    }
}
