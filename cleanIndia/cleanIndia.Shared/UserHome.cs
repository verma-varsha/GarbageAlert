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
using Newtonsoft;
using Newtonsoft.Json;

namespace cleanIndia
{
    public sealed partial class UserHome:Page
    {
        public UserHome()
        {
            this.InitializeComponent();
            myComplaintHistory();
            
        }

        private async void myComplaintHistory()
        {
            string email = (App.Current as App).GEmail;
            string url = "http://immense-cliffs-95646.herokuapp.com/api/userComplaintData/";
            JsonObject j =await complainHistory(url, email);
            if (j.GetNamedNumber("status") == 1)
            {
                dynamic jsonObj = JsonConvert.DeserializeObject(j.GetNamedArray("data").ToString());
                List<Complaint> cl = new List<Complaint>();
                foreach (dynamic x in jsonObj)
                {
                    Complaint c1 = new Complaint();
                    c1.Latitude = x.latitude;
                    c1.Longitude = x.longitude;
                    //c1.ComplaintName = x.title;
                    c1.DateOfComplaint = x.dateOfComplain;
                    c1.DateOfCompletion = x.dateOfCompletion;
                    c1.DateOfSchedule = x.dateOfSchedule;
                    c1.ComplaintImageURI=x.complaintImage;
                    c1.FinalImageURI = x.finalImage;
                    c1.AgencyName = x.agency;
                    c1.UserName = x.user;
                    cl.Add(c1);
                }
                myComplaints_ListView.ItemsSource = cl;
            }
        }

        private async static Task<JsonObject> complainHistory(string url, string email)
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

        private void AddComplaint_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(UserAddComplaint));
        }
        

        
    }
}
