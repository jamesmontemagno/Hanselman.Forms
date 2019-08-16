using System;
using System.Collections.Generic;
using System.Text;
using Hanselman.Models;
using MvvmHelpers;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

// ClintonRocksmith cheered 100 August 2, 2019

namespace Hanselman.ViewModels
{
    public class VideoSeriesViewModel : ViewModelBase
    {
        public ICommand LoadEpisodesCommand { get; set; }
        public VideoSeries VideoSeries { get; set; }
        public List<VideoFeedItem> AllEpisodes { get; set; }
        public ObservableRangeCollection<VideoFeedItem> Episodes { get; set; }

        public VideoSeriesViewModel()
        {
            LoadEpisodesCommand = new Command(async () => await ExecuteLoadEpisodesCommand());
            Episodes = new ObservableRangeCollection<VideoFeedItem>();
            AllEpisodes = new List<VideoFeedItem>();
        }
        public VideoSeriesViewModel(VideoSeries videoSeries) : this()
        {
            VideoSeries = videoSeries;
        }

        async Task ExecuteLoadEpisodesCommand()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
#if DEBUG
                await Task.Delay(1000);
#endif
                var episodes = await DataService.GetVideoEpisodesAsync(VideoSeries.Id, false);

                AllEpisodes.Clear();
                Episodes.Clear();
                CanLoadMore = true;
                AllEpisodes.AddRange(episodes);
                LoadMoreEpisodes();
            }
            catch (System.Exception ex)
            {
                //stuff
            }
            finally
            {
                IsBusy = false;
            }
        }

        const int chunk = 50;
        public void LoadMoreEpisodes()
        {
            if (!CanLoadMore)
                return;

            var totalLeft = AllEpisodes.Count - Episodes.Count;
            var toGet = totalLeft > chunk ? chunk : totalLeft;
            Episodes.AddRange(AllEpisodes.GetRange(Episodes.Count, toGet));
            CanLoadMore = Episodes.Count != AllEpisodes.Count;
        }
    }
}
