using Parse;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChildTracker.Models
{
    [ParseClassName("Location")]
    public class LocationModel : ParseObject
    {

        [ParseFieldName("longitude")]
        public double Longitude
        {
            get { return GetProperty<double>(); }
            set { SetProperty<double>(value); }
        }
        
        [ParseFieldName("latitude")]
        public double Latitude
        {
            get { return GetProperty<double>(); }
            set { SetProperty<double>(value); }
        }

        //TODO: Change LocationModel to hold userId 
    }
}
