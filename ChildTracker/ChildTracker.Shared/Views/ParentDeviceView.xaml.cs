﻿using ChildTracker.ViewModels;
using Parse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ChildTracker.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ParentDeviceView : Page
    {
        public const string PageKey = "Parent";

        public ParentDeviceView()
        {
            this.InitializeComponent();

            this.ViewModel = new ParentDeviceViewModel();
        }

        private void OnLogoutClick(object sender, RoutedEventArgs e)
        {
            ParseUser.LogOut();
            this.Frame.Navigate(typeof(LoginSignupPage));
        }

        public ParentDeviceViewModel ViewModel
        {
            get
            {
                return this.DataContext as ParentDeviceViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }

        private async void GetLocationBtn_Click(object sender, RoutedEventArgs e)
        {
            await this.ViewModel.GetChildLastLocation();
            this.ChildLocationMap.Center = new Geopoint(this.ViewModel.CurrentSelection);
            this.ChildLocationMap.Zoom = 10d;
        }

        private void MapZoom_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (this.ChildLocationMap != null)
            {
                this.ChildLocationMap.Zoom = e.NewValue;
            }
        }
    }
}
