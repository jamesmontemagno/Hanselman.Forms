using System;
using Hanselman.Helpers;
using Hanselman.Models;
using Hanselman.ViewModels;
using MediaManager;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hanselman.Views
{
    public partial class VideoDetailsPage : ContentPage
    {
        VideoDetailsViewModel? vm;
        VideoDetailsViewModel? VM => vm ??= BindingContext as VideoDetailsViewModel;

        public VideoDetailsPage(VideoFeedItem item) : this()
        {
            BindingContext = new VideoDetailsViewModel(item);
        }
        public VideoDetailsPage()
        {
            InitializeComponent();
        }
        bool shouldSeek;
        long seekTo;
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            CrossMediaManager.Current.StateChanged += PlaybackStateChanged;

            if (VM == null || VM.Video == null)
                return;

            if (!CrossMediaManager.Current.IsStopped() &&
                Settings.PlaybackId == VM.VideoId)
                return;

            await CrossMediaManager.Current.Stop();
            Settings.PlaybackId = VM.VideoId;
            Settings.PlaybackUrl = VM.VideoUrl;
            seekTo = Settings.GetPlaybackPosition(VM.Video.Id);
            await CrossMediaManager.Current.Play(VM.VideoUrl);
            shouldSeek = seekTo > 0;
        }

        async void PlaybackStateChanged(object sender, MediaManager.Playback.StateChangedEventArgs e)
        {
            if(shouldSeek && e.State == MediaManager.Player.MediaPlayerState.Playing)
            {
                shouldSeek = false;
                await CrossMediaManager.Current.SeekTo(TimeSpan.FromTicks(seekTo));
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            CrossMediaManager.Current.StateChanged -= PlaybackStateChanged;
        }
    }
}