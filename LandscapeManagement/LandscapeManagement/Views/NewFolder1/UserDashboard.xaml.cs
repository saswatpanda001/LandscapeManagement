using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LandscapeManagement.Models;
using LandscapeManagement.Views.NewFolder1;
using System;

namespace LandscapeManagement.Views.NewFolder1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserDashboard : ContentPage
    {
        public UserDashboard()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Display the logged-in user's name if available
            if (SessionManager.IsUserLoggedIn())
            {
                WelcomeLabel.Text = $"Welcome, {SessionManager.LoggedInUser.name}";
            }
            else
            {
                WelcomeLabel.Text = "Welcome, Guest";
            }
        }

        private async void GoToProfile(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserProfile());
        }

        private async void GoToBookingHistory(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BookingHistory());
        }

        private async void GoToService(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ShowServices());
        }

        private async void GoToServices(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ShowServices());
        }

    }
}
