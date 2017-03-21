using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Content.PM;
using Hanselman.Portable;
using Android.Graphics.Drawables;
using ImageCircle.Forms.Plugin.Droid;
using Plugin.MediaManager.Forms;

namespace HanselmanAndroid
{
    [Activity(Label = "Hanselman",
        MainLauncher = true,
        ScreenOrientation = ScreenOrientation.Portrait,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {

        protected override void OnCreate(Bundle bundle)
        {
            FormsAppCompatActivity.ToolbarResource = Resource.Layout.toolbar;
            FormsAppCompatActivity.TabLayoutResource = Resource.Layout.tabs;
            base.OnCreate(bundle);
            Forms.Init(this, bundle);
            Plugin.MediaManager.Forms.Android.VideoViewRenderer.Init();
            ImageCircleRenderer.Init();
            LoadApplication(new App());

         
        }
    }
}
