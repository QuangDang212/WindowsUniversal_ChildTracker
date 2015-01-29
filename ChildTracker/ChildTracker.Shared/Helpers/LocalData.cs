using System;
using System.Collections.Generic;
using System.Text;
using Windows.Storage;

namespace ChildTracker.Helpers
{
    public static class LocalData
    {
        private static ApplicationDataContainer localData = ApplicationData.Current.LocalSettings;


        public static string PASSWORD
        {
            get { return localData.Values["password"] as string; }
            set { localData.Values["password"] = value; }
        }

        public static string USERNAME
        {
            get { return localData.Values["userName"] as string; }
            set { localData.Values["userName"] = value; }
        }

        public static string LOGIN_TYPE
        {
            get { return localData.Values["loginType"] as string; }
            set { localData.Values["loginType"] = value; }
        }

        public static string TOKEN
        {
            get { return localData.Values["accessToken"] as string; }
            set { localData.Values["accessToken"] = value; }
        }        
    }
}
