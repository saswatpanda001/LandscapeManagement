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
            NavigationPage.SetHasBackButton(this, false);
        }

        private async void OnHomeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AboutPage());
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

            // Password length validation
            if (password.Length < 6)
            {
                await DisplayAlert("Error", "Password must be at least 6 characters long.", "OK");
                return;
            }

            try
            {
                // Call the AdminLoginAsync service
                LandscapeManagement.Models.User loggedInAdmin = await _userService.AdminLoginAsync(email, password);

                if (loggedInAdmin != null)
                {
                    // Optional: Check if user has admin privileges
                    if (loggedInAdmin.role != "Admin")
                    {
                        await DisplayAlert("Error", "Access Denied. You are not authorized as an Admin.", "OK");
                        return;
                    }

                    SessionManager.SetUser(loggedInAdmin);
                    await DisplayAlert("Success", "Admin Login Successful", "OK");
                    await Navigation.PushAsync(new AdminDashboard());
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
