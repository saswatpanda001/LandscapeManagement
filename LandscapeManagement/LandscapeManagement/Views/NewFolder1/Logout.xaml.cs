using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LandscapeManagement.Views.NewFolder1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Logout : ContentPage
    {
        public Logout()
        {
            InitializeComponent();
        }
        private void OnCancelClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync(); // Go back to the previous page
        }

        private void OnLogoutClicked(object sender, EventArgs e)
        {
            // Navigate to Login Page and clear navigation stack
            Application.Current.MainPage = new NavigationPage(new AboutPage());
        }


    }
}