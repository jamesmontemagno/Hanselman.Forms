using System.Threading.Tasks;
using System.Windows.Input;
using Hanselman.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Hanselman.Models
{
    public class SocialItem
    {
        public SocialItem()
        {
            OpenUrlCommand = new Command(async () => await OpenSocialUrl());
        }

        public string Icon { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;

        public ICommand OpenUrlCommand { get; }

        async Task OpenSocialUrl()
        {
            if (DeviceInfo.Platform == DevicePlatform.iOS && Url.Contains("twitter"))
            {
                if(await Launcher.CanOpenAsync("twitter://"))
                {
                    await Launcher.OpenAsync("twitter://user?screen_name=shanselman");
                    return;
                }
                else if(await Launcher.CanOpenAsync("tweetbot://"))
                {
                    await Launcher.OpenAsync("tweetbot://shanselman/timeline");
                    return;
                }
            }
            await ViewModelBase.OpenBrowserAsync(Url);
        }
    }
}
