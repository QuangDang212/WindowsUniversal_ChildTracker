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
            this.GetLatestLocations();
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
            var latestLocation = await new ParseQuery<LocationModel>()
                .OrderByDescending(l=>l.CreatedAt)
                .FirstOrDefaultAsync();
            
            if (latestLocation!=null)
            {
                var position = new BasicGeoposition();
                position.Latitude = latestLocation.Latitude;
                position.Longitude = latestLocation.Longitude;
                this.CurrentSelection = position;
                
                var dbLocation = new SQLiteLocationModel(){
                    Latitude = latestLocation.Latitude,
                    Longitude = latestLocation.Longitude,
                    LocationDate = (DateTime)latestLocation.CreatedAt,
                    UserId = ParseUser.CurrentUser.ObjectId.ToString()
                };

                await this._locationsDb.AddLocation(dbLocation);
            }
        }

        private async void GetLatestLocations()
        {
            var userID = ParseUser.CurrentUser.ObjectId.ToString();
            this.LatestLocationReviews = await this._locationsDb.GetLatestFiveLocationsForUser(userID);
        }
    }
}
