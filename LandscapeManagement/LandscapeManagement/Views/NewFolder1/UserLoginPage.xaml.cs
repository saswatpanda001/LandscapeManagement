using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LandscapeManagement.Models;
using LandscapeManagement.Services;

namespace LandscapeManagement.Views.NewFolder1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserLoginPage : ContentPage
    {
        private readonly UserService _userService = new UserService();

        public UserLoginPage()
        {
            InitializeComponent();
        }

        private async void OnSignUpClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Signup());
        }

        private async void OnForgotPasswordClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PasswordReset());
        }



        private async void HandleLogin(object sender, EventArgs e)
        {
            string email = emailEntry.Text?.Trim();
            string password = passwordEntry.Text?.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Error", "Please enter email and password", "OK");
                return;
            }

            User loggedInUser = await _userService.LoginAsync(email, password);

            if (loggedInUser != null)
            {
                SessionManager.SetUser(loggedInUser);
                await DisplayAlert("Success", "Login Successful", "OK");
                await Navigation.PushAsync(new UserDashboard());
            }
            else
            {
                await DisplayAlert("Error", "Invalid email or password", "OK");
            }
        }





    }
}

