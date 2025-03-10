using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using LandscapeManagement.Services;

namespace LandscapeManagement.Views.NewFolder1
{
    public partial class PasswordReset : ContentPage
    {
        private readonly UserService _userService;

        public PasswordReset()
        {
            InitializeComponent();
            _userService = new UserService();
        }

        private async void OnResetPasswordClicked(object sender, EventArgs e)
        {
            string email = emailEntry.Text;
            string newPassword = newPasswordEntry.Text;
            string confirmPassword = confirmPasswordEntry.Text;

            // Validations
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                errorLabel.Text = "All fields are required.";
                errorLabel.IsVisible = true;
                return;
            }

            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                errorLabel.Text = "Invalid email format.";
                errorLabel.IsVisible = true;
                return;
            }

            if (newPassword.Length < 6)
            {
                errorLabel.Text = "Password must be at least 6 characters.";
                errorLabel.IsVisible = true;
                return;
            }

            if (newPassword != confirmPassword)
            {
                errorLabel.Text = "Passwords do not match.";
                errorLabel.IsVisible = true;
                return;
            }

            // Call API to reset password
            var response = await _userService.ResetPasswordAsync(email, newPassword);

            if (response)
            {
                await DisplayAlert("Success", "Password reset successful!", "OK");
                await Navigation.PushAsync(new AboutPage());
            }
            else
            {
                errorLabel.Text = "Password reset failed. Try again.";
                errorLabel.IsVisible = true;
            }
        }

        private async void OnBackToLoginClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserLoginPage());
        }
    }
}
