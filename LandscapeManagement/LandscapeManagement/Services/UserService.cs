using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using LandscapeManagement.Models;
using System.Collections.Generic;
using System.Linq;






namespace LandscapeManagement.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "http://10.0.2.2:58505/api/Users"; // Base API URL


        public UserService()
        {
            // Disable SSL verification for local development (not recommended for production)
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };
            _httpClient = new HttpClient(handler);

        }


        

        public async Task<List<User>> GetAllUsersAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(ApiUrl);
                if (!response.IsSuccessStatusCode)
                    return null;

                string jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<User>>(jsonResponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching users: {ex.Message}");
                return null;
            }
        }


        public async Task<User> LoginAsync(string email, string password)
        {
            List<User> users = await GetAllUsersAsync();
            if (users == null)
                return null;

            return users.Find(u => u.email == email && u.password == password && u.role=="Customer");
        }


        public async Task<User> AdminLoginAsync(string email, string password)
        {
            List<User> users = await GetAllUsersAsync();
            if (users == null)
                return null;

            return users.Find(u => u.email == email && u.password == password && u.role == "Admin");
        }




        // Reset Password
        public async Task<bool> ResetPasswordAsync(string email, string newPassword)
        {
            try
            {
                string apiUrl = "http://10.0.2.2:58505/api/Users"; // Base API URL

                // Fetch all users
                HttpResponseMessage getUsersResponse = await _httpClient.GetAsync(apiUrl);
                if (!getUsersResponse.IsSuccessStatusCode)
                    return false; // API error

                string usersJson = await getUsersResponse.Content.ReadAsStringAsync();
                List<User> users = JsonConvert.DeserializeObject<List<User>>(usersJson);

                // Find user by email
                User user = users.FirstOrDefault(u => u.email == email);
                if (user == null)
                    return false; 

                // Update password
                user.password = newPassword; // Consider hashing the password before sending

                // Convert to JSON
                string updatedUserJson = JsonConvert.SerializeObject(user);
                var content = new StringContent(updatedUserJson, Encoding.UTF8, "application/json");

                // Send PUT request to update user
                HttpResponseMessage response = await _httpClient.PutAsync($"{apiUrl}/{user.user_id}", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Password Reset Error: {ex.Message}");
                return false;
            }
        }


        public async Task<User> GetUserProfileAsync(int userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{ApiUrl}/{userId}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<User>(json);
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching user profile: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> UpdateUserAsync(int userId, User user)
        {
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{ApiUrl}/{user.user_id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteUser(int userId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{ApiUrl}/{userId}"); 
                Console.WriteLine(response);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RegisterUserAsync(User user)
        {
            try
            {
                // Check if email already exists
                var allUsers = await GetAllUsersAsync();
                if (allUsers != null && allUsers.Any(u => u.email.Equals(user.email, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine("Registration Error: Email already exists.");
                    return false; // Email exists; do not register
                }

                // Proceed with registration if email is unique
                string apiUrl = "http://10.0.2.2:58505/api/Users"; // API endpoint
                string json = JsonConvert.SerializeObject(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                Console.WriteLine(content);
                HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Registration Error: {ex.Message}");
                return false;
            }
        }























    }
}
