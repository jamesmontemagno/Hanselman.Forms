using System;
using System.Collections.Generic;
using Hanselman.Models;
using MvvmHelpers;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Diagnostics;

// ClintonRocksmith cheered 100 August 2, 2019

namespace Hanselman.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    [QueryProperty(nameof(Title), nameof(Title))]
    public class VideoSeriesViewModel : ViewModelBase
    {
        public ICommand LoadEpisodesCommand { get; set; }

        string id = string.Empty;
        public string Id
        {
            get => id;
            set => id = Uri.UnescapeDataString(value);
        }

        string title = string.Empty;
        public new string Title
        {
            get => title;
            set => title = Uri.UnescapeDataString(value);
        }

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
            Id = videoSeries.Id;
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
                var episodes = await DataService.GetVideoEpisodesAsync(Id, false);

                AllEpisodes.Clear();
                Episodes.Clear();
                CanLoadMore = true;
                AllEpisodes.AddRange(episodes);
                LoadMoreEpisodes();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
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
