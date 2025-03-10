using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using LandscapeManagement.Models;

namespace LandscapeManagement.Services
{
    public class AuthService
    {
        private readonly HttpClient _client;

        private readonly string baseUrl = "http://10.0.2.2:58505/api/Users"; // Update API URL if needed

        public AuthService()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };
            _client = new HttpClient(handler);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            try
            {
                var response = await _client.GetStringAsync(baseUrl);
                return JsonConvert.DeserializeObject<List<User>>(response) ?? new List<User>();
            }
            catch (Exception)
            {
                return new List<User>(); // Return empty list on failure
            }
        }
    }
}
