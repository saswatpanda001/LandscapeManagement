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
    public partial class Service : ContentPage
    {
        public Service()
        {
           
        }

        private async void OnBookNowClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BookingConfirmation());
        }

    }
}