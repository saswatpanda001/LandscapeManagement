using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;




namespace LandscapeManagement.Views.NewFolder2
{
    public class Booking
    {
        public string CustomerName { get; set; }
        public string BookingDate { get; set; }
        public string Service { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
    }
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManageBookings : ContentPage
    {
        public ObservableCollection<Booking> Bookings { get; set; }

        public ManageBookings()
        {
            InitializeComponent();
            Bookings = new ObservableCollection<Booking>
            {
                new Booking { CustomerName = "John Doe", BookingDate = "March 1, 2025", Service = "Landscaping" },
                new Booking { CustomerName = "Jane Smith", BookingDate = "March 5, 2025", Service = "Garden Design" },
                new Booking { CustomerName = "Michael Brown", BookingDate = "March 10, 2025", Service = "Tree Trimming" }
            };

            foreach (var booking in Bookings)
            {
                booking.EditCommand = new Command<Booking>(OnEditBooking);
                booking.DeleteCommand = new Command<Booking>(OnDeleteBooking);
            }

            BindingContext = this;
        }

        private async void OnAddBookingClicked(object sender, EventArgs e)
        {
           
        }


        private async void OnClickedEditBookings(object sender, EventArgs e)
        {
            
        }

        private void OnBookingAdded(Booking newBooking)
        {
            if (newBooking != null)
            {
                newBooking.EditCommand = new Command<Booking>(OnEditBooking);
                newBooking.DeleteCommand = new Command<Booking>(OnDeleteBooking);
                Bookings.Add(newBooking);
            }
        }

        private void OnEditBooking(Booking booking)
        {
            DisplayAlert("Edit Booking", $"Editing {booking.CustomerName}'s booking.", "OK");
        }

        private void OnDeleteBooking(Booking booking)
        {
            if (Bookings.Contains(booking))
            {
                Bookings.Remove(booking);
                DisplayAlert("Booking Deleted", $"{booking.CustomerName}'s booking has been deleted.", "OK");
            }
        }
    }
}
