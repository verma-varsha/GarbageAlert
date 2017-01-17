using Bing.Maps;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Devices.Geolocation;
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

namespace cleanIndia
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AgencyMapView : Page
    {
        public AgencyMapView()
        {
            this.InitializeComponent();
            setCenter();
            showMyComplaints();
        }

        private async void setCenter()
        {
            Geolocator gl = new Geolocator();
            Geoposition gp = await gl.GetGeopositionAsync();
            MyMap.Center = new Location(gp.Coordinate.Latitude, gp.Coordinate.Longitude);
            ClearMap();
        }
        private void ClearMap()
        {
            DataLayer.Children.Clear();
        }

        private void OpenInfobox(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Pushpin pin = sender as Pushpin;
            //(App.Current as App).GLatitude=pin.
            Location a = MapLayer.GetPosition(pin);
            (App.Current as App).GLatitude = a.Latitude;
            (App.Current as App).GLongitude = a.Longitude;
            Infobox.DataContext = pin.Tag;
            InfoboxLayer.Visibility = Visibility.Visible;
            MapLayer.SetPosition(Infobox, MapLayer.GetPosition(pin));
        }
        private async static Task<JsonObject> openComplaintData(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(url))
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

        private void CloseInfobox_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            InfoboxLayer.Visibility = Visibility.Collapsed;
        }
        private async void showMyComplaints()
        {

            string url = "http://immense-cliffs-95646.herokuapp.com/api/openComplaintData/";
            JsonObject c = await openComplaintData(url);
            if (c.GetNamedNumber("status") == 1)
            {
                dynamic jsonObj = JsonConvert.DeserializeObject(c.GetNamedArray("data").ToString());
                /*foreach (dynamic x in jsonObj)
                {
                    double latitude = x.latitude;
                    Debug.WriteLine(latitude);
                    double longitude = x.longitude;
                    string status = x.status;
                    string user = x.user;
                    Location l = new Location(latitude, longitude);
                    Pushpin p = new Pushpin();
                    p.Text = user;
                    p.Background = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Red);
                    p.Tag = user;
                    MapLayer.SetPosition(p, l);
                    p.Tapped += OpenInfobox;
                    DataLayer.Children.Add(p); 
                }*/
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
                    c1.ComplaintImageURI = x.complaintImage;
                    c1.FinalImageURI = x.finalImage;
                    c1.AgencyName = x.agency;
                    c1.UserName = x.user;
                    c1.status=x.status;
                    cl.Add(c1);
                }
                foreach (Complaint a in cl)
                {
                    Location l = new Location(a.Latitude, a.Longitude);
                    Pushpin p = new Pushpin();
                    p.Text = a.UserName;
                    //p.Background = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Red);
                    if(a.status==0)
                    {
                        p.Background = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Red);
                    }
                    else
                    {
                        p.Background = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Green);
                    }
                        p.Tag = a;
                    MapLayer.SetPosition(p, l);
                    p.Tapped += OpenInfobox;
                    DataLayer.Children.Add(p);
                }
            }
            else
            {
                var d = new MessageDialog("Your data could not be loaded.");
                await d.ShowAsync();
            }
        }

        private async void PickComplaint_Click(object sender, RoutedEventArgs e)
        {
            string url = "http://immense-cliffs-95646.herokuapp.com/api/myComplain/";
            JsonObject j = await myComplain(url, (App.Current as App).GLatitude.ToString(), (App.Current as App).GLongitude.ToString(), (App.Current as App).GEmail);
            if (j.GetNamedNumber("status") == 1)
            {
                var d = new MessageDialog("Buck up! Time to do the job.");
                await d.ShowAsync();
            }
            else
            {
                var d = new MessageDialog("Error in Connection. Try Again.");
                await d.ShowAsync();
            }
        }

        private async static Task<JsonObject> myComplain(string url, string latitude, string longitude, string email)
        {
            IEnumerable<KeyValuePair<string, string>> emails = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string,string>("email",email),
                new KeyValuePair<string,string>("latitude",latitude),
                new KeyValuePair<string,string>("longitude",longitude)
                
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

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AgencyHome));
        }
    }
}
