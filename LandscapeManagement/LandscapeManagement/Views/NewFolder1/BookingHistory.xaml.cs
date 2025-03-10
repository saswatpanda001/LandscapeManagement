using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using LandscapeManagement.Services;
using LandscapeManagement.Models;
using LandscapeManagement.ViewModels;


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
        }

        private async void LoadBookings()
        {
            var bookings = await _bookingService.GetUserBookingsAsync(loggedInUserId);
            BookingListView.ItemsSource = bookings;
        }
    }
}
