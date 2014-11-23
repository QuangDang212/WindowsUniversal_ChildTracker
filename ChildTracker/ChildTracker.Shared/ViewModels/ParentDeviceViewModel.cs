using ChildTracker.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Devices.Geolocation;

namespace ChildTracker.ViewModels
{
    public class ParentDeviceViewModel:ViewModelBase
    {
        //private ICommand _getLocationCommand;
        private BasicGeoposition _currentSelection;

        //public ICommand GetLocation
        //{
        //    get
        //    {
        //        if (this._getLocationCommand == null)
        //        {
        //            this._getLocationCommand = new RelayCommand(GetChildLastLocation);
        //        }
        //        return this._getLocationCommand;
        //    }
        //}

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
                //TODO: add to SQLite
            }
        }
    }
}
