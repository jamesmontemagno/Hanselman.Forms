using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Hanselman.Helpers;
using Hanselman.Models;
using Hanselman.ViewModels;
using Xamarin.Forms;

namespace Hanselman.Views
{
    public partial class VideoDetailsPage : ContentPage
    {
        VideoDetailsViewModel? vm;
        VideoDetailsViewModel? VM => vm ??= BindingContext as VideoDetailsViewModel;
        System.Timers.Timer inactivityTimer;
        public VideoDetailsPage(VideoFeedItem item) : this()
        {
            BindingContext = new VideoDetailsViewModel(item);
        }
        public VideoDetailsPage()
        {
            InitializeComponent();
            inactivityTimer = new System.Timers.Timer(TimeSpan.FromSeconds(3).TotalMilliseconds);
        }
        bool shouldSeek;
        long seekTo;
        protected override void OnAppearing()
        {
            base.OnAppearing();
                    

            if (VM?.Video == null)
                return;

            Settings.PlaybackId = VM.VideoId;
            Settings.PlaybackUrl = VM.VideoUrl;
            seekTo = Settings.GetPlaybackPosition(VM.Video.Id);
            shouldSeek = seekTo > 0;
            inactivityTimer.Elapsed += OnInactivityTimerElapsed;
            inactivityTimer.Start();
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            var current = MediaElementVideo.Position.Ticks;
            Settings.SavePlaybackPosition(Settings.PlaybackId, current);
            MediaElementVideo.Stop();
            inactivityTimer.Elapsed -= OnInactivityTimerElapsed;
            inactivityTimer.Stop();
#if !DEBUG
            if (Navigation.ModalStack.Any())
                await Navigation.PopModalAsync();
#endif
        }

        async void OnInactivityTimerElapsed(object sender, ElapsedEventArgs e)
        {
            await Task.WhenAny<bool>
            (
                CloseButton.FadeTo(0)
            );

            inactivityTimer.Stop();
        }

        async void OnTapGestureRecognizerTapped(object sender, EventArgs e)
        {
            if (CloseButton.Opacity == 1)
            {
                await Task.WhenAny<bool>
                (
                    CloseButton.FadeTo(0)
                );
                inactivityTimer.Stop();
            }
            else
            {
                await Task.WhenAny<bool>
                (
                    CloseButton.FadeTo(1, 100)
                );
                inactivityTimer.Start();
            }

        }

        void MediaElementVideo_MediaOpened(object sender, EventArgs e)
        {
            if (!shouldSeek)
            {
                MediaElementVideo.Play();
                return;
            }
            shouldSeek = false;
            var time = TimeSpan.FromTicks(seekTo);
            MediaElementVideo.Position = time;
            MediaElementVideo.Play();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (CloseButton.Opacity == 0)
                return;

            await Navigation.PopModalAsync();
        }


        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            base.LayoutChildren(x, y, width, height);
            MediaElementVideo.Aspect = Aspect.AspectFill;
            MediaElementVideo.Aspect = Aspect.AspectFit;

        }
    }
}