using ChildTracker.ViewModels;
using ChildTracker.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
            var username = this.ViewModel.User.Username;
            var pass = this.ViewModel.User.Password;

            var msg = new MessageDialog(string.Format("{0} - {1}", username, pass), "Test");
            this.ViewModel.IsLoading = true;
            await msg.ShowAsync();
            this.ViewModel.IsLoading = false;
        }

        private void OnLoginParentClick(object sender, RoutedEventArgs e)
        {

            this.Frame.Navigate(typeof(ParentDeviceView));
        }

        private void OnLoginChildClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ChildDeviceView));
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
    }
}
