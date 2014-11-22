using Parse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ChildTracker.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ConfirmLogoutView : Page
    {
        public ConfirmLogoutView()
        {
            this.InitializeComponent();
        }

        private async void OnLogoutButtonClick(object sender, RoutedEventArgs e)
        {
            ApplicationDataContainer localData = ApplicationData.Current.LocalSettings;
            var passwordInput = this.PasswordBoxInput.Password.GetHashCode().ToString();
            var userPassword = localData.Values["password"].ToString();

            if (passwordInput == userPassword)
            {
                ParseUser.LogOut();
                localData.Values["loginType"] = String.Empty;
                this.Frame.Navigate(typeof(LoginSignupPage));
            }
            else
            {
                var popup = new MessageDialog("Wrong password!", "Incorrect data!");
                await popup.ShowAsync();
            }
        }

        private void OnCancelButtonClick(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }
    }
}
