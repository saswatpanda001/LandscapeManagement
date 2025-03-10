using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LandscapeManagement.Models;
using LandscapeManagement.Services;

namespace LandscapeManagement.Views.NewFolder2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManageUserPage : ContentPage
    {
        private readonly UserService _userService = new UserService();

        public ManageUserPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadUsersAsync();
        }

        private async Task LoadUsersAsync()
        {
            var users = await _userService.GetAllUsersAsync();
            if (users != null)
            {
                UsersListView.ItemsSource = users;
            }
            else
            {
                await DisplayAlert("Error", "Failed to load users.", "OK");
            }
        }

        private async void OnAddNewUserClicked(object sender, EventArgs e)
        {
            // Navigate to ManageUserDetailPage with null parameter for a new user.
            await Navigation.PushAsync(new ManageUserDetailPage(null));
        }

        private async void OnEditUserClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is int userId)
            {
                var users = (UsersListView.ItemsSource as System.Collections.IEnumerable).Cast<User>();
                var selectedUser = users.FirstOrDefault(u => u.user_id == userId);
                if (selectedUser != null)
                {
                    await Navigation.PushAsync(new ManageUserDetailPage(selectedUser));
                }
            }
        }

        private async void OnDeleteUserClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is int user_id)
            {
                bool confirm = await DisplayAlert("Confirm", "Are you sure you want to delete this user?", "Yes", "No");
                if (!confirm) return;
                
                bool deleted = await  _userService.DeleteUser(user_id);
                
                if (deleted)
                {
                    await DisplayAlert("Success", "User deleted successfully.", "OK");
                    await LoadUsersAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Failed to delete user.", "OK");
                }
            }
        }
    }
}
