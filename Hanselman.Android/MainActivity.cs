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

namespace HanselmanAndroid
{
    [Activity(Label = "Hanselman",
        MainLauncher = true,
        ScreenOrientation = ScreenOrientation.Portrait,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsApplicationActivity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);


            Forms.Init(this, bundle);
            ImageCircleRenderer.Init();
            LoadApplication(new App());

            if ((int)Android.OS.Build.VERSION.SdkInt >= 21)
            {
                ActionBar.SetIcon(
                    new ColorDrawable(Resources.GetColor(Android.Resource.Color.Transparent)));
            }
        }
    }
}
