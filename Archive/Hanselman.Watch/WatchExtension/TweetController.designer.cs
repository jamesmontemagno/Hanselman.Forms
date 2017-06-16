// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace WatchExtension
{
	[Register ("TweetController")]
	partial class TweetController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		WatchKit.WKInterfaceImage ImageScott { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		WatchKit.WKInterfaceLabel TweetDate { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		WatchKit.WKInterfaceLabel TweetText { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (ImageScott != null) {
				ImageScott.Dispose ();
				ImageScott = null;
			}
			if (TweetDate != null) {
				TweetDate.Dispose ();
				TweetDate = null;
			}
			if (TweetText != null) {
				TweetText.Dispose ();
				TweetText = null;
			}
		}
	}
}
