﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFormsBlogClient.Services;
using XamarinFormsBlogClient.Views;

namespace XamarinFormsBlogClient
{
    public partial class App : Application
    {

        public App()
        {
            Device.SetFlags(new[] {
                "CarouselView_Experimental",
                "IndicatorView_Experimental",
                "SwipeView_Experimental",
                "CollectionView_Experimental",
                "AppTheme_Experimental"
            });

            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
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
