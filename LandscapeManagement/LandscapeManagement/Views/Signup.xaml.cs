using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using LandscapeManagement.Services;
using LandscapeManagement.Models;
using LandscapeManagement.Views;
using LandscapeManagement.Views.NewFolder1;



namespace LandscapeManagement.Views
{
    public partial class Signup : ContentPage
    {
        private readonly UserService _userService;

        public Signup()
        {
            InitializeComponent();
            _userService = new UserService();
        }

        private async void OnSignupClicked(object sender, EventArgs e)
        {
            string name = nameEntry.Text;
            string email = emailEntry.Text;
            string phone = phoneEntry.Text;
            string password = passwordEntry.Text;
            string confirmPassword = confirmPasswordEntry.Text;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(confirmPassword))
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

            if (password.Length < 6)
            {
                errorLabel.Text = "Password must be at least 6 characters.";
                errorLabel.IsVisible = true;
                return;
            }

            if (password != confirmPassword)
            {
                errorLabel.Text = "Passwords do not match.";
                errorLabel.IsVisible = true;
                return;
            }

            // Create a User object
            var user = new User
            {
                name = name,
                email = email,
                phone = phone,
                password = password,
                role = "Customer" // Default role
            };

            // Call API to register user
            var response = await _userService.RegisterUserAsync(user);

            if (response)
            {
                await DisplayAlert("Success", "User registered successfully!", "OK");
                await Navigation.PushAsync(new UserLoginPage());
            }
            else
            {
                errorLabel.Text = "Registration failed. Try again.";
                errorLabel.IsVisible = true;
            }
        }


        private async void OnLoginClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserLoginPage());
        }
    }
}
