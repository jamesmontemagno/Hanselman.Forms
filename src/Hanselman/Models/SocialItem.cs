using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Hanselman.Helpers;
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

        public string Icon { get; set; }
        public string Url { get; set; }

        public ICommand OpenUrlCommand { get; }

        async Task OpenSocialUrl()
        {
            if (Url.Contains("twitter"))
            {
                var launch = DependencyService.Get<ILaunchTwitter>();
                if (launch?.OpenUserName("shanselman") ?? false)
                    return;
            }
            await ViewModelBase.OpenBrowserAsync(Url);
        }
    }
}
