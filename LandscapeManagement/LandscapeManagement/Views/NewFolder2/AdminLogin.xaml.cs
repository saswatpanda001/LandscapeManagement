using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LandscapeManagement.Views;
using LandscapeManagement.Services;
using LandscapeManagement.Models;
using LandscapeManagement.Views.NewFolder1;



namespace LandscapeManagement.Views.NewFolder2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminLogin : ContentPage
    {
        private readonly UserService _userService = new UserService();

        public AdminLogin()
        {
            InitializeComponent();
        }
        private void TogglePasswordVisibility(object sender, EventArgs e)
        {
            PasswordEntry.IsPassword = !PasswordEntry.IsPassword;
        }


        private async void GoToForgotPassword(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PasswordReset());
        }

        



        private async void HandleAdminLogin(object sender, EventArgs e)
        {
            string email = EmailEntry.Text?.Trim();
            string password = PasswordEntry.Text?.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Error", "Please enter email and password", "OK");
                return;
            }

            // ✅ Use AdminLoginAsync instead of LoginAsync
            LandscapeManagement.Models.User loggedInAdmin = await _userService.AdminLoginAsync(email, password);

            if (loggedInAdmin != null)
            {
                SessionManager.SetUser(loggedInAdmin);
                await DisplayAlert("Success", "Admin Login Successful", "OK");
                await Navigation.PushAsync(new AdminDashboard()); // Redirect to Admin Dashboard
            }
            else
            {
                await DisplayAlert("Error", "Invalid email or password", "OK");
            }
        }
    }
}
