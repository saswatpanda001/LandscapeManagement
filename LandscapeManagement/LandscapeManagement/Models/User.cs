using System;

namespace LandscapeManagement.Models
{
    public class User
    {
        public int user_id { get; set; }

        public string name { get; set; }

        public string email { get; set; }

        public string phone { get; set; }

        public string address { get; set; }

        public string role { get; set; }

        public string password { get; set; } // Consider encrypting before sending to API

        public DateTime created_at { get; set; }
    }
}
