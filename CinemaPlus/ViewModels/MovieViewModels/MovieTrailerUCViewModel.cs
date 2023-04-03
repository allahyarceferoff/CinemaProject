using Microsoft.Web.WebView2.Wpf;
using System;

namespace CinemaPlus.ViewModels.MovieViewModels
{
    public class MovieTrailerUCViewModel : BaseViewModel
    {
        public WebView2 Web { get; set; } = new WebView2();
        public void Navigate(string video)
        {
            try
            {
                Web.NavigateToString(video);
            }
            catch (Exception)
            {

            }
        }
    }
}
