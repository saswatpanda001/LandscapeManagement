using System;
using System.Threading.Tasks;
using LandscapeManagement.Views.NewFolder2;
using LandscapeManagement.Models;
using LandscapeManagement.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LandscapeManagement.Views.NewFolder2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManageServicePage : ContentPage
    {
        private readonly SerService _serviceService = new SerService();

        public ManageServicePage()
        {
            InitializeComponent();
            LoadServices();
            NavigationPage.SetHasBackButton(this, false);
        }

        private async void LoadServices()
        {
            try
            {
                ServiceListView.ItemsSource = await _serviceService.GetServicesAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void OnAddServiceClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ManageServiceDetailPage(null));
        }

        private async void OnHomeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AdminDashboard());
        }


        private async void OnEditServiceClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var service = (Service)button.CommandParameter;
            if (service != null)
            {
                await Navigation.PushAsync(new ManageServiceDetailPage(service));
            }
            else
            {
                await DisplayAlert("Error", "Failed to load service details.", "OK");
            }
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var service = (Service)button.CommandParameter;
            if (service != null)
            {
                var confirm = await DisplayAlert("Confirm", $"Are you sure you want to delete {service.service_name}?", "Yes", "No");
                if (confirm)
                {
                    await _serviceService.DeleteServiceAsync(service.service_id);
                    LoadServices();
                }
            }
            else
            {
                await DisplayAlert("Error", "Failed to delete service.", "OK");
            }
        }
    }
}
