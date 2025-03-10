using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LandscapeManagement.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LandscapeManagement.Views.NewFolder1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookingConfirmation : ContentPage
    {
        public BookingConfirmation()
        {
            InitializeComponent();
        }

        private void GoToDashboard(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UserDashboard());
        }

    }
}