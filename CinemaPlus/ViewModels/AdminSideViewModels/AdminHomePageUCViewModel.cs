using CinemaPlus.Commands;
using CinemaPlus.Helpers;
using CinemaPlus.ViewModels.Movie;
using CinemaPlus.ViewModels.MovieViewModels;
using CinemaPlus.Views.UserControls;
using CinemaPlus.Views.UserControls.AdminSide;
using CinemaPlus.Views.UserControls.EndOfPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaPlus.ViewModels.AdminSideViewModels
{
    public class AdminHomePageUCViewModel : BaseViewModel
    {
        public RelayCommand EditMovieCommand { get; set; }
        public RelayCommand AddMovieCommand { get; set; }
        public RelayCommand ViewPurchasedTicketsCommand { get; set; }

        public EditMovieTabUC EditMovieTabView { get; set; } = new EditMovieTabUC();
        public EditMovieTabUCViewModel EditMovieTabViewModel { get; set; } = new EditMovieTabUCViewModel();
        public AddMovieTabUC AddMovieTabView { get; set; } = new AddMovieTabUC();
        public AddMovieTabUCViewModel AddMovieTabViewModel { get; set; } = new AddMovieTabUCViewModel();
        public PurchasedTicketsUC PurchasedTicketsView { get; set; } = new PurchasedTicketsUC();
        public PurchasedTicketsUCViewModel PurchasedTicketsViewModel { get; set; } = new PurchasedTicketsUCViewModel();

        public AdminHomePageUCViewModel()
        {
            PurchasedTicketsView.DataContext = PurchasedTicketsViewModel;
            PurchasedTicketsViewModel.PurchasedTicketsWrapPanel = PurchasedTicketsView.PurchasedTicketsWrapPanel;
            PurchasedTicketsViewModel.CreateCellsForPurchasedTickets();

            EditMovieTabView.DataContext = EditMovieTabViewModel;
            EditMovieTabViewModel.MoviesForEditWrapPanel = EditMovieTabView.MoviesForEditWrapPanel;
            EditMovieTabViewModel.EditMoviesTabScroll = EditMovieTabView.EditMoviesTabScroll;
            EditMovieTabViewModel.EnglishTB = EditMovieTabView.EnglishTB;
            EditMovieTabViewModel.FilterMovies();
            EditMovieTabViewModel.EditMoviesTabScroll.ScrollToTop();

            AddMovieTabView.DataContext = AddMovieTabViewModel;
            AddMovieTabViewModel.SearchTb = AddMovieTabView.SearchTB;
            AddMovieTabViewModel.SearchUCScrollViewer = AddMovieTabView.SearchUCScroll;
            AddMovieTabViewModel.SearchedMoviesWrapPanelAdminSide = AddMovieTabView.SearchedMoviesWrapPanelAdminSide;
            AddMovieTabViewModel.AddRandomMoviesToSearch();

            App.AdminSideEditSide = true;
            App.AdminSideAddSide = false;

            EditMovieCommand = new RelayCommand((e) =>
            {
                App.AdminSideEditSide = true;
                App.AdminSideAddSide = false;
                App.AdminPage.Children.RemoveAt(0);
                App.AdminPage.Children.Add(EditMovieTabView);
            });

            AddMovieCommand = new RelayCommand((a) =>
            {
                App.AdminSideEditSide = false;
                App.AdminSideAddSide = true;
                App.AdminPage.Children.RemoveAt(0);
                App.AdminPage.Children.Add(AddMovieTabView);
                AddMovieTabView.SearchUCScroll.ScrollToTop();
            });
            ViewPurchasedTicketsCommand = new RelayCommand((v) =>
            {
                if (App.PageWrapPanel.Children.Count > 0)
                    App.PageWrapPanel.Children.RemoveAt(0);
                App.PageWrapPanel.Children.Add(PurchasedTicketsView);
            });

        }
    }
}
