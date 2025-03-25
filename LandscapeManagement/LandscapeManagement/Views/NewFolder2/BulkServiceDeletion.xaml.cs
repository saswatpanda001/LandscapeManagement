
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LandscapeManagement.Models;
using LandscapeManagement.Services;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LandscapeManagement.Views.NewFolder2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BulkServiceDeletion : ContentPage
    {
        private readonly SerService _service;
        public ObservableCollection<Service> Services { get; set; }

        public BulkServiceDeletion()
        {
            InitializeComponent();
            _service = new SerService();
            Services = new ObservableCollection<Service>();
            BindingContext = this;
            NavigationPage.SetHasBackButton(this, false);
        }

        private async void OnHomeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AdminDashboard());
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadServices();
        }

        private async Task LoadServices()
        {
            var services = await _service.GetServicesAsync();
            Services.Clear();
            foreach (var service in services)
            {
                service.IsSelected = false;
                Services.Add(service);
            }
        }

        private void OnSelectAllClicked(object sender, EventArgs e)
        {
            bool allSelected = Services.All(s => s.IsSelected);

            foreach (var service in Services)
            {
                service.IsSelected = !allSelected;
            }

            // Refresh CollectionView
            ServicesCollectionView.ItemsSource = null;
            ServicesCollectionView.ItemsSource = Services;
        }
        private async void OnDeleteSelected(object sender, EventArgs e)
        {
            var selectedServices = Services.Where(s => s.IsSelected).ToList();

            if (selectedServices.Count == 0)
            {
                await DisplayAlert("Error", "No services selected for deletion.", "OK");
                return;
            }

            bool confirm = await DisplayAlert("Confirm", $"Delete {selectedServices.Count} services?", "Yes", "No");
            if (!confirm) return;

            foreach (var service in selectedServices.ToList())
            {
                bool deleted = await _service.DeleteServiceAsync(service.service_id);
                if (deleted)
                {
                    Services.Remove(service);
                }
            }

            await DisplayAlert("Success", "Selected services deleted.", "OK");
        }
    }
}
