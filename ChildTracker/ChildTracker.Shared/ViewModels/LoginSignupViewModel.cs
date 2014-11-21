using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Windows.UI.Popups;

namespace ChildTracker.ViewModels
{
    public class LoginSignupViewModel : ViewModelBase
    {

        public LoginSignupViewModel()
        {
            this.User = new UserViewModel();
        }
        public UserViewModel User { get; set; }

        private bool isLoading;

        public bool IsLoading
        {
            get { return this.isLoading; }
            set
            {
                this.isLoading = value;
                this.RaisePropertyChanged(() => this.IsLoading);
            }
        }

    }
}
