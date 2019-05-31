
using Android.App;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Content.PM;
using Hanselman;
using ImageCircle.Forms.Plugin.Droid;
using Android.Runtime;
using Refractored.XamForms.PullToRefresh.Droid;

namespace HanselmanAndroid
{
    [Activity(Label = "Hanselman",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {

        protected override void OnCreate(Bundle bundle)
        {
            ToolbarResource = Resource.Layout.Toolbar;
            TabLayoutResource = Resource.Layout.Tabbar;
            base.OnCreate(bundle);

            Forms.SetFlags("CollectionView_Experimental");
            Forms.Init(this, bundle);
            FormsMaterial.Init(this, bundle);
            Xamarin.Essentials.Platform.Init(this, bundle); 
            PullToRefreshLayoutRenderer.Init();
            //MediaManager.Forms.Platforms.Android.VideoViewRenderer.Init();
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
