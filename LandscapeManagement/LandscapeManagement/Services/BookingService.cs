using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;
using LandscapeManagement.Models;
using System.Text;
using System.Linq;


namespace LandscapeManagement.Services
{
    public class BookingService
    {
        private readonly HttpClient _client;
        private const string ApiUrl = "http://10.0.2.2:58505/api/Bookings"; // Base API URL

       

        public BookingService()
        {
            // Disable SSL verification for local development (not recommended for production)
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };
            _client = new HttpClient(handler);

        }

        public async Task<List<Booking>> GetUserBookingsAsync(int loggedInUserId)
        {
            try
            {
                var response = await _client.GetStringAsync(ApiUrl);
                var allBookings = JsonConvert.DeserializeObject<List<Booking>>(response);

                // Ensure bookings have valid data
                var userBookings = allBookings?.Where(b => b.user_id == loggedInUserId).ToList() ?? new List<Booking>();

                if (userBookings.Count == 0)
                {
                    Console.WriteLine("No bookings found for user.");
                }

                return userBookings;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching bookings: {ex.Message}");
                return new List<Booking>();
            }
        }


        public async Task<List<Booking>> GeAllBookingsAsync()
        {
            try
            {
                var response = await _client.GetStringAsync(ApiUrl);
                var allBookings = JsonConvert.DeserializeObject<List<Booking>>(response);
                Console.WriteLine(allBookings.Count);
                return allBookings;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching bookings: {ex.Message}");
                return new List<Booking>();
            }
        }





        public async Task<bool> CreateBookingAsync(Booking booking)
        {
            try
            {
                string json = JsonConvert.SerializeObject(booking);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                Console.WriteLine(content);
                HttpResponseMessage response = await _client.PostAsync(ApiUrl, content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }



        public async Task<bool> DeleteBookingAsync(int bookingId)
        {
            try
            {
                var response = await _client.DeleteAsync($"{ApiUrl}/{bookingId}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                // Optionally log the exception
                System.Diagnostics.Debug.WriteLine($"Error deleting booking: {ex.Message}");
                return false;
            }
        }


        public async Task<bool> UpdateBookingAsync(Booking booking)
        {
            try
            {
                string json = JsonConvert.SerializeObject(booking);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PutAsync($"{ApiUrl}/{booking.booking_id}", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating booking: {ex.Message}");
                return false;
            }
        }














    }
}
