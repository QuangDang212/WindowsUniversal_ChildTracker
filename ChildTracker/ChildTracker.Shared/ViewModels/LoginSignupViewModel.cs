using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ChildTracker.ViewModels
{
    public class LoginSignupViewModel : ViewModelBase
    {
        public UserViewModel User { get; set; }
        public string Username { get; set; }
        private ICommand signUpCommand;
        private ICommand loginChildDevice;

        public LoginSignupViewModel()
        {
            this.User = new UserViewModel();
        }

        public ICommand SignUpCommand
        {
            get
            {
                if (this.signUpCommand==null)
                {
                    this.signUpCommand = new RelayCommand(PerformSignup);
                }
                return this.signUpCommand;
            }           
        }
        
        public ICommand LoginChildDevice
        {
            get
            {
                if (this.loginChildDevice == null)
                {
                    this.loginChildDevice = new RelayCommand(PerformChildLogin);
                }
                return this.loginChildDevice;
            }           
        }

        private void PerformChildLogin()
        {
            throw new NotImplementedException();
        }

        private void PerformSignup()
        {
            var username = User.Username;
            var pass = User.Password;
            var b = 5;
        }

    }
}
