using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LandscapeManagement.Services;
using LandscapeManagement.Models;
using LandscapeManagement.Views.NewFolder2;
using System;
using System.IO;
using System.Text;
using Xamarin.Essentials;



namespace LandscapeManagement.Views.NewFolder2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminDashboard : ContentPage
    {
        private readonly UserService _userService = new UserService();
        private readonly SerService _serviceService = new SerService();
        private readonly BookingService _bookingService = new BookingService();

        public AdminDashboard()
        {
            InitializeComponent();
            LoadDashboardData();
        }

        private async void LoadDashboardData()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                var services = await _serviceService.GetServicesAsync();
                var bookings = await _bookingService.GeAllBookingsAsync();



                int totalUsers = users?.Count ?? 0;
                int totalServices = services?.Count ?? 0;
                int totalBookings = bookings?.Count ?? 0;

                var lastThreeBookings = bookings?.OrderByDescending(b => b.booking_date).Take(3).ToList();


                // Update UI
                lblTotalUsers.Text = totalUsers.ToString();
                lblTotalServices.Text = totalServices.ToString();
                lblTotalBookings.Text = totalBookings.ToString();
                lstRecentBookings.ItemsSource = lastThreeBookings;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading dashboard data: {ex.Message}");
            }
        }



        private async void OnClikedBulkBookingDeletion(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BulkBookingDeletion());
        }

        private async void OnClikedBulkServiceDeletion(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BulkServiceDeletion());
        }

        private async void GoToManageUser(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ManageUserPage());
        }
       



        private async void OnGenerateReportClicked(object sender, EventArgs e)
        {
            try
            {
                // 1. Gather data from services
                var users = await _userService.GetAllUsersAsync();
                var services = await _serviceService.GetServicesAsync();
                var bookings = await _bookingService.GeAllBookingsAsync();

                // 2. Build a simple report as a text string
                var sb = new StringBuilder();
                sb.AppendLine("REPORT SUMMARY");
                sb.AppendLine($"Total Users: {users?.Count ?? 0}");
                sb.AppendLine($"Total Services: {services?.Count ?? 0}");
                sb.AppendLine($"Total Bookings: {bookings?.Count ?? 0}");
                sb.AppendLine();
                sb.AppendLine("----- Users -----");
                if (users != null)
                {
                    foreach (var user in users)
                    {
                        sb.AppendLine($"ID: {user.user_id}, Name: {user.name}, Email: {user.email}, Role: {user.role}");
                    }
                }
                sb.AppendLine();
                sb.AppendLine("----- Services -----");
                if (services != null)
                {
                    foreach (var service in services)
                    {
                        sb.AppendLine($"ID: {service.service_id}, Name: {service.service_name}, Description: {service.description}");
                    }
                }
                sb.AppendLine();
                sb.AppendLine("----- Bookings -----");
                if (bookings != null)
                {
                    foreach (var booking in bookings)
                    {
                        sb.AppendLine($"Booking ID: {booking.booking_id}, User ID: {booking.user_id}, Service ID: {booking.service_id}, Date: {booking.booking_date:yyyy-MM-dd}");
                    }
                }

                // 3. Save the report file to local storage (e.g., LocalApplicationData folder)
                var fileName = $"Report_{DateTime.Now:yyyyMMddHHmmss}.txt";
                var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                var filePath = System.IO.Path.Combine(folderPath, fileName);
                System.IO.File.WriteAllText(filePath, sb.ToString());

                // 4. Open the share sheet so the admin can choose an app (e.g., Gmail, Drive) to share or save the report
                await Share.RequestAsync(new ShareFileRequest
                {
                    Title = "Report Generated",
                    File = new ShareFile(filePath)
                });

                // Optionally, alert that the report has been saved successfully.
                await DisplayAlert("Report Generated", $"Report generated and saved at:\n{filePath}", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Failed to generate report: " + ex.Message, "OK");
            }
        }

    }
}

