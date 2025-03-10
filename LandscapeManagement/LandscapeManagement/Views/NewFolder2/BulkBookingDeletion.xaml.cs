using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LandscapeManagement.Models;
using LandscapeManagement.Services;
using System.Collections.ObjectModel;

namespace LandscapeManagement.Views.NewFolder2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BulkBookingDeletion : ContentPage
    {
        // Using an observable collection to allow UI updates.
        public ObservableCollection<Booking> Bookings { get; set; }
        private readonly BookingService _bookingService = new BookingService();

        public BulkBookingDeletion()
        {
            InitializeComponent();
            Bookings = new ObservableCollection<Booking>();
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadBookingsAsync();
        }

        private async Task LoadBookingsAsync()
        {
            var bookings = await _bookingService.GeAllBookingsAsync();
            Bookings.Clear();
            foreach (var booking in bookings)
            {
                booking.IsSelected = false; // Initialize selection to false
                Bookings.Add(booking);
            }
            BookingsCollectionView.ItemsSource = Bookings;
        }

        private async void OnDeleteSelectedClicked(object sender, EventArgs e)
        {
            var selectedBookings = Bookings.Where(b => b.IsSelected).ToList();
            if (!selectedBookings.Any())
            {
                await DisplayAlert("Error", "No bookings selected for deletion.", "OK");
                return;
            }

            bool confirm = await DisplayAlert("Confirm", $"Are you sure you want to delete {selectedBookings.Count} booking(s)?", "Yes", "No");
            if (!confirm) return;

            bool allDeleted = true;
            foreach (var booking in selectedBookings.ToList())
            {
                bool deleted = await _bookingService.DeleteBookingAsync(booking.booking_id);
                if (deleted)
                {
                    Bookings.Remove(booking);
                }
                else
                {
                    allDeleted = false;
                }
            }

            if (allDeleted)
                await DisplayAlert("Success", "Selected bookings deleted successfully.", "OK");
            else
                await DisplayAlert("Partial Success", "Some bookings could not be deleted.", "OK");
        }
    }
}
