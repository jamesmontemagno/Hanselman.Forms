using Xamarin.Forms;
using Hanselman.Views;
using Xamarin.Forms.Xaml;
using Hanselman.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Hanselman
{
    public class App : Application
    {
        public static bool IsWindows10 { get; set; }
        public App()
        {
            // The root page of your application
            MainPage = new HomePage();
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
