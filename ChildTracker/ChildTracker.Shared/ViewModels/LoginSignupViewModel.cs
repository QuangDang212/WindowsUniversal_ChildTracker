using ChildTracker.Helpers;
using ChildTracker.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Parse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;

namespace ChildTracker.ViewModels
{
    public class LoginSignupViewModel : ViewModelBase
    {
        private bool isLoading;

        public LoginSignupViewModel()
        {
            this.User = new UserViewModel();
        }
        public UserViewModel User { get; set; }

        public bool IsLoading
        {
            get { return this.isLoading; }
            set
            {
                this.isLoading = value;
                this.RaisePropertyChanged(() => this.IsLoading);
            }
        }

        public async Task<string> SignUpUser()
        {

            var user = new SignUpModel();
            user.Email = this.User.Email;
            user.Password = this.User.Password;
            user.ConfirmPassword = this.User.Password;

            var requester = new LocationsHttpRequester();
            var message = await requester.RegisterUser(user);

            return message;
        }

        public async Task<string> LoginUser()
        {
            
                var user = new LoginModel();
                user.Username = this.User.Email;
                user.Password = this.User.Password;
                user.Grant_Type = "password";

                var requester = new LocationsHttpRequester();
                var message = await requester.LoginUser(user);

                return message;          
        }
    }
}
