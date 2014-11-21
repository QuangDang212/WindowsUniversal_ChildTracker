using Parse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public sealed partial class ChildDeviceView : Page
    {
        public const string PageKey = "Child";

        public ChildDeviceView()
        {
            this.InitializeComponent();
        }

        private void OnLogoutClick(object sender, RoutedEventArgs e)
        {
            ParseUser.LogOut();
            //TODO: add password confirmation
            ApplicationDataContainer localData = ApplicationData.Current.LocalSettings;
            localData.Values["loginType"] = String.Empty;
            this.Frame.Navigate(typeof(LoginSignupPage));
            
        }

        //private async Task<bool> ConfirmPassword()
        //{
        //    var dialog = new MessageDialog("", "Confirm password");
            
        //    await dialog.ShowAsync();
        //}
    }
}
