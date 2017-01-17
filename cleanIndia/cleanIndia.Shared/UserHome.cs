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
            
        }
        private void AddComplaint_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(UserAddComplaint));
        }
        

        
    }
}
