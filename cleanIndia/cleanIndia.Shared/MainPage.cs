﻿using System;
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
using Windows.Data.Json;

namespace cleanIndia
{
    public sealed partial class MainPage :Page
    {
        
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Registration));
        }
        

        
        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string url = "http://immense-cliffs-95646.herokuapp.com/api/userLogin/";
            string uemail = email.Text;
            string password = pass.Password;
            JsonObject j = await login(url, uemail, password);
            double status = j.GetNamedNumber("status");
            if (status == 1)
            {
                (App.Current as App).GEmail = j.GetNamedString("email");
                (App.Current as App).GName = j.GetNamedString("name");
                (App.Current as App).GPhone = j.GetNamedString("mobileNumber");
                this.Frame.Navigate(typeof(UserHome));
            }
            else
            {
                var d = new MessageDialog("Invalid Credentials.");
                await d.ShowAsync();
            }
            
       /*     string Name = name.Text;
            string Email = email.Text;
            string Number = phone.Text;
            string pass = password.Password;
            RegisterData holder = new RegisterData(Name, Email, Number, pass);
            string url = "http://localhost:8000/api/registration/";
            Class1.register(url, holder);
            //this.Frame(typeof(Registration));
         */   //Class1.PostRequestaa("http://technexuser.herokuapp.com/api/register/");
        }

        private void AgencyLink_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AgencyLogin));
        }



        private async static Task<JsonObject> login(string url, string email, string password)
        {
            IEnumerable<KeyValuePair<string, string>> emails = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string,string>("email",email),
                new KeyValuePair<string,string>("password",password)
                
            };
            HttpContent q = new FormUrlEncodedContent(emails);
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "x-www-form-urlencoded");
                using (HttpResponseMessage response = await client.PostAsync(url, q))
                {
                    using (HttpContent content = response.Content)
                    {
                        string mycontent = await content.ReadAsStringAsync();
                        JsonObject dataJson = JsonObject.Parse(mycontent);
                        Debug.WriteLine(dataJson);
                        return dataJson;
                    }


                }

            }
        }
        
    }
}
