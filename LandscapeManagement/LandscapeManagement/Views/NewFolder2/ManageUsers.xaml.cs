using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LandscapeManagement.Services;
using LandscapeManagement.Models;
using System.Threading.Tasks;
using LandscapeManagement.Views.NewFolder2;


namespace LandscapeManagement.Views.NewFolder2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManageUsers : ContentPage
    {
        private readonly UserService _userService = new UserService();
        public ObservableCollection<User> Users { get; set; }

        public ManageUsers()
        {
            InitializeComponent();
            Users = new ObservableCollection<User>();
            BindingContext = this;
            LoadUsers();
        }

        private async void LoadUsers()
        {
            var usersList = await _userService.GetAllUsersAsync();
            Users.Clear();
            foreach (var user in usersList)
            {
                Users.Add(user);
            }
        }

        private async void OnAddUserClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserFormPage());  // No user passed → Create mode
        }

        private async void OnEditUserClicked(object sender, EventArgs e)
        {
            var user = (User)((Button)sender).CommandParameter;
            await Navigation.PushAsync(new UserFormPage(user));  // Pass user → Edit mode
        }
        private async void OnDeleteUserClicked(object sender, EventArgs e)
        {
            var user = (User)((Button)sender).CommandParameter;
            bool confirm = await DisplayAlert("Confirm", $"Delete {user.name}?", "Yes", "No");
            if (!confirm) return;

            bool success = await _userService.DeleteUser(user.user_id);
            if (success)
            {
                Users.Remove(user);
                await DisplayAlert("Deleted", $"{user.name} removed.", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Failed to delete user.", "OK");
            }
        }
    }
}
