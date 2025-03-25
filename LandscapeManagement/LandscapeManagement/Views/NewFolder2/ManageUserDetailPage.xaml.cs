using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LandscapeManagement.Models;
using LandscapeManagement.Services;

namespace LandscapeManagement.Views.NewFolder2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManageUserDetailPage : ContentPage
    {
        private readonly UserService _userService = new UserService();
        private User _currentUser;

        // If user is null, then we're adding a new user.

        private async void OnHomeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AdminDashboard());
        }

        public ManageUserDetailPage(User user)
        {
            InitializeComponent();
            _currentUser = user;
            NavigationPage.SetHasBackButton(this, false);
            if (_currentUser != null)
            {
                // Populate fields for editing
                NameEntry.Text = _currentUser.name;
                EmailEntry.Text = _currentUser.email;
                PhoneEntry.Text = _currentUser.phone;
                AddressEntry.Text = _currentUser.address;
                PasswordEntry.Text = _currentUser.password;
                RolePicker.SelectedItem = _currentUser.role;
            }
            else
            {
                // Default for new user
                RolePicker.SelectedItem = "Customer";
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(NameEntry.Text) ||
                string.IsNullOrWhiteSpace(EmailEntry.Text) ||
                string.IsNullOrWhiteSpace(PasswordEntry.Text) ||
                RolePicker.SelectedItem == null)
            {
                await DisplayAlert("Error", "Please fill in all required fields.", "OK");
                return;
            }

            // Name Validation
            if (NameEntry.Text.Length < 3 || NameEntry.Text.Length > 50 || !System.Text.RegularExpressions.Regex.IsMatch(NameEntry.Text, @"^[a-zA-Z\s]+$"))
            {
                await DisplayAlert("Validation Error", "Name must be between 3-50 characters and contain only letters and spaces.", "OK");
                return;
            }

            // Email Validation
            if (!System.Text.RegularExpressions.Regex.IsMatch(EmailEntry.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                await DisplayAlert("Validation Error", "Please enter a valid email address.", "OK");
                return;
            }


            if (!string.IsNullOrWhiteSpace(PhoneEntry.Text) &&
    (!System.Text.RegularExpressions.Regex.IsMatch(PhoneEntry.Text, @"^\d{10}$")))
            {
                await DisplayAlert("Validation Error", "Phone number must be exactly 10 digits and contain only numbers.", "OK");
                return;
            }

            if (!string.IsNullOrWhiteSpace(AddressEntry.Text) &&
                (AddressEntry.Text.Length < 10 || AddressEntry.Text.Length > 200))
            {
                await DisplayAlert("Validation Error", "Address must be between 10 to 200 characters.", "OK");
                return;
            }

            // Password Validation
            if (PasswordEntry.Text.Length < 8 ||
                !System.Text.RegularExpressions.Regex.IsMatch(PasswordEntry.Text, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$"))
            {
                await DisplayAlert("Validation Error", "Password must be at least 8 characters long and contain uppercase, lowercase, a number, and a special character.", "OK");
                return;
            }

            try
            {
                if (_currentUser == null)
                {
                    // Create a new user object
                    _currentUser = new User
                    {
                        name = NameEntry.Text.Trim(),
                        email = EmailEntry.Text.Trim(),
                        phone = PhoneEntry.Text?.Trim(),
                        address = AddressEntry.Text?.Trim(),
                        password = PasswordEntry.Text,
                        role = RolePicker.SelectedItem.ToString(),
                        created_at = DateTime.Now
                    };

                    bool created = await _userService.RegisterUserAsync(_currentUser);
                    if (created)
                    {
                        await DisplayAlert("Success", "User created successfully.", "OK");
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Error", "Failed to create user.", "OK");
                    }
                }
                else
                {
                    // Update the existing user object
                    _currentUser.name = NameEntry.Text.Trim();
                    _currentUser.email = EmailEntry.Text.Trim();
                    _currentUser.phone = PhoneEntry.Text?.Trim();
                    _currentUser.address = AddressEntry.Text?.Trim();
                    _currentUser.password = PasswordEntry.Text;
                    _currentUser.role = RolePicker.SelectedItem.ToString();

                    bool updated = await _userService.UpdateUserAsync(_currentUser.user_id, _currentUser);
                    if (updated)
                    {
                        await DisplayAlert("Success", "User updated successfully.", "OK");
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Error", "Failed to update user.", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An unexpected error occurred: {ex.Message}", "OK");
            }
        }

    }
}
