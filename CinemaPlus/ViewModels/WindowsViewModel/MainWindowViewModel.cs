using System;
using System.IO;
using System.Reflection;
using CinemaPlus;
using System.Windows;
using CinemaPlus.Helpers;
using CinemaPlus.Helpers.MovieCellUCHelpers;
using CinemaPlus.ViewModels;
using CinemaPlus.ViewModels.EndingViewModels;
using CinemaPlus.ViewModels.HomePageViewModels;
using CinemaPlus.ViewModels.TabsViewModels;
using CinemaPlus.Views.UserControls;
using CinemaPlus.Views.UserControls.EndOfPage;
using CinemaPlus.Views.UserControls.Home;
using CinemaPlus.Views.UserControls.HomePage;
using CinemaPlus.Views.UserControls.MovieUC;
using CinemaPlus.ViewModels.Movie;
using CinemaPlus.ViewModels.MovieViewModels;
using CinemaPlus.Models;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using CinemaPlus.ViewModels.Main;
using CinemaPlus.Views.Main;
using CinemaPlus.ViewModels.WindowsViewModel;
using System.Collections.Generic;

namespace CinemaPlus.ViewModels.WindowsViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel()
        {
            Helper.InitializeCinemas();


            var logoView = new LogoUC();
            var logoViewModel = new LogoUCViewModel();
            App.LogoUCViewModel = logoViewModel;
            logoView.DataContext = logoViewModel;

            var firstToolsView = new FirstToolsUC();
            var firstToolsViewModel = new FirstToolsUCViewModel();
            App.FirstToolsViewModel = firstToolsViewModel;
            firstToolsView.DataContext = firstToolsViewModel;

            var secondToolsView = new SecondToolsUC();
            var secondToolsViewModel = new SecondToolsUCViewModel();
            App.SecondToolsViewModel = secondToolsViewModel;
            secondToolsView.DataContext = secondToolsViewModel;

            var homePageView = new HomePageUC();
            var homePageViewModel = new HomePageUCViewModel();
            homePageView.DataContext = homePageViewModel;
            App.HomePage = homePageView.Page;
            MovieCellUCHelper.AddMoviesToView(homePageViewModel.TodayView);

            App.HomePageViewModel = homePageViewModel;
            App.HomePage.Children.Add(homePageViewModel.TodayView);
            App.LogoGrid.Children.Add(logoView);
            App.PageWrapPanel.Children.Add(homePageView);
            App.PreviousPages.Add(App.PageWrapPanel.Children[0]);
        }
    }
}
