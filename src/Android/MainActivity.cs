
using Android.App;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Hanselman;
using Hanselman.Styles;
using Shiny;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

// haavamoa cheered 1000 August 2, 2019
// blounty cheered 100 August 16, 2019


namespace HanselmanAndroid
{
    [Activity(Label = "@string/app_name",
        Theme="@style/MyTheme",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.UiMode | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {

        protected override void OnCreate(Bundle bundle)
        {
            ToolbarResource = Resource.Layout.Toolbar;
            TabLayoutResource = Resource.Layout.Tabbar;
            base.OnCreate(bundle);

            Forms.Init(this, bundle);
            FormsMaterial.Init(this, bundle);
            Xamarin.Essentials.Platform.Init(this, bundle);
            Android.Glide.Forms.Init(this);
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            AndroidShinyHost.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);

            ThemeHelper.ChangeTheme(Hanselman.Helpers.Settings.ThemeOption, true);
        }

    }
}
