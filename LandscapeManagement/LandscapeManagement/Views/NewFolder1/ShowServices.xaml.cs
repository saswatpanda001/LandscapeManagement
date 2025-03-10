using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using LandscapeManagement.Models;  // ✅ Ensure you use this
using LandscapeManagement.Services;
using LandscapeManagement.Views.NewFolder1;

namespace LandscapeManagement.Views.NewFolder1
{
    public partial class ShowServices : ContentPage
    {
        private readonly SerService _service;

        public ShowServices()
        {
            InitializeComponent();
            _service = new SerService();
            LoadServices();
        }

        private async void LoadServices()
        {
            try
            {
                List<LandscapeManagement.Models.Service> services = await _service.GetServicesAsync();  // ✅ Explicitly specify correct namespace
                ServiceListView.ItemsSource = services;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Failed to load services", "OK");
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

     


            private async void OnServiceSelected(object sender, ItemTappedEventArgs e){
            if (e.Item is Service selectedService)
            {
                await Navigation.PushAsync(new ServiceBookingPage(selectedService));
            }

        }
    }
}
