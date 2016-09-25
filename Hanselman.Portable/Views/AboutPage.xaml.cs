using Hanselman.Portable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Share;
using Hanselman.Portable.Helpers;

namespace Hanselman.Portable.Views
{
    public partial class AboutPage : ContentPage
    {

        void OpenBrowser(string url)
        {
            CrossShare.Current.OpenBrowser(url, new Plugin.Share.Abstractions.BrowserOptions
            {
                ChromeShowTitle = true,
                ChromeToolbarColor = new Plugin.Share.Abstractions.ShareColor { R = 3, G = 169, B = 244, A = 255 },
                UseSafairReaderMode = true,
                UseSafariWebViewController = true
            });
        }
        public AboutPage()
        {
            InitializeComponent();

            twitter.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    //try to launch twitter or tweetbot app, else launch browser
                    var launch = DependencyService.Get<ILaunchTwitter>();
                    if(launch == null || !launch.OpenUserName("shanselman"))
                        OpenBrowser("http://m.twitter.com/shanselman");
                })
            });

            facebook.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() => OpenBrowser("https://m.facebook.com/shanselman"))
            });


            instagram.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() => OpenBrowser("https://www.instagram.com/shanselman"))
            });



        }
    }
}
