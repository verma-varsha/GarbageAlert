using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace cleanIndia
{
    public sealed partial class AgencyReg:Page
    {
        private async void AgencyReg_Click(object sender, RoutedEventArgs e)
        {
            string Name = uname.Text;
            string Email = email.Text;
            string Number = phone.Text;
            string pass = password.Password;
            RegisterData holder = new RegisterData(Name, Email, Number, pass);
            string url = "http://immense-cliffs-95646.herokuapp.com/api/registration/";

            JsonObject c = await register(url, holder, "1");
            //test.Text = (App.Current as App).GName;
            //this.Frame(typeof(Registration));
            //Class1.PostRequestaa("http://technexuser.herokuapp.com/api/register/");


            if ((c.GetNamedNumber("status") == 1))
            {
                (App.Current as App).GName = c.GetNamedString("name");
                (App.Current as App).GEmail = c.GetNamedString("email");
                (App.Current as App).GPhone = c.GetNamedString("mobileNumber");
                this.Frame.Navigate(typeof(AgencyHome));
            }
            else
            {
                var d = new MessageDialog("Registration Failed!");
                await d.ShowAsync();
            }
           
        }
        private async static Task<JsonObject> register(string url, RegisterData data, string userType)
        {
            IEnumerable<KeyValuePair<string, string>> emails = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string,string>("email",data.email),
                new KeyValuePair<string,string>("name",data.name),
                new KeyValuePair<string,string>("password",data.password),
                new KeyValuePair<string,string>("mobileNumber",data.phoneNumber),
                new KeyValuePair<string,string>("userType",userType)
                
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

    }
}
