
using Hanselman.Portable.Helpers;
using Hanselman.WindowsStore.Renderers;
using System;
using System.IO;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinRT;

[assembly: ExportRenderer(typeof(CircleImage), typeof(ImageCircleRenderer))]
namespace Hanselman.WindowsStore.Renderers
{
    /// <summary>
    /// ImageCircle Implementation
    /// </summary>
    public class ImageCircleRenderer : ViewRenderer<Xamarin.Forms.Image, Ellipse>
    {


        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);
            Ellipse ellipse = new Ellipse();

            SetNativeControl(ellipse);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected async override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (Control != null)
            {
                var min = Math.Min(Element.Width, Element.Height) / 2.0f;
                if (min <= 0)
                    return;


                Control.Width = Element.Width;
                Control.Height = Element.Height;

                // That will be our fallback fill if can't make sense of the ImageSource.
                Control.Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 52, 152, 219));


                BitmapImage bitmapImage = null;

                // Handle file images
                if (Element.Source is FileImageSource)
                {
                    FileImageSource fi = Element.Source as FileImageSource;
                    string myFile = System.IO.Path.Combine(Package.Current.InstalledLocation.Path, fi.File);
                    StorageFolder myFolder = await StorageFolder.GetFolderFromPathAsync(System.IO.Path.GetDirectoryName(myFile));

                    using (Stream s = await myFolder.OpenStreamForReadAsync(System.IO.Path.GetFileName(myFile)))
                    {
                        var memStream = new MemoryStream();
                        await s.CopyToAsync(memStream);
                        memStream.Position = 0;
                        bitmapImage = new BitmapImage();
                        bitmapImage.SetSource(memStream.AsRandomAccessStream());
                    }

                }

                // Handle uri images
                if (Element.Source is UriImageSource)
                    bitmapImage = new BitmapImage((Element.Source as UriImageSource).Uri);

                if (bitmapImage != null)
                    Control.Fill = new ImageBrush() { ImageSource = bitmapImage };

            }

        }
    }
}
