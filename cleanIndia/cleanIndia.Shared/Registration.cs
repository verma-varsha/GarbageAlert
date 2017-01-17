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
using Windows.Data.Json;
using System.Runtime.Serialization;
using System.Net.Http;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Windows.UI.Popups;
using System.Globalization;
using System.Text.RegularExpressions;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

//using SDKTemplate;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace cleanIndia
{
    sealed partial class Registration : Page
    {
        
        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            
            string Name = uname.Text;
            string Email = email.Text;
            string Number = phone.Text;
            string pass = password.Password;
            RegisterData holder = new RegisterData(Name, Email, Number, pass);
            string url = "http://immense-cliffs-95646.herokuapp.com/api/registration/";
            
            JsonObject c=await register(url, holder);
            //test.Text = (App.Current as App).GName;
            //this.Frame(typeof(Registration));
            //Class1.PostRequestaa("http://technexuser.herokuapp.com/api/register/");
            

            if((c.GetNamedNumber("status")==1))
            {
                (App.Current as App).GName = c.GetNamedString("name");
                (App.Current as App).GEmail = c.GetNamedString("email");
                (App.Current as App).GPhone = c.GetNamedString("mobileNumber");
                this.Frame.Navigate(typeof(UserHome));
            }
            else
            {
                var d = new MessageDialog("Registration Failed!");
                await d.ShowAsync();
            }
           
        }



        private async static Task<JsonObject> register(string url, RegisterData data)
        {
            IEnumerable<KeyValuePair<string, string>> emails = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string,string>("email",data.email),
                new KeyValuePair<string,string>("name",data.name),
                new KeyValuePair<string,string>("password",data.password),
                new KeyValuePair<string,string>("mobileNumber",data.phoneNumber)
                
            };
            HttpContent q = new FormUrlEncodedContent(emails);

            using (HttpClient client = new HttpClient())
            {
                //client.GetAsync()
                // HttpResponseMessage response = client.GetAsync("http://www.google.co.in");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "x-www-form-urlencoded");
                using (HttpResponseMessage response = await client.PostAsync(url, q))
                {
                    using (HttpContent content = response.Content)
                    {
                        string mycontent = await content.ReadAsStringAsync();
                        HttpContentHeaders heaPOSTders = content.Headers;
                        //JsonValue data = JsonValue.Parse(mycontent);
                        //JsonObject result = data as JsonObject;
                        //JsonObject result = data as JsonObject;
                        JsonObject dataJson = JsonObject.Parse(mycontent);
                        Debug.WriteLine(dataJson);
                        /*double Status = dataJson.GetNamedNumber("status");
                        string Name = dataJson.GetNamedString("name");
                        string Email = dataJson.GetNamedString("email");
                        string MobileNumber = dataJson.GetNamedString("mobileNumber");
                        Debug.WriteLine(Status);
                        (App.Current as App).GName = Name;
                        Debug.WriteLine((App.Current as App).GName);
                        var d = new MessageDialog((App.Current as App).GName);
                        await d.ShowAsync();


                        //Debug.WriteLine(dataJson.GetNamedNumber("status"));
                        RegisterData g = new RegisterData(Name, Email, MobileNumber, "shat");*/
                        return dataJson;
                        //Debug.WriteLine(g);
                        //Debug.WriteLine(mycontent);
                    }


                }
               
            }
            
            
        }

        public static async Task<string> Upload(byte[] image)
        {
            using (var client = new HttpClient())
            {
                using (var content =
                    new MultipartFormDataContent("Upload----" + DateTime.Now.ToString(CultureInfo.InvariantCulture)))
                {
                    content.Add(new StreamContent(new MemoryStream(image)), "bilddatei", "upload.jpg");

                    using (
                       var message =
                           await client.PostAsync("http://www.directupload.net/index.php?mode=upload", content))
                    {
                        var input = await message.Content.ReadAsStringAsync();

                        return !string.IsNullOrWhiteSpace(input) ? Regex.Match(input, @"http://\w*\.directupload\.net/images/\d*/\w*\.[a-z]{3}").Value : null;
                    }
                }
            }
        }

        private async static Task<JsonObject> complainHistory(string url, string email = "TextBox@g.b")
        {
            IEnumerable<KeyValuePair<string, string>> emails = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string,string>("email",email)
                
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


        private async static Task<JsonObject> login(string url, string email = "TextBox@g.b", string password = "b")
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
        

        public static void captureComplain()
        {
            
        }
        
    }
}

