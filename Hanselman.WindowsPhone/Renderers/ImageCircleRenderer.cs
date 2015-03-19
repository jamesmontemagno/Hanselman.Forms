
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;
using System.Windows.Media;
using Hanselman.Portable.Helpers;
using Hanselman.WindowsPhone.Renderers;

[assembly: ExportRenderer(typeof(CircleImage), typeof(ImageCircleRenderer))]
namespace Hanselman.WindowsPhone.Renderers
{
  /// <summary>
  /// ImageCircle Implementation
  /// </summary>
  public class ImageCircleRenderer : ImageRenderer
  {


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
        Control.Clip = new EllipseGeometry
        {
          Center = new System.Windows.Point(min, min),
          RadiusX = min,
          RadiusY = min
        };
      }
    }
  }
}
