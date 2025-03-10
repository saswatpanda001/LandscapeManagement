using System;
using LandscapeManagement.Models;
using LandscapeManagement.Services;
using Xamarin.Forms;

namespace LandscapeManagement.Views.NewFolder1
{
    public partial class ServiceBookingPage : ContentPage
    {
        private readonly Service _selectedService;
        private readonly BookingService _bookingService;

        public ServiceBookingPage(Service service)
        {
            InitializeComponent();
            _selectedService = service ?? throw new ArgumentNullException(nameof(service));
            _bookingService = new BookingService();

            // Set UI elements
            SelectedServiceLabel.Text = $"Selected Service: {_selectedService.service_name}"; // ✅ Use correct property name
            ServiceDatePicker.MinimumDate = DateTime.Today;  // ✅ Set minimum date here
        }

        private async void OnBookNowClicked(object sender, EventArgs e)
        {
            try
            {
                int userId = SessionManager.LoggedInUser?.user_id ?? 0;
                if (userId == 0)
                {
                    await DisplayAlert("Error", "User not logged in!", "OK");
                    return;
                }

                var booking = new Booking
                {
                    user_id = userId,
                    service_id = _selectedService.service_id, // ✅ Use correct property name
                    booking_date = ServiceDatePicker.Date.Add(ServiceTimePicker.Time),
                    status = "Pending",
                    created_at = DateTime.Now
                };

                bool isSuccess = await _bookingService.CreateBookingAsync(booking);
                if (isSuccess)
                {
                    await DisplayAlert("Success", "Booking Confirmed!", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Booking failed. Try again.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }
    }
}
