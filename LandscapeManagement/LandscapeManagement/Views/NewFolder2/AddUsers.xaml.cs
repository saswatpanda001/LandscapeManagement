using System;
using Xamarin.Forms;
using LandscapeManagement.Models;
using LandscapeManagement.Services;

namespace LandscapeManagement.Views.NewFolder2
{
    public partial class AddUsers : ContentPage
    {
        public event Action<User> UserAdded; // Event to send data back

        public AddUsers()
        {
            InitializeComponent();
        }

        private async void OnSaveUserClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EntryName.Text) || string.IsNullOrWhiteSpace(EntryEmail.Text))
            {
                await DisplayAlert("Error", "Please enter valid name and email.", "OK");
                return;
            }

            User newUser = new User
            {
                name = EntryName.Text,
                email = EntryEmail.Text
            };

            UserAdded?.Invoke(newUser); // Send data back

            await Navigation.PopAsync(); // Go back to ManageUsers
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
