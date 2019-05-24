using Xamarin.Forms;
using Hanselman.Views;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

// ElectricHavoc cheered 10 March 29, 2019
// KymPhillpotts cheered 50 March 29, 2019
// ElecticHavoc cheered 40 March 29, 2019

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Hanselman
{
    public partial class App : Application
    {
        public static bool IsWindows10 { get; set; }
        public App()
        {
            InitializeComponent();

            // The root page of your application
            if (DeviceInfo.Platform == DevicePlatform.UWP)
                MainPage = new HomePage();
            else
                MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
