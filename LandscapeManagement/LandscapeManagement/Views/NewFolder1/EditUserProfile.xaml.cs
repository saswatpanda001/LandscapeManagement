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

        private async void OnHomeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserDashboard());

        }


        public EditUserProfile(User user)
        {
            InitializeComponent();
            _user = user;

            // Populate fields
            nameEntry.Text = _user.name;
            emailEntry.Text = _user.email;
            phoneEntry.Text = _user.phone;
            addressEditor.Text = _user.address;
            NavigationPage.SetHasBackButton(this, false);


        }

        private async void SaveChanges_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Validate user input
                if (string.IsNullOrWhiteSpace(nameEntry.Text) || nameEntry.Text.Length < 3)
                {
                    await DisplayAlert("Validation Error", "Name must be at least 3 characters long.", "OK");
                    return;
                }

                if (string.IsNullOrWhiteSpace(emailEntry.Text) || !IsValidEmail(emailEntry.Text))
                {
                    await DisplayAlert("Validation Error", "Please enter a valid email address.", "OK");
                    return;
                }

                if (string.IsNullOrWhiteSpace(phoneEntry.Text) || phoneEntry.Text.Length != 10 || !phoneEntry.Text.All(char.IsDigit))
                {
                    await DisplayAlert("Validation Error", "Phone number must be exactly 10 digits and contain only numbers.", "OK");
                    return;
                }

                if (string.IsNullOrWhiteSpace(addressEditor.Text) || addressEditor.Text.Length < 10)
                {
                    await DisplayAlert("Validation Error", "Address must be at least 10 characters long.", "OK");
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

        // Email validation function using regular expression
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
