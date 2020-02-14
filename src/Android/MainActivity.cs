
using Android.App;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Content.PM;
using Hanselman;
using ImageCircle.Forms.Plugin.Droid;
using Android.Runtime;
using Shiny;
using MediaManager;

// haavamoa cheered 1000 August 2, 2019
// blounty cheered 100 August 16, 2019


namespace HanselmanAndroid
{
    [Activity(Label = "@string/app_name",
        Theme="@style/MyTheme",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {

        protected override void OnCreate(Bundle bundle)
        {
            ToolbarResource = Resource.Layout.Toolbar;
            TabLayoutResource = Resource.Layout.Tabbar;
            base.OnCreate(bundle);

            Forms.SetFlags("CarouselView_Experimental", "IndicatorView_Experimental");
            Forms.Init(this, bundle);
            FormsMaterial.Init(this, bundle);
            Xamarin.Essentials.Platform.Init(this, bundle);
            CrossMediaManager.Current.Init(this);
            ImageCircleRenderer.Init();
            Android.Glide.Forms.Init(this);
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            AndroidShinyHost.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}
