﻿using LandscapeManagement.Services;
using LandscapeManagement.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;



namespace LandscapeManagement
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new NavigationPage(new AboutPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
