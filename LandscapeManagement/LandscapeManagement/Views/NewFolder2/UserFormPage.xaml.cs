using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LandscapeManagement.Services;
using LandscapeManagement.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LandscapeManagement.Views.NewFolder2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserFormPage : ContentPage
    {
        private readonly UserService _userService = new UserService();
        private User _user; // Holds user object for editing

        public UserFormPage(User user = null)  // Accepts user if editing
        {
            InitializeComponent();

            if (user == null)
            {
                // Creating a new user
                _user = new User();
                PageTitle.Text = "Add New User";
                SaveButton.Text = "Create User";
            }
            else
            {
                // Editing existing user
                _user = user;
                PageTitle.Text = "Edit User";
                SaveButton.Text = "Update User";

                // Fill form with existing user data
                NameEntry.Text = _user.name;
                EmailEntry.Text = _user.email;
                PhoneEntry.Text = _user.phone;
                AddressEntry.Text = _user.address;
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            _user.name = NameEntry.Text;
            _user.email = EmailEntry.Text;
            _user.phone = PhoneEntry.Text;
            _user.address = AddressEntry.Text;

            bool success;

            if (_user.user_id == 0)  // New user
                success = await _userService.RegisterUserAsync(_user);
            else  // Editing existing user
                success = await _userService.UpdateUserAsync(_user.user_id,_user);

            if (success)
            {
                await DisplayAlert("Success", "User saved successfully!", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Failed to save user.", "OK");
            }
        }
    }
}