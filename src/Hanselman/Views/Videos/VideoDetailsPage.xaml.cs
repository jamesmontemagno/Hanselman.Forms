using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hanselman.Helpers;
using Hanselman.Models;
using Hanselman.ViewModels;
using MediaManager;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hanselman.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideoDetailsPage : ContentPage
    {
        VideoDetailsViewModel vm;
        VideoDetailsViewModel VM => vm ?? (vm = (VideoDetailsViewModel)BindingContext);

        public VideoDetailsPage(VideoFeedItem item) : this()
        {
            BindingContext = new VideoDetailsViewModel(item);
        }
        public VideoDetailsPage()
        {
            InitializeComponent();
        }
        bool shouldSeek;
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            CrossMediaManager.Current.StateChanged += PlaybackStateChanged;

            if (!CrossMediaManager.Current.IsStopped() &&
                Settings.PlaybackId == VM.VideoId)
                return;            

            Settings.PlaybackId = VM.VideoId;
            Settings.PlaybackUrl = VM.VideoUrl;
            shouldSeek = true;
            await CrossMediaManager.Current.Play(VM.VideoUrl);
        }

        private async void PlaybackStateChanged(object sender, MediaManager.Playback.StateChangedEventArgs e)
        {
            if(shouldSeek && e.State == MediaManager.Player.MediaPlayerState.Playing)
            {
                shouldSeek = false;
                var seekTo = Settings.GetPlaybackPosition(VM.Video.Id);
                if (seekTo > 0)
                    await CrossMediaManager.Current.SeekTo(TimeSpan.FromTicks(seekTo));
            }
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            CrossMediaManager.Current.StateChanged -= PlaybackStateChanged;
        }
    }
}