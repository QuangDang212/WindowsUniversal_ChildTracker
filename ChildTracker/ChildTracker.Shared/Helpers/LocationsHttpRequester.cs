namespace ChildTracker.Helpers
{
    using ChildTracker.Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Windows.Storage;

    public class LocationsHttpRequester
    {
        private const string BASE_ADDRESS = "http://childtrackerservice.apphb.com/api/";
        private const string LOGOUT_URL = "Account/Logout";
        private const string LOGIN_URL = "Account/Login";
        private const string REGISTER_URL = "Account/Register";
        private const string LOCATIONS_URL = "locations";

        private HttpClient client;

        public LocationsHttpRequester()
        {
            this.client = new HttpClient
            {
                BaseAddress = new Uri(BASE_ADDRESS)
            };
        }

        public async Task<string> RegisterUser(SignUpModel user)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(user));
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await this.client.PostAsync(REGISTER_URL, content);
            var message = await response.Content.ReadAsStringAsync();

            try
            {
                response.EnsureSuccessStatusCode();

                return "OK";
            }
            catch (Exception)
            {
                var index = message.IndexOf("ModelState");
                var msg = message.Substring(index + 18);

                return msg;
            }
        }

        public async Task<string> LoginUser(LoginModel user)
        {
            var requestAsString = string.Format("grant_type=password&username={0}&password={1}", user.Username, user.Password);
            HttpContent content = new StringContent(requestAsString, Encoding.UTF8);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await this.client.PostAsync(LOGIN_URL, content);
            var message = await response.Content.ReadAsStringAsync();

            try
            {
                response.EnsureSuccessStatusCode();
                var responseAsObject = JsonConvert.DeserializeObject<SuccessfulLoginModel>(message);

                LocalData.TOKEN = responseAsObject.access_token;
                LocalData.USERNAME = responseAsObject.userName;

                return "OK";
            }
            catch (Exception)
            {
                return "Incorrect username or password!";
            }
        }

        public async Task<LocationModel> GetLocation()
        {
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", LocalData.TOKEN);
            var response = await this.client.GetAsync(LOCATIONS_URL);
            var obj = await response.Content.ReadAsStringAsync();

            var location = JsonConvert.DeserializeObject<LocationModel>(obj);
            return location;
        }

        public async void SendLocation(LocationModel location)
        {
            var locationAsString = JsonConvert.SerializeObject(location);
            HttpContent content = new StringContent(locationAsString, Encoding.UTF8);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", LocalData.TOKEN);
          
            await this.client.PostAsync(LOCATIONS_URL, content);
        }

        public async void Logout()
        {
            LocalData.LOGIN_TYPE = String.Empty;
            LocalData.PASSWORD = String.Empty;
            LocalData.USERNAME = String.Empty;
            LocalData.TOKEN = String.Empty;
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", LocalData.TOKEN);
            await this.client.GetAsync(LOGOUT_URL);
        }
    }
}
