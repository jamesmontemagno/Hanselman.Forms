using System;

using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using ImageCircle.Forms.Plugin.iOS;
using MediaManager.Forms.Platforms.iOS;
using Refractored.XamForms.PullToRefresh.iOS;
using MediaManager;

namespace Hanselman.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to
    // application events from iOS.
    [Foundation.Register("AppDelegate")]
    public partial class AppDelegate : FormsApplicationDelegate
    {

        //
        // This method is invoked when the application has loaded and is ready to run. In this
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(43, 132, 211); //bar background
            UINavigationBar.Appearance.TintColor = UIColor.White; //Tint color of button items
            UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes()
            {
                Font = UIFont.FromName("HelveticaNeue-Light", (nfloat)20f),
                TextColor = UIColor.White
            });

            Forms.SetFlags("CollectionView_Experimental");
            Forms.Init();
            FormsMaterial.Init();
            PullToRefreshLayoutRenderer.Init();
            CrossMediaManager.Current.Init();
            ImageCircleRenderer.Init();

            Shiny.iOSShinyHost.Init(new Hanselman.Startup());

            LoadApplication(new App());


            return base.FinishedLaunching(app, options);
        }

        public override void PerformFetch(UIApplication application, Action<UIBackgroundFetchResult> completionHandler)
            => Shiny.Jobs.JobManager.OnBackgroundFetch(completionHandler);

    }
}

