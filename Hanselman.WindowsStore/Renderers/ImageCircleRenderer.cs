
using System;
using Xamarin.Forms;
using Hanselman.Portable.Helpers;
using Hanselman.WindowsStore.Renderers;
using Xamarin.Forms.Platform.WinRT;
using Windows.UI.Xaml.Media;

[assembly: ExportRenderer(typeof(CircleImage), typeof(ImageCircleRenderer))]
namespace Hanselman.WindowsStore.Renderers
{
  /// <summary>
  /// ImageCircle Implementation
  /// </summary>
  public class ImageCircleRenderer : ImageRenderer
  {

    protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
    {
      base.OnElementChanged(e);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
      base.OnElementPropertyChanged(sender, e);
      if (Control != null && Control.Clip == null)
      {
        var min = Math.Min(Element.Width, Element.Height) / 2.0f;
        if (min <= 0)
          return;

        /*Control.Clip = new RectangleGeometry
        {
          
          Center = new Windows.Foundation.Point(min, min),
          RadiusX = min,
          RadiusY = min
        };*/
      }

    }
  }
}
