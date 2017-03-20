using System;
using System.Linq;
using Hanselman.Portable.Models;
using Plugin.MediaManager;
using Plugin.MediaManager.Abstractions.Enums;
using Plugin.MediaManager.Abstractions.EventArguments;
using Plugin.MediaManager.Abstractions.Implementations;
using Xamarin.Forms;

namespace Hanselman.Portable.Views
{
    public partial class Channel9VideoPlaybackPage : ContentPage
    {
        public Channel9VideoPlaybackPage(VideoFeedItem item)
        {
            InitializeComponent();
            BindingContext = item;
            
        }

        private void OnPauseClicked(object sender, EventArgs e)
        {
            CrossMediaManager.Current.Pause();
            pause.IsEnabled = false;
        }

        private void OnStopClicked(object sender, EventArgs e)
        {
            CrossMediaManager.Current.Stop();
            pause.IsEnabled = false;
            stop.IsEnabled = false;
        }

        private async void OnPlayClicked(object sender, EventArgs e)
        {
            await CrossMediaManager.Current.PlaybackController.PlayPause();
            pause.IsEnabled = true;
            stop.IsEnabled = true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var item = (VideoFeedItem) BindingContext;
            CrossMediaManager.Current.StatusChanged += CurrentOnStatusChanged;
            CrossMediaManager.Current.PlayingChanged += OnPlayingChanged;
            player.Source = item.VideoUrls.First().Url;
            play.Clicked += OnPlayClicked;
            stop.Clicked += OnStopClicked;
            pause.Clicked += OnPauseClicked;
        }

        private void OnPlayingChanged(object sender, PlayingChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                progress.Progress = e.Progress;
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            CrossMediaManager.Current.Stop();
            play.Clicked -= OnPlayClicked;
            stop.Clicked -= OnStopClicked;
            pause.Clicked -= OnPauseClicked;
            CrossMediaManager.Current.StatusChanged -= CurrentOnStatusChanged;
            CrossMediaManager.Current.PlayingChanged -= OnPlayingChanged;
        }

        private void CurrentOnStatusChanged(object sender, StatusChangedEventArgs statusChangedEventArgs)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var status = statusChangedEventArgs.Status;
                switch (status)
                {
                    case MediaPlayerStatus.Stopped:
                        pause.IsEnabled = false;
                        stop.IsEnabled = false;
                        break;
                    case MediaPlayerStatus.Paused:
                        pause.IsEnabled = true;
                        stop.IsEnabled = true;
                        break;
                    case MediaPlayerStatus.Playing:
                        pause.IsEnabled = true;
                        stop.IsEnabled = true;
                        break;
                    case MediaPlayerStatus.Loading:
                        pause.IsEnabled = false;
                        stop.IsEnabled = false;
                        break;
                    case MediaPlayerStatus.Buffering:
                        pause.IsEnabled = false;
                        stop.IsEnabled = true;
                        break;
                    case MediaPlayerStatus.Failed:
                        pause.IsEnabled = false;
                        stop.IsEnabled = false;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            });
        }
    }
}
