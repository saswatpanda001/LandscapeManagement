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
        public ManageUserDetailPage(User user)
        {
            InitializeComponent();
            _currentUser = user;
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

            if (_currentUser == null)
            {
                // Create a new user object
                _currentUser = new User
                {
                    name = NameEntry.Text,
                    email = EmailEntry.Text,
                    phone = PhoneEntry.Text,
                    address = AddressEntry.Text,
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
                _currentUser.name = NameEntry.Text;
                _currentUser.email = EmailEntry.Text;
                _currentUser.phone = PhoneEntry.Text;
                _currentUser.address = AddressEntry.Text;
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
    }
}
