using CinemaPlus.Commands;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CinemaPlus.Views.UserControls.AdminSide;

namespace CinemaPlus.ViewModels.AdminSideViewModels
{
    public class AdminSignInUCViewModel : BaseViewModel
    {
        public RelayCommand SignInCommand { get; set; }
        public string UserName { get; set; } = string.Empty;
        public PasswordBox PasswordBox { get; internal set; }
        public TextBlock UsernameWarningTB { get; set; }
        public TextBlock PasswordWarningTB { get; set; }

        public AdminSignInUCViewModel()
        {
            SignInCommand = new RelayCommand((s) =>
            {
                bool _return = false;
                if (UserName.Trim() == String.Empty)
                {
                    UsernameWarningTB.Foreground = Brushes.Red;
                    _return = true;
                }
                else
                {
                    UsernameWarningTB.Foreground = Brushes.Transparent;
                }

                if (PasswordBox.Password.Trim() == String.Empty)
                {
                    PasswordWarningTB.Foreground = Brushes.Red;
                    _return = true;
                }
                else
                {
                    PasswordWarningTB.Foreground = Brushes.Transparent;
                }

                if (_return)
                    return;

                foreach (var admin in App.Admins)
                {
                    if (admin.Username == UserName)
                    {
                        if (admin.Password == PasswordBox.Password)
                        {
                            App.IsInAdminSide = true;
                            var adminHomePageView = new AdminHomePageUC();
                            var adminHomePageViewModel = new AdminHomePageUCViewModel();
                            adminHomePageView.DataContext = adminHomePageViewModel;
                            App.CurrentAdmin = admin;
                            App.AdminPage = adminHomePageView.AdminPage;
                            App.AdminPage.Children.Add(adminHomePageViewModel.EditMovieTabView);
                            App.AdminHomePage = adminHomePageView;
                            App.PageWrapPanel.Children.RemoveAt(0);
                            App.PageWrapPanel.Children.Add(adminHomePageView);
                            App.SideChangedCommands();
                            return;
                        }
                    }
                }
                string caption = "Warning";
                string message = "Admin not found";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;

                MessageBox.Show(message, caption, button, icon);

            });
        }
    }
}
