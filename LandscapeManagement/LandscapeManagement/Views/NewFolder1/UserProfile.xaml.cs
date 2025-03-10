using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LandscapeManagement.Models;
using LandscapeManagement.Services;
using LandscapeManagement.Views.NewFolder1;

namespace LandscapeManagement.Views.NewFolder1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserProfile : ContentPage
    {
        private readonly UserService _userService;

        public UserProfile()
        {
            InitializeComponent();
            _userService = new UserService();
            LoadUserProfile();
        }

        private async void LoadUserProfile()
        {
            int userId = SessionManager.LoggedInUser.user_id; // Get logged-in user ID
            User user = await _userService.GetUserProfileAsync(userId);

            if (user != null)
            {
                UserNameLabel.Text = user.name;
                UserEmailLabel.Text = user.email;
                UserPhoneLabel.Text = $"Phone: {user.phone}";
                UserAddressLabel.Text = $"Address: {user.address}";
            }
            else
            {
                await DisplayAlert("Error", "Failed to load user profile.", "OK");
            }
        }

        private async void GoToEditProfile(object sender, EventArgs e)
        {
            var user = new User
            {
                user_id = SessionManager.LoggedInUser.user_id,
                name = SessionManager.LoggedInUser.name,
                email = SessionManager.LoggedInUser.email,
                phone = SessionManager.LoggedInUser.phone,
                address = SessionManager.LoggedInUser.address,
                role = SessionManager.LoggedInUser.role,
                password = SessionManager.LoggedInUser.password
            };

            await Navigation.PushAsync(new EditUserProfile(user));
        }


        private async void GoToResetPassword(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PasswordReset());
        }

        private async void GoToLogout(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Logout());
        }
    }
}
