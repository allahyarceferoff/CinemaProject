using CinemaPlus.Commands;
using CinemaPlus.Helpers;
using CinemaPlus.Models;
using CinemaPlus.ViewModels.EndingViewModels;
using CinemaPlus.Views.UserControls.AdminSide;
using CinemaPlus.Views.UserControls.EndOfPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CinemaPlus.ViewModels.AdminSideViewModels
{
    public class PurchasedTicketsUCViewModel : BaseViewModel
    {
        public RelayCommand AdminHomeCommand { get; set; }
        public RelayCommand IsCheckedRadioButton { get; set; }
        public WrapPanel PurchasedTicketsWrapPanel { get; set; }

        public void CreateCellsForPurchasedTickets()
        {
            var allPurchasedTickets = JsonSerialization<Ticket>.Deserialize(@"~/../../../Files\purchasedTickets.json");
            PurchasedTicketsWrapPanel.Children.Clear();
            if (allPurchasedTickets.Count == 0)
            {
                var noResultUC = new NoResultUC();
                var noResultViewModel = new NoResultUCViewModel("There is no purchased ticket yet . . . ");
                noResultUC.DataContext = noResultViewModel;
                PurchasedTicketsWrapPanel.Children.Add(noResultUC);
                return;
            }
            foreach (var ticket in allPurchasedTickets)
            {
                var purchasedTicketView = new PurchasedTicketCellUC();
                var purchasedTicketViewModel = new PurchasedTicketCellUCViewModel(ticket);
                purchasedTicketView.DataContext = purchasedTicketViewModel;
                PurchasedTicketsWrapPanel.Children.Add(purchasedTicketView);
            }
        }

        public PurchasedTicketsUCViewModel()
        {
            AdminHomeCommand = new RelayCommand((a) =>
            {
                if (App.PageWrapPanel.Children.Count > 0)
                {
                    App.PageWrapPanel.Children.RemoveAt(0);
                    App.PageWrapPanel.Children.Add(App.AdminHomePage);
                }
            });
        }
    }
}