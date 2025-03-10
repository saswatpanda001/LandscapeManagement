using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LandscapeManagement.Services;
using LandscapeManagement.Models;
using LandscapeManagement.Views.NewFolder1;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LandscapeManagement.Views.NewFolder2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditBookingPage : ContentPage
    {
        private readonly BookingService _bookingService;
        private Booking _bookingToEdit;

        public EditBookingPage(Booking booking)
        {
            InitializeComponent();
            _bookingService = new BookingService();
            _bookingToEdit = booking;

            // Bind current booking values to UI elements
            ServiceNameEntry.Text = booking.Service.service_name;
            BookingDatePicker.Date = booking.booking_date;
            StatusPicker.SelectedItem = booking.status;
        }

        public Command SaveCommand => new Command(async () => await SaveBooking());

        private async Task SaveBooking()
        {
            // Update the booking with new values
            _bookingToEdit.booking_date = BookingDatePicker.Date;
            _bookingToEdit.status = StatusPicker.SelectedItem.ToString();

            bool result = await _bookingService.UpdateBookingAsync(_bookingToEdit);
            if (result)
            {
                await DisplayAlert("Success", "Booking updated successfully!", "OK");
                await Navigation.PopAsync();  // Go back to the previous page
            }
            else
            {
                await DisplayAlert("Error", "Failed to update booking.", "OK");
            }
        }
    }
}