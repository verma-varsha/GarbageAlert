using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace cleanIndia
{
    public sealed partial class UserHome:Page
    {
        private void AddComplaint_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(UserAddComplaint));
        }
    }
}
