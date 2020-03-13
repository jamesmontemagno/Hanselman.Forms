using Xamarin.Forms;
using Hanselman.Views;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using MonkeyCache.FileStore;
using Hanselman.Helpers;
using Hanselman.Styles;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

// ElectricHavoc cheered 10 March 29, 2019
// KymPhillpotts cheered 50 March 29, 2019
// ElecticHavoc cheered 40 March 29, 2019
// lachlanwgordon cheered 100 August 30, 2019
// ClintonRocksmith cheered 110 September 20, 2019
// ClintonRocksmith cheered 1000 October 4th, 2019
// LotanB cheered 200 October 10, 2019
// mjfreelancing cheered 250 October 18, 2019
// Instafluff raided with 60 people on March 6 2020

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Hanselman
{
    public partial class App : Application
    {
        public static bool IsWindows10 { get; set; }
        public App()
        {
            Xamarin.Forms.Device.SetFlags(new List<string>() {
                    "StateTriggers_Experimental",
                    "IndicatorView_Experimental",
                    "CarouselView_Experimental",
                    "MediaElement_Experimental"
                });

            InitializeComponent();

            Barrel.ApplicationId = AppInfo.PackageName;

            if (DeviceInfo.Platform == DevicePlatform.UWP)
                MainPage = new HomePage();
            else
                MainPage = new AppShell();

        }

        const string AppCenteriOS = "APPCENTER_IOS";
        const string AppCenterAndroid = "APPCENTER_ANDROID";
        const string AppCenterUWP = "APPCENTER_UWP";

        protected override void OnStart()
        {

#if !DEBUG
            AppCenter.Start($"ios={AppCenteriOS};" +
                $"android={AppCenterAndroid};" +
                $"uwp={AppCenterUWP}", 
                typeof(Analytics), 
                typeof(Crashes),
                typeof(Distribute));
#endif
            OnResume();
        }

        DisplayOrientation currentOrientation = DisplayOrientation.Unknown;
        void DeviceDisplay_MainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
        {
            if (DeviceInfo.DeviceType == DeviceType.Virtual)
            {
                Task.Delay(500).ContinueWith((t) =>
                {
                    SetSpans(DeviceDisplay.MainDisplayInfo);
                }, TaskScheduler.FromCurrentSynchronizationContext());
                return;
            }

            SetSpans(e.DisplayInfo);
        }

        public static event EventHandler<EventArgs>? SpanChanged;

        void SetSpans(DisplayInfo info)
        {
            if (currentOrientation == info.Orientation)
                return;

            currentOrientation = info.Orientation;

            var dp = info.Width / info.Density;

            App.Current.Resources["BlogSpan"] = (int)(dp / 300);
            App.Current.Resources["VideoSpan"] = (int)(dp / 300);
            SpanChanged?.Invoke(null, EventArgs.Empty);
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            DeviceDisplay.MainDisplayInfoChanged -= DeviceDisplay_MainDisplayInfoChanged;
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            DeviceDisplay.MainDisplayInfoChanged += DeviceDisplay_MainDisplayInfoChanged;
            SetSpans(DeviceDisplay.MainDisplayInfo);
            ThemeHelper.ChangeTheme(Settings.ThemeOption, true);
        }
    }
}
