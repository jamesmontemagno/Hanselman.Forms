using System;
using System.Linq;
using System.Timers;
using Hanselman.Models;
using Hanselman.ViewModels;
using Xamarin.Forms;

// mjfreelancing cheered 100 on March 6th 2020;

namespace Hanselman.Views
{
    public partial class PodcastEpisodePage : ContentPage
    {
        Timer playbackTimer;
        public PodcastEpisodePage(PodcastEpisode episode, string title) : this()
        {
            BindingContext = new PodcastEpisodeViewModel(episode)
            {
                Title = title
            };

        }

        public PodcastEpisodePage()
        {
            InitializeComponent();
            playbackTimer = new Timer(TimeSpan.FromSeconds(1).TotalMilliseconds);

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MediaElementAudio.StateRequested += MediaElementAudio_StateRequested;
        }

       


        async void ButtonClose_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            MediaElementAudio.StateRequested -= MediaElementAudio_StateRequested;
            if (MediaElementAudio.CurrentState == MediaElementState.Playing)
                Stop();

#if !DEBUG
            if (Navigation.ModalStack.Any())
                await Navigation.PopModalAsync();
#endif
        } 
        
        void Start()
        {
            playbackTimer.Elapsed += OnPlaybackTimerElapsed;
            playbackTimer.Start();
            MediaElementAudio.Play();
        }

        void Stop()
        {
            playbackTimer.Elapsed -= OnPlaybackTimerElapsed;
            playbackTimer.Stop();
            MediaElementAudio.Pause();
        }

        private void MediaElementAudio_StateRequested(object sender, StateRequested e)
        {
            VisualStateManager.GoToState(PlayPauseButton,
                 (e.State == MediaElementState.Playing)
                 ? "playing"
                 : "paused");
        }

        void OnPlaybackTimerElapsed(object sender, ElapsedEventArgs e)
        {
            UpdateTimeDisplay();
        }

        void PlayPauseButton_Clicked(object sender, EventArgs e)
        {
            if (MediaElementAudio.CurrentState == MediaElementState.Playing)
                Stop();
            else
                Start();
        }       

        void UpdateTimeDisplay()
        {

            Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(() =>
            {
                var total = MediaElementAudio.Duration?.TotalSeconds ?? 0;
                if (total == 0)
                    return;

                LabelTimeRemaining.Text = $"{MediaElementAudio.Position.ToString(@"hh\:mm\:ss")} / {MediaElementAudio.Duration?.ToString(@"hh\:mm\:ss")}";

                var progress = (MediaElementAudio.Position.TotalSeconds / total);
                if (progress > 1)
                    progress = 1;
                ProgressBarProgress.Progress = progress;
            });
        }
    }
}