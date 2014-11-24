using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Popups;
using Windows.Devices.Geolocation;
using ChildTracker.Models;
using Windows.UI.Xaml;
using Windows.ApplicationModel.Background;
using Parse;
using LocationSenderTask;
using Windows.Storage;
namespace ChildTracker.ViewModels
{
    public class ChildDeviceViewModel : ViewModelBase
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

            ////Background task
            //string taskName = "Localization task";
            //bool isRegistered = false;
            //// check if task is already registered
            //foreach (var cur in BackgroundTaskRegistration.AllTasks)
            //    if (cur.Value.Name == taskName)
            //    {
            //        isRegistered = true;
            //    }

            //if (!isRegistered)
            //{
            //    await BackgroundExecutionManager.RequestAccessAsync();
            //    BackgroundTaskBuilder taskBuilder = new BackgroundTaskBuilder { Name = taskName, TaskEntryPoint = "LocationSenderTask.LocationSender" };
            //    taskBuilder.SetTrigger(new TimeTrigger(30, false));
            //    BackgroundTaskRegistration myTask = taskBuilder.Register();

            //    await (new MessageDialog("Task registered")).ShowAsync();
            //}
        }
    }
}
