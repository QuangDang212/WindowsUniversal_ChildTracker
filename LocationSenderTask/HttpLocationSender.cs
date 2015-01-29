namespace LocationSenderTask
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Windows.Storage;

    public sealed class HttpLocationSender
    {
        private const string BASE_ADDRESS = "http://childtrackerservice.apphb.com/api/";
        private const string LOGOUT_URL = "Account/Logout";     
        private const string LOCATIONS_URL = "locations";

        private HttpClient client;

        public HttpLocationSender()
        {
            this.client = new HttpClient
            {
                BaseAddress = new Uri(BASE_ADDRESS)
            };
        }

        public async void SendLocation(BackgroundLocationModel location)
        {
            var locationAsString = JsonConvert.SerializeObject(location);
            HttpContent content = new StringContent(locationAsString, Encoding.UTF8);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            ApplicationDataContainer localData = ApplicationData.Current.LocalSettings;
            var token = localData.Values["accessToken"].ToString();
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
          
            var result = await this.client.PostAsync(LOCATIONS_URL, content);
        }
    }
}
