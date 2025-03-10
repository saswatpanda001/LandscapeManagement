using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LandscapeManagement.Views.NewFolder1;
using LandscapeManagement.Views.NewFolder2;


namespace LandscapeManagement.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }
        private async void OnCustomerLoginClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserLoginPage());
        }

        private async void OnAdminLoginClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AdminLogin());
        }


    }
}