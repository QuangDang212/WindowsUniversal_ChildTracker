using ChildTracker.Helpers;
using ChildTracker.Models;
using ChildTracker.SQLiteManagement;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Parse;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Devices.Geolocation;
using Windows.Storage;

namespace ChildTracker.ViewModels
{
    public class ParentDeviceViewModel:ViewModelBase
    {

        private BasicGeoposition _currentSelection;
        private SQLiteLocationsDBHelper _locationsDb;
        private ObservableCollection<SQLiteLocationModel> _latestLocationReviews;

        public ParentDeviceViewModel()
        {
            this._locationsDb = SQLiteLocationsDBHelper.Instance();
            this._locationsDb.InitializeDB();
            this.GetLocationsFromSQLiteAsync();
        }

        public IEnumerable<SQLiteLocationModel> LatestLocationReviews
        {
            get 
             {
                if (this._latestLocationReviews == null)
                {
                    this._latestLocationReviews = new ObservableCollection<SQLiteLocationModel>();
                }
                return this._latestLocationReviews;
            }
            set
            {
             if (this._latestLocationReviews == null)
                {
                    this._latestLocationReviews = new ObservableCollection<SQLiteLocationModel>();
                }
                this._latestLocationReviews.Clear();
                foreach (var item in value)
                {
                    this._latestLocationReviews.Add(item);
                }
            }
        }

        public BasicGeoposition CurrentSelection
        {
            get
            {
                return this._currentSelection;
            }
            set
            {
             this._currentSelection = value   ;
             this.RaisePropertyChanged(() => this.CurrentSelection);
            }
        }

        public async Task GetChildLastLocation()
        {

            var requester = new LocationsHttpRequester();
            var latestLocation = await requester.GetLocation();
            

            //TODO: >>Add user filtraton for multiple users

            if (latestLocation!=null)
            {
                var position = new BasicGeoposition();
                position.Latitude = latestLocation.Latitude;
                position.Longitude = latestLocation.Longitude;
                this.CurrentSelection = position;
                
                var dbLocation = new SQLiteLocationModel(){
                    Latitude = latestLocation.Latitude,
                    Longitude = latestLocation.Longitude,
                    LocationDate = (DateTime)latestLocation.CreationDate,
                    UserId = LocalData.USERNAME
                };

                ((ObservableCollection<SQLiteLocationModel>)this.LatestLocationReviews).Insert(0, dbLocation);
                await this._locationsDb.AddLocation(dbLocation);
            }
        }

        private async void GetLocationsFromSQLiteAsync()
        {
            //As emails are unique for all users it will be the userID in the SQLite
            var userId = LocalData.USERNAME;
            this.LatestLocationReviews = await this._locationsDb.GetLatestFiveLocationsForUser(userId);
        }

        public void Logout()
        {
            var requester = new LocationsHttpRequester();
            requester.Logout();
        }
    }
}
