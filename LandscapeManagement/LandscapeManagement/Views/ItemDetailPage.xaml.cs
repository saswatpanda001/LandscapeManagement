using LandscapeManagement.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace LandscapeManagement.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}