using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hanselman.Portable.Helpers;
using Xamarin.Forms;
using Hanselman.iOS.Helpers;
using UIKit;
using Foundation;

[assembly: Xamarin.Forms.Dependency (typeof(Share))]
namespace Hanselman.iOS.Helpers
{
	public class Share : IShare
	{
		public void ShareText (string text)
		{
			var items = new NSObject[] { new NSString (text) };
			var activityController = new UIActivityViewController (items, null);
      UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewControllerAsync(activityController, true);
		}

		public void LaunchBrowser (string url)
		{
			if (string.IsNullOrWhiteSpace (url))
				return;
			try {
				UIApplication.SharedApplication.OpenUrl (new NSUrl (url));
			}
			catch (Exception ex) {
			}
		}
	}
}