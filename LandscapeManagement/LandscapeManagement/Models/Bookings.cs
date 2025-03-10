using System;
using Newtonsoft.Json;

namespace LandscapeManagement.Models
{
    public class Booking
    {
        public int booking_id { get; set; }

        public int user_id { get; set; }

        public int service_id { get; set; }

        public DateTime booking_date { get; set; }

        public string status { get; set; } = "Pending"; // Default status

        public DateTime created_at { get; set; }

        // Navigation properties (optional, can be used for UI binding)
        public User User { get; set; }
        public Service Service { get; set; }
        public bool IsSelected { get; set; }
    }
}
