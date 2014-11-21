using ChildTracker.ViewModels;
using ChildTracker.Views;
using Parse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ChildTracker
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginSignupPage : Page
    {
        public LoginSignupPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            var loginSignupVM = new LoginSignupViewModel();
            this.ViewModel = loginSignupVM;

        }
        public LoginSignupViewModel ViewModel
        {
            get
            {
                return (this.DataContext as LoginSignupViewModel);
            }

            set
            {
                this.DataContext = value;
            }
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private async void OnSignUpClick(object sender, RoutedEventArgs e)
        {
            string msg, msgTitle;

            if (!this.isUserInputValid())
            {
                msg = "Username and password should be at least 5 symbols long!";
                msgTitle = "Incorrect user input!";
            }
            else
            {
                this.ViewModel.IsLoading = true;
                var isSignUpSuccessful = await this.ViewModel.SignUpUser();

                if (isSignUpSuccessful)
                {
                    var username = this.ViewModel.User.Username;
                    msg = string.Format("You were successfully signed in. \n Now you can choose the appropriate Login!", username);
                    msgTitle = "Successful registration!";
                }
                else
                {
                    msg = "Username is already taken!";
                    msgTitle = "Registration failed!";
                }
            }

            var dialog = new MessageDialog(msg, msgTitle);
            await dialog.ShowAsync();
            this.ViewModel.IsLoading = false;
        }

        private async void OnLoginParentClick(object sender, RoutedEventArgs e)
        {
            var isLoginSuccessful = await this.Login();
            if (isLoginSuccessful)
            {
                this.Frame.Navigate(typeof(ParentDeviceView));
                //TODO: mark type of login
            }

        }

        private async void OnLoginChildClick(object sender, RoutedEventArgs e)
        {
            var isLoginSuccessful = await this.Login();
            if (isLoginSuccessful)
            {
                this.Frame.Navigate(typeof(ChildDeviceView));
                //TODO: mark type of login
            }
        }

        private async Task<bool> Login()
        {
            string msg, msgTitle;

            if (!this.isUserInputValid())
            {
                msg = "Username and password should be at least 5 symbols long!";
                msgTitle = "Incorrect user input!";
                await AlertUser(msg, msgTitle);

                return false;
            }
            else
            {
                this.ViewModel.IsLoading = true;
                var loginMsg = await this.ViewModel.LoginUser();
                this.ViewModel.IsLoading = false;

                if (loginMsg != "OK")
                {
                    await AlertUser(loginMsg, "Login failed!");
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        private bool isUserInputValid()
        {
            var username = this.ViewModel.User.Username;
            var password = this.ViewModel.User.Password;

            if (string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(password) ||
                username.Length < 5 || password.Length < 5)
            {
                return false;
            }
            return true;
        }

        private async Task AlertUser(string message, string title)
        {
            var dialog = new MessageDialog(message, title);
            await dialog.ShowAsync();
        }
    }
}
