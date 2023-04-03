using CinemaPlus.Views.Main;
using CinemaPlus.Models;
using System.Windows.Controls;
using CinemaPlus.Commands;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.Generic;

namespace CinemaPlus.ViewModels.Main
{
    public class SelectSeatsWindowViewModel : BaseViewModel
    {
        public RelayCommand CloseCommand { get; set; }
        public RelayCommand SeatSelectedCommand { get; set; }
        public RelayCommand ConfirmCommand { get; set; }

        public WrapPanel SeatsWP { get; set; } = new WrapPanel();
        public Button ConfirmButton { get; set; }

        public double PriceOfTicket { get; set; }
        private string movieName;

        public string MovieName
        {
            get { return movieName; }
            set { movieName = value; OnPropertyChanged(); }
        }

        private string session;

        public string Session
        {
            get { return session; }
            set { session = value; OnPropertyChanged(); }
        }

        private string cinemaAndHall;

        public string CinemaAndHall
        {
            get { return cinemaAndHall; }
            set { cinemaAndHall = value; OnPropertyChanged(); }
        }

        private string total;

        public string Total
        {
            get { return total; }
            set { total = value; OnPropertyChanged(); }
        }

        public int SeatCount { get; set; } = 0;

        public SelectSeatsWindowViewModel()
        {
            Total = (PriceOfTicket * SeatCount).ToString() + " AZN";
            CloseCommand = new RelayCommand((c) =>
            {
                App.ChildWindow.Close();
                App.ChildWindow = null;
                App.Rectangle.Visibility = Visibility.Hidden;
                DisableConfirmButton();
            });

            SeatSelectedCommand = new RelayCommand((tb) =>
            {
                var toggleButton = tb as ToggleButton;

                if (toggleButton.IsChecked ?? true)
                {
                    if (SeatCount > 4)
                    {
                        toggleButton.IsChecked = false;
                        string caption = "Warning";
                        string message = "You can't select more than 5 places";
                        MessageBoxButton button = MessageBoxButton.OK;
                        MessageBoxImage icon = MessageBoxImage.Warning;
                        MessageBox.Show(message, caption, button, icon);
                    }
                    else
                    {
                        SeatCount++;
                    }
                }
                else
                {
                    SeatCount--;
                }

                if (SeatCount > 0)
                {
                    ConfirmButton.Background = App.ColorsDictionary["thirdColor"] as Brush;
                    ConfirmButton.Cursor = Cursors.Hand;
                    ConfirmButton.IsEnabled = true;
                    Total = ((int)(PriceOfTicket / 100 * SeatCount)).ToString() + ".00 AZN";
                }
                else
                {
                    DisableConfirmButton();
                }
            });

            ConfirmCommand = new RelayCommand((c) =>
            {
                DisableConfirmButton();
                App.ChildWindow.Close();
                var orderView = new OrderWindow();
                var orderViewModel = new OrderWindowViewModel();
                orderView.DataContext = orderViewModel;
                orderViewModel.EmailWarning = orderView.EmailWarning;
                orderViewModel.PhoneWarning = orderView.PhoneWarning;
                orderViewModel.DigitsOfCardWarning = orderView.DigitsOfCardWarning;
                App.ChildWindow = orderView;
                orderView.Owner = App.Current.MainWindow;
                orderView.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
                App.SelectedSeats = new List<Seat>();
                int row;
                int seat;
                for (int x = 0; x < SeatsWP.Children.Count; x++)
                {
                    var tb = SeatsWP.Children[x] as ToggleButton;
                    if (tb.IsChecked is true)
                    {
                        decimal no = x / 13M;
                        if ((int)no == no)
                        {
                            row = (int)no;
                        }
                        else
                        {
                            row = (int)no + 1;
                        }
                        if (x % 13 == 0)
                            row += 1;
                        seat = (x % 13) + 1;
                        App.SelectedSeats.Add(new Seat { SeatNo = seat, RowNo = row });
                    }
                }
                orderView.ShowDialog();
            });
        }

        public void DisableConfirmButton()
        {
            ConfirmButton.Background = App.ColorsDictionary["fourthColor"] as Brush;
            ConfirmButton.Cursor = Cursors.None;
            ConfirmButton.IsEnabled = false;
            Total = "0 AZN";
        }
    }
}