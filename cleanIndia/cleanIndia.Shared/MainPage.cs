using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Geolocation;
using System.Diagnostics;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Media.Animation;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Windows.UI.Popups;
using Windows.ApplicationModel.Core;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.ApplicationModel.Activation;

namespace cleanIndia
{
    public sealed partial class MainPage :Page
    {
        
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            
        }

        

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            this.Frame.Navigate(typeof(Registration),null);
       /*     string Name = name.Text;
            string Email = email.Text;
            string Number = phone.Text;
            string pass = password.Password;
            RegisterData holder = new RegisterData(Name, Email, Number, pass);
            string url = "http://localhost:8000/api/registration/";
            Class1.register(url, holder);
            //this.Frame(typeof(Registration));
         */   //Class1.PostRequestaa("http://technexuser.herokuapp.com/api/register/");
        }    }
}
