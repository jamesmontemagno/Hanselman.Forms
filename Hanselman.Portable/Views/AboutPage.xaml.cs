
using Xamarin.Forms;
using Hanselman.Portable.Helpers;
using Xamarin.Essentials;

namespace Hanselman.Portable.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();

            twitter.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {
                    //try to launch twitter or tweetbot app, else launch browser
                    var launch = DependencyService.Get<ILaunchTwitter>();
                    if (launch == null || !launch.OpenUserName("shanselman"))
                        await Browser.OpenAsync("http://m.twitter.com/shanselman");
                })
            });

            facebook.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () => await Browser.OpenAsync("https://m.facebook.com/shanselman"))
            });


            instagram.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () => await Browser.OpenAsync("https://www.instagram.com/shanselman"))
            });



        }
    }
}
