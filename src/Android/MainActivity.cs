
using Android.App;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Content.PM;
using Hanselman.Portable;
using ImageCircle.Forms.Plugin.Droid;
using Android.Runtime;

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
            ToolbarResource = Resource.Layout.Toolbar;
            TabLayoutResource = Resource.Layout.Tabbar;
            base.OnCreate(bundle);
            Forms.Init(this, bundle);
            Xamarin.Essentials.Platform.Init(this, bundle);
            Plugin.MediaManager.Forms.Android.VideoViewRenderer.Init();
            ImageCircleRenderer.Init();
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
