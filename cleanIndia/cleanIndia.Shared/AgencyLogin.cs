using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace cleanIndia
{
    sealed partial class AgencyLogin:Page
    {
        private async void AgencyLogin_Click(object sender, RoutedEventArgs e)
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
                this.Frame.Navigate(typeof(AgencyHome));
            }
            else
            {
                var d = new MessageDialog("Invalid Credentials.");
                await d.ShowAsync();
            }

        }

        private void AgencyRegLink_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AgencyReg));
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
