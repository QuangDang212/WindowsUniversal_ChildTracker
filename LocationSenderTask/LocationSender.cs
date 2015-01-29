namespace LocationSenderTask
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using Windows.ApplicationModel.Background;
    using Windows.Devices.Geolocation;
    using Windows.Storage;
    using Windows.UI.Popups;

    //using Windows.Web.Http;

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

            var location = new BackgroundLocationModel()
            {
                Latitude = lat,
                Longitude = lon
            };

            var requester = new HttpLocationSender();
            requester.SendLocation(location);

            deferral.Complete();
        }      
    }
}
