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

        public async Task<bool> SignUpUser()
        {
            try
            {
                var user = new ParseUser();
                user.Username = this.User.Username;
                user.Password = this.User.Password;

                await user.SignUpAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<string> LoginUser()
        {
            try
            {
                await ParseUser.LogInAsync(this.User.Username, this.User.Password);
                return "OK";
            }
            catch (Exception e)
            {                
                return e.Message;
            }
        }
    }
}
