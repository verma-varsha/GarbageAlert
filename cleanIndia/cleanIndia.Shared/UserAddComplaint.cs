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
using Windows.ApplicationModel.Core;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Graphics.Imaging;
using System.Diagnostics;
using Windows.Devices.Geolocation;


namespace cleanIndia
{
    public sealed partial class UserAddComplaint:Page
    {
        private async void UploadPicture_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker picker = new FileOpenPicker();
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.FileTypeFilter.Clear();
            picker.FileTypeFilter.Add(".png");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".bmp");
            // Create An Instance of WriteableBitmap object  
            WriteableBitmap writeableBitmap = new WriteableBitmap(300, 300);
            StorageFile file = await picker.PickSingleFileAsync();
            using (IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read))
            {
                // set the source for WriteableBitmap  
                await writeableBitmap.SetSourceAsync(fileStream);
            }
            // Save the writeableBitmap object to JPG Image file 
            FileSavePicker fpicker = new FileSavePicker();
            //   picker.FileTypeChoices.Add("JPG File", new List<string>() { ".jpg" });
            // picker.FileTypeChoices.Add("PNG File", new List<string>() { ".png" });
            //  picker.FileTypeChoices.Add("JPG File", new List<string>() { ".jpg" });
            //  StorageFile savefile = await file.OpenAsync();
            if (file == null)
                return;
            IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.ReadWrite);
            BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, stream);
            // Get pixels of the WriteableBitmap object 
            Stream pixelStream = writeableBitmap.PixelBuffer.AsStream();
            byte[] pixels = new byte[pixelStream.Length];
            await pixelStream.ReadAsync(pixels, 0, pixels.Length);
            // Save the image file with jpg extension 
            encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore, (uint)writeableBitmap.PixelWidth, (uint)writeableBitmap.PixelHeight, 96.0, 96.0, pixels);
            await encoder.FlushAsync();
            img.Source = writeableBitmap;
            Debug.WriteLine(pixelStream.Length);
        }

        private async void SubmitComplaint_Click(object sender, RoutedEventArgs e)
        {
            string complaintTitle = ComplaintTitle.Text;
            ImageSource image = img.Source;
            Geolocator gl = new Geolocator();
            Geoposition gp = await gl.GetGeopositionAsync();
            double latitude = gp.Coordinate.Latitude;
            double longitude = gp.Coordinate.Longitude;
        }
    }
}
