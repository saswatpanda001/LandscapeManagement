using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LandscapeManagement.Views.NewFolder2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditBookings : ContentPage
    {
        public EditBookings()
        {
            InitializeComponent();
        }

        private async void OnSave(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ManageBookings());
        }
        private async void OnCancel(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ManageBookings());
        }

    }
}