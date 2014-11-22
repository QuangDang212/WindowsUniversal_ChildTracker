using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Popups;
using Windows.Devices.Geolocation;
using ChildTracker.Models;
namespace ChildTracker.ViewModels
{
    public class ChildDeviceViewModel:ViewModelBase
    {
        public ChildDeviceViewModel()
        {

            this.SendLocation();
        }

        private async void SendLocation()
        {
            var locator = new Geolocator();
            locator.DesiredAccuracyInMeters = 50;
            var position = await locator.GetGeopositionAsync();
            var lat = position.Coordinate.Point.Position.Latitude;
            var lon = position.Coordinate.Point.Position.Longitude;

            var location = new LocationModel()
            {
                Latitude = lat,
                Longitude = lon
            };

            await location.SaveAsync();

            var popup = new MessageDialog(string.Format("latitude: {0} \nlongitude: {1} ", lat, lon));
            await popup.ShowAsync();
        }
    }
}
