using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LandscapeManagement.Services;
using LandscapeManagement.Models;
using LandscapeManagement.Views.NewFolder1;

namespace LandscapeManagement.Views.NewFolder1
{
    public partial class EditUserProfile : ContentPage
    {
        private User _user;
        private readonly UserService _userService = new UserService();

        public EditUserProfile(User user)
        {
            InitializeComponent();
            _user = user;

            // Populate fields
            nameEntry.Text = _user.name;
            emailEntry.Text = _user.email;
            phoneEntry.Text = _user.phone;
            addressEditor.Text = _user.address;

        }

        private async void SaveChanges_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Validate user input
                if (string.IsNullOrWhiteSpace(nameEntry.Text) ||
                    string.IsNullOrWhiteSpace(emailEntry.Text) ||
                    string.IsNullOrWhiteSpace(phoneEntry.Text) ||
                    string.IsNullOrWhiteSpace(addressEditor.Text))
                {
                    await DisplayAlert("Validation Error", "All fields are required!", "OK");
                    return;
                }

                // Update user data
                _user.name = nameEntry.Text.Trim();
                _user.email = emailEntry.Text.Trim();
                _user.phone = phoneEntry.Text.Trim();
                _user.address = addressEditor.Text.Trim();

                // Call API to update user
                bool isUpdated = await _userService.UpdateUserAsync(_user.user_id, _user);

                if (isUpdated)
                {
                    await DisplayAlert("Success", "Profile updated successfully!", "OK");
                    await Navigation.PushAsync(new UserProfile()); // Navigate to profile page
                }
                else
                {
                    await DisplayAlert("Error", "Failed to update profile. Please try again.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Something went wrong: {ex.Message}", "OK");
            }
        }


    }
}
