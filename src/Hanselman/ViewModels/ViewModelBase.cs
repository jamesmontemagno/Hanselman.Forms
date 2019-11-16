using System.Threading.Tasks;
using Hanselman.Interfaces;
using MvvmHelpers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Hanselman.ViewModels
{
    public class ViewModelBase : BaseViewModel
    {
        IDataService? dataService;
        protected IDataService DataService => dataService ??= DependencyService.Get<IDataService>();

        protected Page CurrentPage => Application.Current.MainPage;

        protected Task DisplayAlert(string title, string message, string cancel) =>
            CurrentPage.DisplayAlert(title, message, cancel);

        public static Task OpenBrowserAsync(string url) =>
            Browser.OpenAsync(url, new BrowserLaunchOptions
            {
                LaunchMode = BrowserLaunchMode.SystemPreferred,
                TitleMode = BrowserTitleMode.Show,
                PreferredControlColor = Color.White,
                PreferredToolbarColor = (Color)Application.Current.Resources["PrimaryColor"]
            });
    }
}
