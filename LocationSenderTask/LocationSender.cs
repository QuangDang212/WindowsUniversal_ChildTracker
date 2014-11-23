using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.ApplicationModel.Background;
using Windows.Devices.Geolocation;
using Windows.UI.Popups;

namespace LocationSenderTask
{
    public sealed class LocationSender : IBackgroundTask
    {
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            BackgroundTaskDeferral deferral = taskInstance.GetDeferral();
            
            var locator = new Geolocator();
            locator.DesiredAccuracyInMeters = 50;
            var position = await locator.GetGeopositionAsync();
            var lat = position.Coordinate.Point.Position.Latitude;
            var lon = position.Coordinate.Point.Position.Longitude;

            //var currentUser = ParseUser.CurrentUser;
            ParseObject location = new ParseObject("Location");
            location["latitude"] = lat;
            location["longitude"] = lon;
            
            await location.SaveAsync();

            
            deferral.Complete();
        }

      
    }
}
