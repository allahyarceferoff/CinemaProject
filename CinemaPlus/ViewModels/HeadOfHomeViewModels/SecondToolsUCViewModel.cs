using CinemaPlus;
using CinemaPlus.Commands;
using CinemaPlus.Helpers;
using CinemaPlus.ViewModels.EndingViewModels;
using CinemaPlus.ViewModels.HomePageViewModels;
using CinemaPlus.Views.UserControls.HomePage;
using System;

namespace CinemaPlus.ViewModels
{
    public class SecondToolsUCViewModel : BaseViewModel
    {
        public RelayCommand HomeCommand { get; set; }
        public RelayCommand CampaignCommand { get; set; }
        public RelayCommand TariffsCommand { get; set; }
        public RelayCommand CineBonusCommand { get; set; }
        public RelayCommand FaqCommand { get; set; }

        public SecondToolsUCViewModel()
        {

            HomeCommand = new RelayCommand((h) => 
            {
                if (App.PreviousPages.Count != 0)
                {
                    App.PageWrapPanel.Children.RemoveAt(0);
                    App.PageWrapPanel.Children.Add(App.PreviousPages[0]);
                    App.PreviousPages.RemoveRange(1, App.PreviousPages.Count - 1);
                    var homePageView = App.PageWrapPanel.Children[0] as HomePageUC;
                    var homePageViewModel = homePageView.DataContext as HomePageUCViewModel;
                    homePageViewModel.TodayView.TodayUCScroll.ScrollToTop();
                    homePageViewModel.TodayCommand.Execute(null);
                    App.Web.Source = new Uri($"https://www.youtube.com");
                }
                App.IsInAdminSide = false;
                App.SideChangedCommands();
            });

            TariffsCommand = new RelayCommand((t) => 
            {
                App.PageWrapPanel.Children.Clear();
                App.Web.Source = new Uri($"https://www.youtube.com");
                App.IsInAdminSide = false;
                App.SideChangedCommands();
            });

            FaqCommand = new RelayCommand((f) =>
            {
                App.PageWrapPanel.Children.Clear();
                App.Web.Source = new Uri($"https://www.youtube.com");
                App.IsInAdminSide = false;
                App.SideChangedCommands();
            });
        }
    }
}

