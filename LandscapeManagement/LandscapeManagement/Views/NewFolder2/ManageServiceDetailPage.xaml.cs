using System;
using LandscapeManagement.Models;
using LandscapeManagement.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LandscapeManagement.Views.NewFolder2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManageServiceDetailPage : ContentPage
    {
        private readonly SerService _serviceService = new SerService();
        private Service _currentService;

        public ManageServiceDetailPage(Service service)
        {
            InitializeComponent();
            _currentService = service ?? new Service();
            BindingContext = _currentService;
            NavigationPage.SetHasBackButton(this, false);
        }


        private async void OnHomeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AdminDashboard());
        }


        private async void OnSaveClicked(object sender, EventArgs e)
        {
            // Field Presence Check
            if (string.IsNullOrWhiteSpace(ServiceNameEntry.Text) ||
                string.IsNullOrWhiteSpace(DescriptionEditor.Text) ||
                string.IsNullOrWhiteSpace(PriceEntry.Text) ||
                string.IsNullOrWhiteSpace(DurationEntry.Text))
            {
                await DisplayAlert("Validation Error", "Please fill in all fields.", "OK");
                return;
            }

            // Service Name Validation
            if (ServiceNameEntry.Text.Length < 3 || ServiceNameEntry.Text.Length > 50)
            {
                await DisplayAlert("Validation Error", "Service name must be between 3 and 50 characters.", "OK");
                return;
            }

            // Description Validation
            if (DescriptionEditor.Text.Length < 10 || DescriptionEditor.Text.Length > 500)
            {
                await DisplayAlert("Validation Error", "Description must be between 10 and 500 characters.", "OK");
                return;
            }

            // Price Validation
            if (!decimal.TryParse(PriceEntry.Text, out decimal price) || price <= 0)
            {
                await DisplayAlert("Validation Error", "Please enter a valid price greater than 0.", "OK");
                return;
            }

            // Duration Validation
            if (!int.TryParse(DurationEntry.Text, out int duration) || duration <= 0)
            {
                await DisplayAlert("Validation Error", "Please enter a valid duration in minutes.", "OK");
                return;
            }

            try
            {
                // Assign values to service object
                _currentService.service_name = ServiceNameEntry.Text.Trim();
                _currentService.description = DescriptionEditor.Text.Trim();
                _currentService.price = price;
                _currentService.duration = duration;

                bool success;

                // Determine whether to create or update
                if (_currentService.service_id == 0)
                {
                    success = await _serviceService.CreateServiceAsync(_currentService);
                }
                else
                {
                    success = await _serviceService.UpdateServiceAsync(_currentService.service_id, _currentService);
                }

                if (success)
                {
                    await DisplayAlert("Success", "Service saved successfully.", "OK");
                    await Navigation.PushAsync(new ManageServicePage());
                }
                else
                {
                    await DisplayAlert("Error", "Failed to save service.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An unexpected error occurred: {ex.Message}", "OK");
            }
        }

    }
}
