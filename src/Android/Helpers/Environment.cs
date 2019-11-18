using System;
using Android.Content.Res;
using Android.OS;
using Hanselman.Interfaces;
using Hanselman.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(HanselmanAndroid.Helpers.Environment))]
namespace HanselmanAndroid.Helpers
{
    public class Environment : IEnvironment
    {
        public Theme GetOSTheme()
        {
            //Ensure the device is running Android Froyo or higher because UIMode was added in Android Froyo, API 8.0
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Froyo)
            {
                var uiModelFlags = Android.App.Application.Context.Resources.Configuration.UiMode & UiMode.NightMask;

                switch (uiModelFlags)
                {
                    case UiMode.NightYes:
                        return Theme.Dark;
                    case UiMode.NightNo:
                        return Theme.Light;
                    default:
                        throw new NotSupportedException($"UiMode {uiModelFlags} not supported");
                }
            }
            else
            {
                return Theme.Light;
            }
        }

        public void SetStatusBarColor(System.Drawing.Color color, bool darkStatusBarTint)
        {
            if (Build.VERSION.SdkInt < Android.OS.BuildVersionCodes.Lollipop)
                return;

            var activity = Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity;
            var window = activity.Window;
            window.AddFlags(Android.Views.WindowManagerFlags.DrawsSystemBarBackgrounds);
            window.ClearFlags(Android.Views.WindowManagerFlags.TranslucentStatus);
            window.SetStatusBarColor(color.ToPlatformColor());
            
            if(Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.M)
            {
                var flag = (Android.Views.StatusBarVisibility)Android.Views.SystemUiFlags.LightStatusBar;
                window.DecorView.SystemUiVisibility = darkStatusBarTint ? flag : 0;
            }
        }
    }
}