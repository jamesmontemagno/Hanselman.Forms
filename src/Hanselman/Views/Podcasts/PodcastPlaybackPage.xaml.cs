using Xamarin.Forms;
using Xamarin.Essentials;
using Hanselman.Models;

namespace Hanselman.Views
{
    public partial class PodcastPlaybackPage : ContentPage
    {
        //IMediaPlayer PlaybackController => CrossMediaManager.Current.MediaPlayer;
        FeedItem item;
        public PodcastPlaybackPage(FeedItem item)
        {
            InitializeComponent();
            this.item = item;
            BindingContext = this.item;
            /*CrossMediaManager.Current.PlayingChanged += (sender, args) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    ProgressBar.Progress = args.Position.TotalSeconds / args.Duration.TotalSeconds;
                });
            };*/

            webView.Source = new HtmlWebViewSource
            {
                //Html = item.Description
            };

            var share = new ToolbarItem
            {
                IconImageSource = "ic_share.png",
                Text = "Share",
                Command = new Command(async () => await Browser.OpenAsync(this.item.Link))
            };

            ToolbarItems.Add(share);

            //play.Clicked += (sender, args) => PlaybackController.Play();
            //pause.Clicked += (sender, args) => PlaybackController.Pause();
            //stop.Clicked += (sender, args) => PlaybackController.Stop();

            if (Device.RuntimePlatform == Device.Android || Device.RuntimePlatform == Device.UWP || App.IsWindows10)
            {
                play.Text = "Play";
                pause.Text = "Pause";
                stop.Text = "Stop";
            }

            if (Device.RuntimePlatform == Device.UWP && !App.IsWindows10)
            {
                BackgroundColor = Color.White;
                title.TextColor = Color.Black;
                date.TextColor = Color.Black;
                play.TextColor = Color.Black;
                pause.TextColor = Color.Black;
                stop.TextColor = Color.Black;
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Device.BeginInvokeOnMainThread(() =>
            {
                //CrossMediaManager.Current.Play(item.Mp3Url);
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //PlaybackController.Stop();
        }
    }
}
