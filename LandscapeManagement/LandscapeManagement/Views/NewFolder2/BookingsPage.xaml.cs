using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LandscapeManagement.Services;
using LandscapeManagement.Models;
using LandscapeManagement.Views.NewFolder2;

namespace LandscapeManagement.Views.NewFolder2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookingsPage : ContentPage
    {
        private readonly BookingService _bookingService;

        public BookingsPage()
        {
            InitializeComponent();
            _bookingService = new BookingService();  // Initialize your booking service
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // Fetch all bookings and bind them to the ListView
            var bookings = await _bookingService.GeAllBookingsAsync();
            BookingsListView.ItemsSource = bookings;
        }

        // Command for Edit button
        public Command<int> EditCommand => new Command<int>(async (bookingId) => await EditBooking(bookingId));

        // Command for Delete button
        public Command<int> DeleteCommand => new Command<int>(async (bookingId) => await DeleteBooking(bookingId));

        // Edit Booking logic
        private async Task EditBooking(int bookingId)
        {
            var booking = await _bookingService.GetUserBookingsAsync(bookingId);
            if (booking != null)
            {
                // Navigate to the EditBookingPage to edit the selected booking
                await Navigation.PushAsync(new EditBookingPage(booking));
            }
            else
            {
                await DisplayAlert("Error", "Booking not found.", "OK");
            }
        }

        // Delete Booking logic
        private async Task DeleteBooking(int bookingId)
        {
            bool isDeleted = await _bookingService.DeleteBookingAsync(bookingId);
            if (isDeleted)
            {
                await DisplayAlert("Success", "Booking deleted successfully!", "OK");
                // Refresh the booking list after deletion
                OnAppearing();
            }
            else
            {
                await DisplayAlert("Error", "Failed to delete booking.", "OK");
            }
        }
    }
}