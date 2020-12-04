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
    
        public VideoDetailsPage()
        {
            InitializeComponent();
            BindingContext = new VideoDetailsViewModel();

            inactivityTimer = new System.Timers.Timer(TimeSpan.FromSeconds(3).TotalMilliseconds);
        }

        void VM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(VM != null && e.PropertyName != nameof(VM.VideoUrl))
            {
                Settings.PlaybackId = VM.Id;
                Settings.PlaybackUrl = VM.VideoUrl;
                seekTo = Settings.GetPlaybackPosition(VM.Id);
                shouldSeek = seekTo > 0;
                inactivityTimer.Elapsed += OnInactivityTimerElapsed;
                inactivityTimer.Start();
            }
        }

        bool shouldSeek;
        long seekTo;
        protected override void OnAppearing()
        {
            base.OnAppearing();

            VM.PropertyChanged += VM_PropertyChanged;
            VM?.LoadVideoCommand.Execute(null);
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            VM.PropertyChanged -= VM_PropertyChanged;
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