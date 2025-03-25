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
            NavigationPage.SetHasBackButton(this, false);
        }

        private async void OnHomeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AboutPage());
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

            // Validate email and password fields
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Error", "Please enter both email and password.", "OK");
                return;
            }

            // Email format validation
            if (!IsValidEmail(email))
            {
                await DisplayAlert("Error", "Please enter a valid email address.", "OK");
                return;
            }

            // Password length check
            if (password.Length < 6)
            {
                await DisplayAlert("Error", "Password must be at least 6 characters long.", "OK");
                return;
            }

            try
            {
                // Attempt login
                User loggedInUser = await _userService.LoginAsync(email, password);

                if (loggedInUser != null)
                {
                    SessionManager.SetUser(loggedInUser);
                    await DisplayAlert("Success", "Login Successful", "OK");
                    await Navigation.PushAsync(new UserDashboard());
                }
                else
                {
                    await DisplayAlert("Error", "Invalid email or password.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        // Email validation function using regex
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }






    }
}

