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
    public partial class AddBookings : ContentPage
    {
        public event Action<Booking> BookingAdded;

        public AddBookings()
        {
            InitializeComponent();
        }

        private async void OnSaveBookingClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EntryCustomerName.Text) || string.IsNullOrWhiteSpace(EntryBookingDate.Text) || string.IsNullOrWhiteSpace(EntryService.Text))
            {
                await DisplayAlert("Error", "Please enter valid details.", "OK");
                return;
            }

            Booking newBooking = new Booking
            {
                CustomerName = EntryCustomerName.Text,
                BookingDate = EntryBookingDate.Text,
                Service = EntryService.Text
            };

            BookingAdded?.Invoke(newBooking);

            await Navigation.PopAsync();
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}

