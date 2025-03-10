using System;

namespace LandscapeManagement.Models
{
    public class Service
    {
        public int service_id { get; set; }

        public string service_name { get; set; }

        public string description { get; set; }

        public decimal price { get; set; }

        public int duration { get; set; } // Duration in minutes
        public bool IsSelected { get; set; }  // New property for bulk selection
    }
}
