using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using LandscapeManagement.Services;
using LandscapeManagement.Models;
using LandscapeManagement.Views.NewFolder1;


namespace LandscapeManagement.Views
{
    public partial class BookingHistory : ContentPage
    {
        private readonly BookingService _bookingService = new BookingService();
        private int loggedInUserId = SessionManager.LoggedInUser.user_id; // Replace this with actual logged-in user ID

        public BookingHistory()
        {
            InitializeComponent();
            LoadBookings();
            NavigationPage.SetHasBackButton(this, false);
        }

        private async void OnHomeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserDashboard());
        }

        private async void LoadBookings()
        {
            var bookings = await _bookingService.GetUserBookingsAsync(loggedInUserId);
            BookingListView.ItemsSource = bookings;
        }

        private async void OnDeleteBookingClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.BindingContext is Booking booking)
            {
                bool confirm = await DisplayAlert("Delete Booking", "Are you sure you want to delete this booking?", "Yes", "No");
                if (confirm)
                {
                    bool isDeleted = await _bookingService.DeleteBookingAsync(booking.booking_id);
                    if (isDeleted)
                    {
                        await DisplayAlert("Success", "Booking deleted successfully!", "OK");
                        LoadBookings(); // Refresh the list
                    }
                    else
                    {
                        await DisplayAlert("Error", "Failed to delete booking. Please try again.", "OK");
                    }
                }
            }
        }
    }
}
