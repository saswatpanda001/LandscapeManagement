using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;
using LandscapeManagement.Models;
using System.Linq;
using System.Text;




namespace LandscapeManagement.Services
{
    class SerService
    {

        private readonly HttpClient _client;
        private const string ApiUrl = "http://10.0.2.2:58505/api/Services"; // Base API URL



        public SerService()
        {
            // Disable SSL verification for local development (not recommended for production)
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };
            _client = new HttpClient(handler);

        }

        public async Task<List<Service>> GetServicesAsync()
        {
            try
            {
                var response = await _client.GetStringAsync(ApiUrl);
                return JsonConvert.DeserializeObject<List<Service>>(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching services: {ex.Message}");
                return new List<Service>();
            }
        }


        public async Task<bool> CreateServiceAsync(Service service)
        {
            var json = JsonConvert.SerializeObject(service);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(ApiUrl, content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateServiceAsync(int id, Service service)
        {
            var json = JsonConvert.SerializeObject(service);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"{ApiUrl}/{id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteServiceAsync(int id)
        {
            var response = await _client.DeleteAsync($"{ApiUrl}/{id}");
            return response.IsSuccessStatusCode;
        }







    }
}
