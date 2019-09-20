using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Hanselman.Models;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace Hanselman.ViewModels
{
    public class MediaDirectoryViewModel : ViewModelBase
    {

        public ObservableRangeCollection<Podcast> Podcasts { get; } = new ObservableRangeCollection<Podcast>();
        public ICommand LoadPodcastsCommand { get; }
        public ObservableRangeCollection<VideoSeries> VideoSeries { get; } = new ObservableRangeCollection<VideoSeries>();
        public ICommand LoadSeriesCommand { get; }
        
        public MediaDirectoryViewModel()
        {
            LoadPodcastsCommand = new AsyncCommand(ExecuteLoadPodcasts);
            LoadSeriesCommand = new AsyncCommand(ExecuteLoadVideoSeries);
        }

        async Task ExecuteLoadPodcasts()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;

                var pods = await DataService.GetPodcastsAsync(false);
                if (Podcasts.Count > 0)
                    Podcasts.ReplaceRange(pods);
                else
                    Podcasts.AddRange(pods);
                //OnPropertyChanged(nameof(Podcasts));
            }
            catch (Exception ex)
            {
#if DEBUG
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
#endif
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task ExecuteLoadVideoSeries()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;

                var pods = await DataService.GetVideoSeriesAsync(false);
                if (VideoSeries.Count > 0)
                    VideoSeries.ReplaceRange(pods);
                else
                    VideoSeries.AddRange(pods);
            }
            catch (Exception ex)
            {
#if DEBUG
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
#endif
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
